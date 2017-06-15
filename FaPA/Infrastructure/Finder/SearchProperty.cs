using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FaPA.Infrastructure.Utils;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace FaPA.Infrastructure.Finder
{
    public abstract class SearchProperty<T> : ISearchProperty, INotifyPropertyChanged, IDataErrorInfo
    {
        //costruttore
        protected SearchProperty( ObjectFinder rootFinder, string propName )
        {
            PropertyChange += PropertyHasChanged;
            RootFinder = rootFinder;
            PropertyName = propName;
        }

        #region props

        private T _operatorMinValue;

        public T OperatorMinValue
        {
            get { return _operatorMinValue; }
            set
            {
                T old = _operatorMinValue;
                _operatorMinValue = value;
                OnPropertyChange(this, new PropertyChangeEventArgs("OperatorMinValue", old, value));
                //OnPropertyChange(this, new PropertyChangeEventArgs("OperatorType", OperatorType, OperatorType));
            }
        }

        private T _operatorMaxValue;

        public T OperatorMaxValue
        {
            get { return _operatorMaxValue; }
            set
            {
                T old = _operatorMaxValue;
                _operatorMaxValue = value;
                OnPropertyChange(this, new PropertyChangeEventArgs("OperatorMaxValue", old, value));
                //OnPropertyChange(this, new PropertyChangeEventArgs("OperatorType", OperatorType, OperatorType));
            }
        }

        protected T _operatorValue;

        public T OperatorValue
        {
            get { return _operatorValue; }
            set
            {
                object old = _operatorValue;
                _operatorValue = value;
                OnPropertyChange(this, new PropertyChangeEventArgs("OperatorValue", old, value));
                //OnPropertyChange(this, new PropertyChangeEventArgs("OperatorType", OperatorType, OperatorType));
            }
        }

        private string _displayPropName;

        public string DisplayPropName
        {
            get { return _displayPropName; }
            set
            {
                var old = value;
                _displayPropName = value;
                OnPropertyChange(this, new PropertyChangeEventArgs("DisplayPropName", old, value));
            }
        }

        private bool? _isUsertType;

        protected virtual bool IsUserTypeProp
        {
            get
            {
                if (_isUsertType != null)
                    return (bool) _isUsertType;

                var propType =typeof(T);
                
                _isUsertType = propType.FullName.StartsWith(NameSpacePrefix) && propType.BaseType != null && propType.BaseType != typeof (Enum);

                return (bool) _isUsertType;
            }
        }

        public string PropertyName { get; private set; }

        private ObservableCollection<ItemValue<T>> _operatorValues = new ObservableCollection<ItemValue<T>>();

        public ObservableCollection<ItemValue<T>> OperatorValues
        {
            get { return _operatorValues; }
            set
            {
                object old = _operatorValues;
                _operatorValues = value;
                OnPropertyChange(this, new PropertyChangeEventArgs("OperatorValues", old, value));
            }
        }

        public ObjectFinder RootFinder { get; private set; }

        private JoinType _requiredJoin = JoinType.None;

        public JoinType RequiredJoin
        {
            get { return _requiredJoin; }
            set { _requiredJoin = value; }
        }

        #endregion
        
        private const string NameSpacePrefix = "Emule.Core"; 
        
        public abstract bool HasCriteria();

        public abstract void ClearSearchParamValue();

        public abstract string Validate();

        public abstract void GetQueryCriteria(DetachedCriteria detachedQueryCriteria, string parent);

        public abstract IEnumerable<string> GetBrokenRules(string propName);

        protected DetachedCriteria GetCriteria(DetachedCriteria detachedCriteria, ref string propPath)
        {
            switch ( propPath )
            {
                case "this":
                    propPath = "this";
                    break;
                case null:
                    propPath = detachedCriteria.Alias == "root" ? "root" : "this";
                    break;
                case "root":
                    propPath = "root";
                    break;
                default:
                    propPath = detachedCriteria.Alias == "this" ? "this" : propPath + "_alias";
                    break;
            }
            return detachedCriteria.GetCriteriaByAlias( propPath );
        }

        protected virtual object GetPropertyValue(object operatorValue, string propName)
        {
            return IsUserTypeProp ? ReflHelpers.GetPropertyValue( operatorValue, propName ) : operatorValue;
        }
        
        #region Criterion helpers
        protected virtual void EqualCriterion(object operatorValue, DetachedCriteria detachedCriteria, string alias)
        {
            if (Equals(operatorValue, null))
                return;

            if ( IsUserTypeProp )
            {
                if ( PropertyName == "Id" )
                    detachedCriteria.Add( Restrictions.Eq( alias + "." + PropertyName, GetPropertyValue( operatorValue, "Id" ) ) );
                else
                    detachedCriteria.Add( Restrictions.Eq( alias + "." + PropertyName, operatorValue ) );
            }
            else
                detachedCriteria.Add( Restrictions.Eq(alias + "." + PropertyName, operatorValue) );
        }

        protected virtual void NotEqualCriterion(object operatorValue, DetachedCriteria detachedCriteria, string alias)
        {
            if (!Equals(operatorValue, null))
                detachedCriteria.Add( GetNotEqualCriterion( operatorValue, alias ) );
        }

        protected AbstractCriterion GetNotEqualCriterion(object operatorValue, string alias)
        {
            return IsUserTypeProp ? Restrictions.Not(Restrictions.IdEq(GetPropertyValue(operatorValue, "Id")))
                : Restrictions.Not( Restrictions.Eq( alias + "." + PropertyName, operatorValue ) );
        }

        protected virtual void GreaterCriterion(object operatorValue, DetachedCriteria detachedCriteria, string alias)
        {
            if (!Equals(operatorValue, null))
                detachedCriteria.Add( Restrictions.Gt( alias + "." + PropertyName, 
                    GetPropertyValue( operatorValue, PropertyName ) ) );
        }

        protected virtual void GreaterOrEqual(object operatorValue, DetachedCriteria detachedCriteria, string alias)
        {
            if (!Equals(operatorValue, null))
                detachedCriteria.Add( Restrictions.Ge( alias + "." + PropertyName, GetPropertyValue( operatorValue,
                    PropertyName ) ) );
        }

        protected virtual void LessCriterion(object operatorValue, DetachedCriteria detachedCriteria, string alias)
        {
            if (!Equals(operatorValue, null))
                detachedCriteria.Add( Restrictions.Lt( alias + "." + PropertyName, GetPropertyValue( operatorValue,
                    PropertyName ) ) );
        }

        protected virtual void LessOrEqualCriterion(object operatorValue, DetachedCriteria detachedCriteria, string alias)
        {
            if (!Equals(operatorValue, null))
                detachedCriteria.Add( Restrictions.Le( alias + "." + PropertyName, GetPropertyValue( operatorValue,
                    PropertyName ) ) );
        }

        protected virtual void OneOfCriterion(DetachedCriteria detachedCriteria, string alias)
        {
            var ids = GeEntitiestIds(IsUserTypeProp ? "Id" : PropertyName);

            if (ids.Any())
                detachedCriteria.Add( Restrictions.In( alias + "." + PropertyName, ids ) );
        }

        private object[] GeEntitiestIds(string propName)
        {
            var ids = (from value in OperatorValues.Where(i => !Equals( i.Item, null ) )
                select GetPropertyValue(value.Item, propName)).
                Where(i => !Equals( i , null) ).Distinct().ToArray();
            return ids;
        }

        protected virtual void NoneOfCriterion(DetachedCriteria detachedCriteria, string alias)
        {
            var ids = GeEntitiestIds(IsUserTypeProp ? "Id" : PropertyName);

            if (ids.Any())
                detachedCriteria.Add( Restrictions.Not( Restrictions.In( alias + "." + PropertyName, ids ) ) );
        }

        #endregion

        #region INotifyPropertyChanged Members

        //event 
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        
        // Custom Delegate
        public delegate void PropertyChangeHandler(object sender, PropertyChangeEventArgs data);
        
        // The custom event
        public event PropertyChangeHandler PropertyChange;
        
        // The custom method which fires the Event
        public void OnPropertyChange(object sender, PropertyChangeEventArgs data)
        {
            // Check if there are any Subscribers
            if (PropertyChange != null)
            {
                // Call the Event
                PropertyChange(this, data);
            }
        }

        private void PropertyHasChanged(object sender, PropertyChangeEventArgs data)
        {

            OnPropertyChanged(data.PropertyName);

            //get broken rule for this property
            //var isValid = !GetBrokenRules(data.PropertyName).Where(b => !string.IsNullOrWhiteSpace(b)).ToArray().Any();

            RootFinder.Validate();
        }

        #endregion

        #region IDataErrorInfo

        /// <summary>
        /// Ottiene il messaggio di errore per la proprietà con il nome specificato.
        /// </summary>
        /// <returns>
        /// Messaggio di errore per la proprietà.Il valore predefinito è una stringa vuota ("").
        /// </returns>
        /// <param name="columnName">Nome della proprietà di cui ottenere il messaggio di errore. </param>
        public string this[string columnName]
        {
            get
            {
                var strings = GetBrokenRules(columnName).Where(e=>!string.IsNullOrWhiteSpace(e)).ToArray();
                return strings.Any() ? strings[0] : null;
            }
        }

        /// <summary>
        /// Ottiene un messaggio di errore che indica l'errore relativo a questo oggetto.
        /// </summary>
        /// <returns>
        /// Messaggio di errore che indica l'errore relativo a questo oggetto.Il valore predefinito è una stringa vuota ("").
        /// </returns>
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

    }
}