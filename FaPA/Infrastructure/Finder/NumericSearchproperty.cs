using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;

namespace FaPA.Infrastructure.Finder
{   
    public abstract class NumericSearchProperty<T> : SearchProperty<T>, INumsSearchProperty
    {
        protected abstract string ValidateRange();
        
        public override string Validate()
        {
            return (from rule in GetBrokenRules("") select rule).FirstOrDefault();

        }

        protected NumericSearchProperty( ObjectFinder rootFinder, string propName )
            : base( rootFinder, propName )
        {
            _operatorType=NumOperatorEnums.Equal;
        }
        
        private NumOperatorEnums _operatorType;
        
        public NumOperatorEnums OperatorType
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

        public override void GetQueryCriteria(DetachedCriteria detachedQueryCriteria, string parent)
        {
            var criteria = GetCriteria(detachedQueryCriteria, ref parent);
            var propName = parent + "." + PropertyName;
            switch (OperatorType)
            {
                case NumOperatorEnums.NotSelected:
                    break;

                case NumOperatorEnums.Null:
                    criteria.Add(Restrictions.IsNull(propName));
                    break;
                
                case NumOperatorEnums.NotNull:
                    criteria.Add(Restrictions.Not(Restrictions.IsNull(propName)));
                    break;

                case NumOperatorEnums.Equal:
                    EqualCriterion(OperatorValue, criteria, parent);
                    break;
                
                case NumOperatorEnums.NotEqual:
                    NotEqualCriterion(OperatorValue, criteria, parent);
                    break;
                
                case NumOperatorEnums.Greater:
                    GreaterCriterion(OperatorValue, criteria, parent);
                    break;
                
                case NumOperatorEnums.GreaterOrEqual:
                    GreaterOrEqual(OperatorValue, criteria, parent);
                    break;

                case NumOperatorEnums.Less:
                    LessCriterion(OperatorValue, criteria, parent);
                    break;

                case NumOperatorEnums.LessOrEqual:
                    LessOrEqualCriterion(OperatorValue, criteria, parent);
                    break;

                case NumOperatorEnums.OneOf:
                    OneOfCriterion(criteria, parent);
                    break;

                case NumOperatorEnums.NoneOf:
                    NoneOfCriterion(criteria, parent);
                    break;

                case NumOperatorEnums.Between:
                    criteria.Add(Restrictions.And(Restrictions.Ge(propName, OperatorMinValue), Restrictions.Le(propName, OperatorMaxValue)));
                    break;

                case NumOperatorEnums.NotBetween:
                    criteria.Add(Restrictions.Not(Restrictions.And(Restrictions.Ge(propName, OperatorMinValue), Restrictions.Le(propName, OperatorMaxValue))));
                    break;

            }
        }

        public override IEnumerable<string> GetBrokenRules( string propName )
        {
            string error = null;
            switch ( _operatorType )
            {
                case NumOperatorEnums.NotSelected:
                    if ( Equals ( OperatorValue, null ) )
                    {
                        const string criErrMsg = "Specificare un criterio di selezione per ricercare il valore inserito";
                        error = criErrMsg;
                    }
                    break;

                case NumOperatorEnums.NoneOf:
                case NumOperatorEnums.OneOf:
                    if ( OperatorValues.All(v => Equals( v.Item, null) ) )
                    {
                        const string valErrMsg = "Aggiungere all'elenco uno o più valori da includere nella ricerca";
                        error = valErrMsg;
                    }
                    else
                        yield return null;
                    break;
                case NumOperatorEnums.Between:
                case NumOperatorEnums.NotBetween:
                    error = ValidateRange ();
                    break;

                default:
                    if ( _operatorType != NumOperatorEnums.Equal && _operatorType != NumOperatorEnums.Null &&
                         _operatorType != NumOperatorEnums.NotNull && Equals ( OperatorValue, null ) )
                    {
                        const string valRequired = "Il criterio di ricerca scelto richiede un valore per essere eseguito";
                        error = valRequired;
                    }
                    break;
            }

            yield return error;
        }

        public override void ClearSearchParamValue()
        {
            OperatorValue = default(T);
            OperatorType = NumOperatorEnums.Equal;
            foreach ( var op in OperatorValues )
            {
                op.Item = default(T);
            }
        }

        public override bool HasCriteria()
        {
            switch ( OperatorType )
            {
                case NumOperatorEnums.NotSelected:
                    return false;
                case NumOperatorEnums.Null:
                case NumOperatorEnums.NotNull:
                    return true;
                case NumOperatorEnums.NoneOf:
                case NumOperatorEnums.OneOf:
                    {
                        return ( from values in OperatorValues
                                 where !Equals( values.Item, null )
                                 select values.Item ).Count() != 0;
                        //return OperatorValues.Count != 0;
                    }
                case NumOperatorEnums.Between:
                case NumOperatorEnums.NotBetween:
                    return true;
                default:
                    return !Equals( OperatorValue, null );
            }
        }

    }

}