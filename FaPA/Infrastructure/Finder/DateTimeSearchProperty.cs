using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NHibernate.Criterion;

namespace FaPA.Infrastructure.Finder
{
    public class DateTimeSearchProperty : SearchProperty<DateTime?>
    {
       
        public override string Validate()
        {
            return (from rule in GetBrokenRules("") select rule).FirstOrDefault();
        }

        private DateTimeOperatorEnums _operatorType;

        public DateTimeOperatorEnums OperatorType
        {
            get { return _operatorType; }
            set
            {
                object old = _operatorType;
                _operatorType = value;
                OnPropertyChange(this, new PropertyChangeEventArgs("OperatorType", old, value));
                //OnPropertyChanged("OperatorValue");

            }
        }

        public DateTimeSearchProperty( string propName, ObjectFinder rootFinder )
            : base( rootFinder, propName )
        {
            _operatorType = DateTimeOperatorEnums.Equal;
            OperatorValues= new ObservableCollection<ItemValue<DateTime?>>();
            //_operatorValues.CollectionChanged += OnCollectionChanged;
            OperatorMinValue = DateTime.Now.AddDays(-1);
            OperatorMaxValue = DateTime.Now;
        }

        public override IEnumerable<string> GetBrokenRules(string propName)
        {
            switch (_operatorType)
            {
                case DateTimeOperatorEnums.Equal:
                case DateTimeOperatorEnums.NotSelected:
                case DateTimeOperatorEnums.Between:
                case DateTimeOperatorEnums.NotBetween:
                    yield return null;
                    break;
                
                case DateTimeOperatorEnums.OneOf:
                case DateTimeOperatorEnums.NoneOf:
                    if (OperatorValues.Count == 0)
                        yield return "Specificare una o più date";
                    break;

                case DateTimeOperatorEnums.NotEqual:
                case DateTimeOperatorEnums.Greater: 
                case DateTimeOperatorEnums.GreaterOrEqual:
                case DateTimeOperatorEnums.Less:
                case DateTimeOperatorEnums.LessOrEqual:
                case DateTimeOperatorEnums.NotLess:
                case DateTimeOperatorEnums.NotGreater:
                    if (OperatorValue==null)
                        yield return "Specificare una data da confrontare";
                    break;

                default:
                    yield return null;
                    break;
            }


        }

        public override void GetQueryCriteria(DetachedCriteria detachedQueryCriteria, string parent)
        {
            var criteria = GetCriteria(detachedQueryCriteria, ref parent);
            switch (OperatorType)
            {
                case DateTimeOperatorEnums.Null:
                    criteria.Add( Restrictions.IsNull( PropertyName ) );
                    break;

                case DateTimeOperatorEnums.NotNull:
                    criteria.Add( Restrictions.Not( Restrictions.IsNull( PropertyName ) ) );
                    break;

                case DateTimeOperatorEnums.Equal:
                    if ( OperatorValue.HasValue )
                        criteria.Add( Restrictions.Eq( PropertyName, OperatorValue ) );
                    break;

                case DateTimeOperatorEnums.NotEqual:
                    if( OperatorValue.HasValue )
                        criteria.Add( Restrictions.Not( Restrictions.Eq( PropertyName, OperatorValue ) ) );
                    break;

                case DateTimeOperatorEnums.Greater:
                    if ( OperatorValue.HasValue )
                        criteria.Add( Restrictions.Gt( PropertyName, OperatorValue ) );
                    break;

                case DateTimeOperatorEnums.NotGreater:
                    if ( OperatorValue.HasValue )
                        criteria.Add( Restrictions.Not( Restrictions.Gt( PropertyName, OperatorValue ) ) );
                    break;

                case DateTimeOperatorEnums.GreaterOrEqual:
                    if ( OperatorValue.HasValue )
                        criteria.Add( Restrictions.Ge( PropertyName, OperatorValue ) );
                    break;

                case DateTimeOperatorEnums.Less:
                    if ( OperatorValue.HasValue )
                        criteria.Add( Restrictions.Lt( PropertyName, OperatorValue ) );
                    break;

                case DateTimeOperatorEnums.NotLess:
                    if ( OperatorValue.HasValue )
                        criteria.Add( Restrictions.Not( Restrictions.Lt( PropertyName, OperatorValue ) ) );
                    break;

                case DateTimeOperatorEnums.LessOrEqual:
                    if ( OperatorValue.HasValue )
                        criteria.Add( Restrictions.Le( PropertyName, OperatorValue ) );
                    break;

                case DateTimeOperatorEnums.OneOf:
                    var values = (from value in OperatorValues select value.Item).ToArray();
                    if (values.Any())
                        criteria.Add( Restrictions.In( PropertyName, values ) );
                    break;

                case DateTimeOperatorEnums.NoneOf:
                    var noneOfValues = (from value in OperatorValues select value.Item).ToArray();
                    if (noneOfValues.Any())
                        criteria.Add( Restrictions.Not( Restrictions.In( PropertyName, noneOfValues ) ) );
                    break;

                case DateTimeOperatorEnums.Between:
                    if ( OperatorMinValue.HasValue && OperatorMaxValue.HasValue )
                    {
                        criteria.Add( Restrictions.And( Restrictions.Ge( PropertyName , OperatorMinValue ) ,
                            Restrictions.Le( PropertyName , OperatorMaxValue ) ) );
                    }
                    break;

                case DateTimeOperatorEnums.NotBetween:
                    if ( OperatorMinValue.HasValue && OperatorMaxValue.HasValue )
                    {
                        criteria.Add( Restrictions.Not( Restrictions.And( Restrictions.Ge( PropertyName, OperatorMinValue ),
                            Restrictions.Le( PropertyName, OperatorMaxValue ) ) ) );
                    }
                    break;

            }

        }

        public string ValidateRange()
        {
            return OperatorMaxValue <= OperatorMinValue
                       ? "Digitare un intervallo temporale valido (massimo > minimo)"
                       : null;
        }

        public override bool HasCriteria()
        {
            switch (_operatorType)
            {
                case DateTimeOperatorEnums.NotSelected:
                    return false;
                case DateTimeOperatorEnums.Null:
                case DateTimeOperatorEnums.NotNull:
                    return true;
                case DateTimeOperatorEnums.NoneOf:
                case DateTimeOperatorEnums.OneOf:
                    {
                        return (from values in OperatorValues
                                where values.Item != null
                                select values.Item).Count() != 0;
                    }
                case DateTimeOperatorEnums.Between:
                case DateTimeOperatorEnums.NotBetween:
                    {
                        return OperatorMaxValue > OperatorMinValue;
                    }
                default:
                    return !Equals(OperatorValue, null) && !Equals(OperatorValue.Value, null);
            }
        }

        public override void ClearSearchParamValue()
        {
            OperatorValue = null;
            OperatorType = DateTimeOperatorEnums.Equal;
            OperatorValues.Clear();
        }


    }
}