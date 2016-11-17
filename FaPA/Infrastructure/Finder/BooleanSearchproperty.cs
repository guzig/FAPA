using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;

namespace FaPA.Infrastructure.Finder
{
    public class BooleanSearchproperty : SearchProperty<bool?>
    {
        //private bool? _operatorValue;

        //private bool? OperatorValue
        //{
        //    get { return _operatorValue; }
        //    set
        //    {
        //        object old = _operatorValue;
        //        _operatorValue = value;
        //        OnPropertyChange(this, new PropertyChangeEventArgs("OperatorValue", old, value));
        //        //OnPropertyChange(this, new PropertyChangeEventArgs("OperatorType", OperatorType, OperatorType));
        //    }
        //}

        //private ObservableCollection<ItemValue<bool>> _operatorValues=new ObservableCollection<ItemValue<bool>>();
        //public ObservableCollection<ItemValue<bool>> OperatorValues
        //{
        //    get { return _operatorValues; }
        //    set
        //    {
        //        object old = _operatorValues;
        //        _operatorValues = value;
        //        OnPropertyChange( this, new PropertyChangeEventArgs( "OperatorValues", old, value ) );
        //    }
        //}


        public override void ClearSearchParamValue()
        {
            OperatorValue = null;
            OperatorType = BoolOperatorEnums.NotSelected;
            OperatorValues.Clear();
        }


        public override string Validate()
        {
            return (from rule in GetBrokenRules("") select rule).FirstOrDefault();

        }

        public BooleanSearchproperty( string propName, ObjectFinder rootFinder )
            : base( rootFinder, propName )
        {
            _operatorType=BoolOperatorEnums.NotSelected;
        }

        private BoolOperatorEnums _operatorType;
        public BoolOperatorEnums OperatorType
        {
            get { return _operatorType; }
            set
            {
                OnPropertyChanged("OperatorType");
            }
        }

        public override void GetQueryCriteria(DetachedCriteria detachedQueryCriteria, string parent)
        {
            switch (OperatorType)
            {
                case BoolOperatorEnums.NotSelected:
                    break;
                default:
                    throw new Exception("operatore non implementato");

            }
        }
        //todo:controllo true/false
        //todo:elenco no: se vuoto filtra vero e false
        public override IEnumerable<string> GetBrokenRules(string propName)
        {

            if (_operatorType.ToString() == "0")
                if (!Equals(OperatorValue, null))
                    yield return "Specificare un criterio di selezione con il quale ricercare il valore inserito...";

            //if (criterio != BoolOperatorEnums.NotNull && criterio != BoolOperatorEnums.Null && Equals(valore, default(TVal)))
            //yield return "Inserire un valore numerico su cui eseguire il criterio di ricerca selezionato...";

        }

        public override bool HasCriteria()
        {
            switch (_operatorType)
            {
                case BoolOperatorEnums.NotSelected:
                    return false;
                default:
                    return true;
            }
        }
    }
}