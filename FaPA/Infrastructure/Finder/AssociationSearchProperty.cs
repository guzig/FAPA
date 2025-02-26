using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FaPA.DomainServices;
using FaPA.Infrastructure.Helpers;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace FaPA.Infrastructure.Finder
{
    public class AssociationSearchProperty<T> : SearchProperty<T>
    {

        public override string Validate()
        {
            return (from rule in GetBrokenRules("") select rule).FirstOrDefault();

        }

        private static ISession _session;    
        protected static ISession Session
        {
            get
            {
                return _session ??
                       (_session = NHibernateStaticContainer.SessionFactory.OpenSession());
            }
        }

        protected ObservableCollection<T> SourceList;
        public virtual ObservableCollection<T> SourceCollection
        {
            get
            {
               if (SourceList != null)
                    return SourceList;

               SourceList = GetSourceList();

                return SourceList;                
            }

            set => SourceList = value;
        }

        protected virtual ObservableCollection<T> GetSourceList()
        {
            IList<T> collection;

            Session.ReconnectSession();

            using ( var tx = Session.BeginTransaction() )
            {
                collection = Session.CreateCriteria( typeof ( T ) )
                    .SetResultTransformer(Transformers.DistinctRootEntity)
					.List<T>();

                tx.Commit();
            }

            Session.ClearAndDisconnectSession();

            return new ObservableCollection<T>( collection );
        }

        public bool IsFreeTextEnabled { get; set; }

        private AssociationPropertyOperatorEnums _operatorType;
        public AssociationPropertyOperatorEnums OperatorType
        {
            get { return _operatorType; }
            set
            {
                object old = _operatorType;
                _operatorType = value;
                OnPropertyChange(this, new PropertyChangeEventArgs("OperatorType", old, value));
            }
        }
        
        public string DisplayMemberPath { get; set; }

        public AssociationSearchProperty(ObjectFinder rootFinder, string propName) : base(rootFinder, propName)
        {
            _operatorType = AssociationPropertyOperatorEnums.Equal;
        }

        public override void GetQueryCriteria(DetachedCriteria detachedCriteria, string parent)
        {
            var criteria = GetCriteria(detachedCriteria, ref parent);
            const string nameSpacePrefix = "Emule.Core";
            var propType = typeof( T );
            var propertyName = parent != "this" && propType.FullName.StartsWith( nameSpacePrefix ) ? "Id" : PropertyName;
            switch (OperatorType)
            {
                case AssociationPropertyOperatorEnums.NotSelected:
                   break;

                case AssociationPropertyOperatorEnums.Equal:
                   EqualCriterion(OperatorValue, criteria, parent);
                    break;

                case AssociationPropertyOperatorEnums.NotEqual:
                    NotEqualCriterion(OperatorValue, criteria, parent);
                    break;

                case AssociationPropertyOperatorEnums.Null:
                    IsNullOrEmptyCriterion( parent, criteria );
                    break;

                case AssociationPropertyOperatorEnums.NotNull:
                    IsNotNullOrEmptyCriterion( parent, criteria, propertyName );
                    break;

                case AssociationPropertyOperatorEnums.OneOf:
                    OneOfCriterion(criteria, parent);
                    break;

                case AssociationPropertyOperatorEnums.NoneOf:
                    NoneOfCriterion(criteria, parent);
                    break;
            }

        }

        protected virtual void IsNotNullOrEmptyCriterion(string alias, DetachedCriteria criteria, string propertyName)
        {
            //criteria.Add( Restrictions.Not( Restrictions.Or( Restrictions.IsEmpty( alias + "." + propertyName ) , 
            //    Restrictions.IsNull( alias + "." + propertyName ) ) ) );

            criteria.Add( Restrictions.Not( Restrictions.IsNull( alias + "." + propertyName ) ) );
        }

        protected virtual void IsNullOrEmptyCriterion(string alias, DetachedCriteria criteria)
        {
            //criteria.Add( Restrictions.Or( Restrictions.IsEmpty( alias + "." + PropertyName ) , 
            //    Restrictions.IsNull( alias + "." + PropertyName ) ) );

            criteria.Add( Restrictions.IsNull( alias + "." + PropertyName ) );
        }

        public override IEnumerable<string> GetBrokenRules(string propName)
        {
            switch (_operatorType)
            {
                case AssociationPropertyOperatorEnums.NoneOf:
                case AssociationPropertyOperatorEnums.OneOf:

                    //nessun operatore per il valore inserito

                    //nessun operatore per il valore inserito
                    if ( !MultiValuesExists() )
                    {
                        const string emptyValueMsg = "Specificare un criterio di selezione per ricercare il valore inserito";
                        yield return emptyValueMsg;
                    }
                    break;


                default:
                    if (_operatorType != AssociationPropertyOperatorEnums.Equal &&
                        _operatorType != AssociationPropertyOperatorEnums.NotSelected &&
                        _operatorType != AssociationPropertyOperatorEnums.Null &&
                        _operatorType != AssociationPropertyOperatorEnums.NotNull &&
                        !ValueExist() )
                    {
                        yield return "Specificare il/i valore/i da cercare";
                    }
                    break;
            }
        }
        
        public virtual void ManageNotInListEntries()
        {}

        public override bool HasCriteria()
        {
            switch (_operatorType)
            {
                case AssociationPropertyOperatorEnums.NotSelected:
                    return false;
                case AssociationPropertyOperatorEnums.Null:
                case AssociationPropertyOperatorEnums.NotNull:
                    return true;
                case AssociationPropertyOperatorEnums.NoneOf:
                case AssociationPropertyOperatorEnums.OneOf:
                    return MultiValuesExists();
                default:
                    return ValueExist();
            }
        }

        protected virtual bool ValueExist()
        {
            return !Equals( OperatorValue, null );
        }

        protected virtual bool MultiValuesExists()
        {
            return ( from values in OperatorValues
                where values.Item != null
                select values.Item ).Count() != 0;
        }

        public override void ClearSearchParamValue()
        {
            OperatorValue = default(T);
            OperatorType = AssociationPropertyOperatorEnums.Equal;
            foreach ( var op in OperatorValues )
            {
                op.Item = default(T);
            }
        }

    }
}