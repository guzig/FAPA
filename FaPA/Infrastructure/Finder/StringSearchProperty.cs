using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;

namespace FaPA.Infrastructure.Finder
{
    public class StringSearchProperty : SearchProperty<string>
    {
        private StringOperatorsEnums _operatorType;
        public StringOperatorsEnums OperatorType
        {
            get { return _operatorType; }
            set
            {
                object old = _operatorType;
                _operatorType = value;
                OnPropertyChange(this, new PropertyChangeEventArgs("OperatorType", old, value));
                OnPropertyChange(this, new PropertyChangeEventArgs("OperatorValue", OperatorValue, OperatorValue));
            }
        }

        public override void ClearSearchParamValue()
        {
            OperatorValue = string.Empty;
            OperatorType = StringOperatorsEnums.Equal;
            foreach (var op in OperatorValues)
            {
                op.Item = string.Empty;
            }
        }

        public override string Validate()
        {
            return (from rule in GetBrokenRules("") select rule).FirstOrDefault();

        }

        public StringSearchProperty( string propName, ObjectFinder rootFinder )
            : base( rootFinder, propName )
        {
            _operatorType = StringOperatorsEnums.Equal;
            OperatorValues = new ObservableCollection<ItemValue<string>>();
        }

        public override IEnumerable<string> GetBrokenRules(string propName)
        {
            string error = null;

            //nessun operatore per il valore inserito
            if ((_operatorType == StringOperatorsEnums.NotSelected) &&
                !String.IsNullOrWhiteSpace(OperatorValue))
            {
                error= "Specificare un criterio di selezione per ricercare il valore inserito";
            }
            else switch (_operatorType)
            {
                case StringOperatorsEnums.OneOf:
                    var count = (from values in OperatorValues
                                 where values.Item != null
                                 select values.Item).Count();
                    if (count == 0)
                        error= "Aggiungere all'elenco uno o più valori da includere";
                    break;

                case StringOperatorsEnums.NoneOf:
                    var count1 = (from values in OperatorValues
                                 where values.Item != null
                                 select values.Item).Count();
                    if (count1 == 0)
                        error= "Aggiungere all'elenco uno o più valori da escludere";
                    break;

                default:
                    if (_operatorType != StringOperatorsEnums.Equal &&
                        _operatorType != StringOperatorsEnums.NotSelected &&
                        _operatorType != StringOperatorsEnums.Null &&
                        _operatorType != StringOperatorsEnums.NotNull &&
                        String.IsNullOrWhiteSpace(OperatorValue))
                    {
                        error = "Il criterio di ricerca scelto richiede un valore per essere eseguito";
                    }
                    break;
            }

            yield return error;
        }

        public override void GetQueryCriteria(DetachedCriteria detachedQueryCriteria, string parentPath)
        {
            var parent = parentPath;
            var propName = parent == null ? PropertyName : parent + "." + PropertyName;
            var criteria = GetCriteria(detachedQueryCriteria, ref parent);
            switch (OperatorType)
            {
                case StringOperatorsEnums.NotSelected:
                    break;

                case StringOperatorsEnums.Equal:
                    EqualCriterion(OperatorValue, criteria, parent);
                    break;
                
                case StringOperatorsEnums.NotEqual:
                    NotEqualCriterion(OperatorValue, criteria, parent);
                    break;
                
                case StringOperatorsEnums.Null:
                    criteria.Add(Restrictions.Or(Restrictions.IsNull( parent + "." + PropertyName ) , 
                        Restrictions.Eq( Projections.SqlFunction( "trim",NHibernateUtil.String,
                        Projections.Property( parent + "."+ PropertyName ) ),"")));
                    break;

                case StringOperatorsEnums.NotNull:
                    criteria.Add( Restrictions.Not( Restrictions.Or( Restrictions.IsNull( parent + "." + PropertyName ) ,
                        Restrictions.Eq(Projections.SqlFunction("trim", NHibernateUtil.String, 
                        Projections.Property( parent + "." + PropertyName ) ), ""))));
                    break;

                case StringOperatorsEnums.StartWith:
                    criteria.Add(Restrictions.Like(propName, String.Format("{0}%", OperatorValue)));
                    break;
                case StringOperatorsEnums.NotStartWith:
                    criteria.Add(Restrictions.Not(Restrictions.Like(propName, String.Format("{0}%", OperatorValue))));
                    break;

                case StringOperatorsEnums.EndWith:
                    criteria.Add(Restrictions.Like(propName, String.Format("%{0}", OperatorValue)));
                    break;
                    
                case StringOperatorsEnums.NotEndWith:
                    criteria.Add(Restrictions.Not(Restrictions.Like(propName, String.Format("%{0}", OperatorValue))));
                    break;
                    
                case StringOperatorsEnums.Contains:
                    criteria.Add(Restrictions.Like(propName, String.Format("%{0}%", OperatorValue)));
                    break;
                    
                case StringOperatorsEnums.NotContains:
                    criteria.Add(Restrictions.Not(Restrictions.Like(propName, String.Format("%{0}%", OperatorValue))));
                    break;

                case StringOperatorsEnums.OneOf:
                    OneOfCriterion(criteria, parent);
                    break;

                case StringOperatorsEnums.NoneOf:
                    NoneOfCriterion(criteria, parent);
                    break;
            }
        }

        public override bool HasCriteria()
        {
            switch (_operatorType)
            {
                case StringOperatorsEnums.NotSelected:
                    return false;
                case StringOperatorsEnums.Null:
                case StringOperatorsEnums.NotNull:
                    return true;
                case StringOperatorsEnums.NoneOf:
                case StringOperatorsEnums.OneOf:
                    {
                        return (from values in OperatorValues
                                where values.Item != null
                                select values.Item).Count() != 0;
                    }
                default:
                    return !string.IsNullOrWhiteSpace(_operatorValue);
            }
        }
    }
}