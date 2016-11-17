using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using NHibernate.Criterion;

namespace FaPA.Infrastructure.Finder
{
    public class EnumSearchProperty : SearchProperty<Enum>
    {
        private readonly Type _userEnumType;

        private readonly List<EnumerationMember> _enumValues;

        public IList EnumValues
        {
            get { return _enumValues; }

        }

        private string GetDescription(object enumValue)
        {
            var descriptionAttribute = _userEnumType.GetField(enumValue.ToString())
                                           .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                           .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute != null
                       ? descriptionAttribute.Description
                       : enumValue.ToString();
        }

        public class EnumerationMember
        {
            public string Description { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Description;
            }
        }

        private ObservableCollection<ItemValue<EnumerationMember>> _operatorValues = 
            new ObservableCollection<ItemValue<EnumerationMember>>();

        public new ObservableCollection<ItemValue<EnumerationMember>> OperatorValues
        {
            get { return _operatorValues; }
            set
            {
                object old = _operatorValues;
                _operatorValues = value;
                OnPropertyChange( this, new PropertyChangeEventArgs( "OperatorValues", old, value ) );
            }
        }
        
        private EnumOperatorEnums _operatorType;
        public EnumOperatorEnums OperatorType
        {
            get { return _operatorType; }
            set
            {
                object old = _operatorType;
                _operatorType = value;
                OnPropertyChange(this, new PropertyChangeEventArgs("OperatorType", old, value));
                //OnPropertyChange(this, new PropertyChangeEventArgs("OperatorValue", OperatorValue, OperatorValue));

            }
        }

        public EnumSearchProperty( ObjectFinder rootFinder, Type userTypeEnum, string propName )
            : base(rootFinder, propName)
        {
            _operatorType = EnumOperatorEnums.Equal;

            _userEnumType = userTypeEnum;

            var enumValues = Enum.GetValues(_userEnumType);

            _enumValues = (from object enumValue in enumValues
                            select new EnumerationMember
                            {
                                Value = enumValue,
                                Description = GetDescription(enumValue)
                            }).ToList();
        }

        public override string Validate()
        {
            return (from rule in GetBrokenRules("") select rule).FirstOrDefault();
        }

        public override void GetQueryCriteria(DetachedCriteria detachedQueryCriteria, string parent)
        {
            var criteria = GetCriteria( detachedQueryCriteria, ref parent );

            switch ( OperatorType )
            {
                case EnumOperatorEnums.NotSelected:
                    break;

                case EnumOperatorEnums.Null:
                    criteria.Add( Restrictions.IsNull( PropertyName ) );
                    break;

                case EnumOperatorEnums.NotNull:
                    criteria.Add( Restrictions.Not( Restrictions.IsNull( parent + "." + PropertyName ) ) );
                    break;

                case EnumOperatorEnums.Equal:
                    EqualCriterion(OperatorValue, criteria, parent);
                    break;

                case EnumOperatorEnums.NotEqual:
                    criteria.Add(Restrictions.Or(Restrictions.IsNull(parent + "." + PropertyName), 
                                                 GetNotEqualCriterion( OperatorValue, parent ) ) );                   
                    //NotEqualCriterion(OperatorValue, criteria, alias);
                    break;

                case EnumOperatorEnums.OneOf:
                    base.OperatorValues = new ObservableCollection<ItemValue<Enum>>( GetItemValues() );
                    OneOfCriterion(criteria, parent);
                    break;

                case EnumOperatorEnums.NoneOf:
                    base.OperatorValues = new ObservableCollection<ItemValue<Enum>>( GetItemValues() );
                    NoneOfCriterion( criteria, parent );
                    break;

            }
        }

        private List<ItemValue<Enum>> GetItemValues()
        {
            var enumItems = ( from enumValue in OperatorValues select new ItemValue<Enum> { 
                Item = (Enum) enumValue.Item.Value } ).ToList();
            return enumItems;
        }

        public override IEnumerable<string> GetBrokenRules(string propName)
        {
            string error = null;
            switch (_operatorType)
            {
                case EnumOperatorEnums.NotSelected:
                    if (Equals(OperatorValue, null))
                        error = "Specificare un criterio di selezione per ricercare il valore inserito";
                    break;

                case EnumOperatorEnums.NoneOf:
                case EnumOperatorEnums.OneOf:
                    var count = (from values in OperatorValues
                                 where !Equals( values.Item, null )
                                 select values.Item).Count();

                    if (count == 0)
                        error = "Aggiungere all'elenco uno o più valori da includere nella ricerca";
                    else
                        yield return null;
                    break;

                default:
                    if (_operatorType != EnumOperatorEnums.Equal && _operatorType != EnumOperatorEnums.Null &&
                        _operatorType != EnumOperatorEnums.NotNull && Equals(OperatorValue, null) )
                    {
                        error = "Il criterio di ricerca scelto richiede un valore per essere eseguito";
                    }
                    break;
            }

            yield return error;
        }

        public override bool HasCriteria()
        {
            switch (_operatorType)
            {
                case EnumOperatorEnums.NotSelected:
                    return false;
                case EnumOperatorEnums.Null:
                case EnumOperatorEnums.NotNull:
                    return true;
                case EnumOperatorEnums.NoneOf:
                case EnumOperatorEnums.OneOf:
                    {
                        return (from values in OperatorValues
                                where !Equals( values.Item, null )
                                select values.Item).Count() != 0;
                    }
                default:
                    return !Equals(OperatorValue,null);
            }
        }

        public override void ClearSearchParamValue()
        {
            OperatorType = EnumOperatorEnums.Equal;
            OperatorValue = null;
        }

    }
}