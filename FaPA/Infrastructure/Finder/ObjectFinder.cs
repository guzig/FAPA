using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using FaPA.GUI.Utils;
using FaPA.Infrastructure.Helpers;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace FaPA.Infrastructure.Finder
{
    public abstract class ObjectFinder : PropertyChangedBase
    {

        //ctor
        protected ObjectFinder(Type rootType, Action<string> callBackOnCriteria): this(rootType)
        {
            _callBackOnCriteria = callBackOnCriteria;
        }

        protected ObjectFinder(Type rootType)
        {
            RootType = rootType;

        }
        
        protected ObjectFinder(Type rootType, JoinType joinType, Action<string> callBackOnCriteria, string associationPath)
        {
            RootType = rootType;
            _associationPath = associationPath;
            _joinType = joinType;
            _callBackOnCriteria = callBackOnCriteria;
        }

        #region props

        public QueryOver QueryCriteria { protected get; set; }

        private readonly IDictionary<string, ObjectFinder> _associations = new Dictionary<string, ObjectFinder>();

        protected IDictionary<string, ObjectFinder> Associations
        {
            get { return _associations; }
        }

        private readonly JoinType _joinType = JoinType.InnerJoin;

        public JoinType JoinType
        {
            get { return _joinType; }
        }

        private bool NodeLevelHasCriteria { get; set; }

        public DetachedCriteria DetachedQueryCriteria { get; protected set; }

        private readonly IDictionary<string, ISearchProperty> _searchProperties =
            new Dictionary<string, ISearchProperty>();

        public IDictionary<string, ISearchProperty> SearchProperties
        {
            get { return _searchProperties; }
        }

        private bool _isValid = true;

        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                if (_isValid == value) return;

                _isValid = value;
                IsValidCriteria.Value = value;
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        private BaseObservable _isValidCriteria = new Observable(true);

        public BaseObservable IsValidCriteria
        {
            get { return _isValidCriteria; }
            set { _isValidCriteria = value; }
        }

        #endregion

        #region field

        protected readonly Type RootType;

        private readonly string _associationPath;

        private readonly Action<string> _callBackOnCriteria;

        public IList<string> BrokenRules = new List<string>();

        protected const string AliasPrefix = "_alias";

        #endregion

        /// <summary>
        /// search property useful with combobox etc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="searchByProp"></param>
        /// <param name="displayPropName"></param>
        /// <returns></returns>
        protected IQueryCriteria CreateSearchProperty<T, TProp>(Expression<Func<T, TProp>> searchByProp,string displayPropName )
        {
            var propName = ReflHelpers.GetNestedPropertyName(searchByProp);

            string propTypeName;

            var typeProp = ReflHelpers.GetPropertyType(searchByProp);

            if (typeProp.BaseType != null && typeProp.BaseType.Name == "Enum")
                propTypeName = "Enum";
            else if (typeProp.FullName.StartsWith("Emule.Core"))
                propTypeName = "AssociationSearchProperty";
            else
            {
                var underlyingType = Nullable.GetUnderlyingType(typeProp);
                if (underlyingType != null)
                    if (!underlyingType.FullName.StartsWith("Emule.Core"))
                        propTypeName = underlyingType.Name;
                    else
                    {
                        var baseType = Nullable.GetUnderlyingType(typeProp).BaseType;
                        propTypeName = baseType != null ? baseType.Name : typeProp.Name;
                        typeProp = underlyingType;
                    }
                else
                {
                    propTypeName =  typeProp.Name;
                }

                //propTypeName = (Nullable.GetUnderlyingType(typeProp) == null 
                //                ? typeProp
                //                : Nullable.GetUnderlyingType(typeProp).BaseType).Name;
            }
            
            var searchProp = GetSearcPropWrapper<T>(displayPropName, propTypeName, propName, typeProp);

            SearchProperties.Add(propName, searchProp);

            return searchProp;
        }


        /// <summary>
        /// search property useful with combobox etc 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="searchByProp"></param>
        /// <param name="finderClassType"></param>
        /// <param name="displayMemberPath"></param>
        /// <param name="displayPropName"></param>
        /// <returns></returns>
        protected IQueryCriteria CreateSearchProperty<T, T1, TProp>(Expression<Func<T, TProp>> searchByProp, 
            Type finderClassType, Expression<Func<T1, object>> displayMemberPath, string displayPropName)
        {
            var propName = ReflHelpers.GetNestedPropertyName(searchByProp) ?? typeof(T).Name;

            var searchProp = ( ISearchProperty ) Activator.CreateInstance( finderClassType, propName, this);

            var memberName = ReflHelpers.GetPropertyName(displayMemberPath).Split('.');

            var displayMember = memberName[memberName.Length - 1];

            var prop = finderClassType.GetProperty("DisplayMemberPath", BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(searchProp, displayMember, null);
            }

            var dispPropName = finderClassType.GetProperty("DisplayPropName", BindingFlags.Public | BindingFlags.Instance);
            if (null != dispPropName && dispPropName.CanWrite)
            {
                dispPropName.SetValue(searchProp, displayPropName, null);
            }

            SearchProperties.Add(finderClassType.Name + "." + propName, searchProp);

            return searchProp;
        }    
       
        public virtual bool CreateDetachedQuery()
        {
            return CreateDetachedQuery(null);
        }

        protected virtual bool CreateDetachedQuery(DetachedCriteria parentCriteria)
        {
            try
            {
                var hasCriteria = HasCriteria();

                DetachedQueryCriteria = parentCriteria ?? CriteriaTransformer.Clone( QueryCriteria.DetachedCriteria );

                if ( hasCriteria )
                    AddJoinAlias( DetachedQueryCriteria );
                else
                    return true;

                //create criteria for this node
                foreach ( var searchProperty in SearchProperties.Values.
                    Where(searchProperty => searchProperty.HasCriteria() ) )
                {
                    searchProperty.GetQueryCriteria(DetachedQueryCriteria, _associationPath);

                    var propName = _associationPath==null ? searchProperty.PropertyName :
                        _associationPath + "." + searchProperty.PropertyName;

                    if (_callBackOnCriteria!=null)
                        _callBackOnCriteria(propName);
                }

                //create criteria for each nested finder
                foreach ( ObjectFinder objectFinder in _associations.Values.Where(o=>o.HasCriteria()) )
                {
                    if ( !( objectFinder is CollectionFinder ) )
                    {
                        var criteria = DetachedQueryCriteria.GetCriteriaByPath(objectFinder._associationPath);
                        
                        if ( !objectFinder.CreateDetachedQuery( criteria ) ) return false;

                        continue;
                    }

                    if ( !objectFinder.CreateDetachedQuery( DetachedQueryCriteria ) ) return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                const string text = "La ricerca ha generato un errore imprevisto";
                const string caption = "Interrogazione fallita";
                MessageBox.Show(text, caption,MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
                
        }
       
        public void ClearSearchParamValues()
        {
            foreach (var prop in SearchProperties)
            {
                prop.Value.ClearSearchParamValue();
            }

            foreach (var objectFinder in _associations)
                ClearAssociations(objectFinder.Value);
        }

        private static void ClearAssociations(ObjectFinder objectFinder)
        {
            foreach (var prop in objectFinder.SearchProperties.Values)
                prop.ClearSearchParamValue();

            if (objectFinder.Associations == null || !objectFinder.Associations.Any()) return;

            foreach (var nestedFinder in objectFinder.Associations.Values)
                ClearAssociations(nestedFinder);
        }

        public void Validate()
        {
            BrokenRules = new List<string>();

            if (SearchProperties.Values.Any(searchProperty => !string.IsNullOrWhiteSpace(searchProperty.Validate())))
            {
                IsValid = false;
                return;
            }

            IsValid = true;
        }

        private ISearchProperty GetSearcPropWrapper<T>(string displayPropName, string propTypeName, 
            string propName, Type typeProp)
        {
            ISearchProperty searchProp;
            switch (propTypeName)
            {
                 //todo:refactor with reflection
                case "SByte":
                case "Byte":
                case "Int16":
                case "UInt16":
                case "Int32":
                case "UInt32":
                case "Int64":
                case "UInt64":
                    searchProp = new Int64SearchProperty( propName, this ) {
                        DisplayPropName = displayPropName
                    };
                    break;
                case "Single":
                    searchProp = new FloatSearchProperty( propName, this ){
                        DisplayPropName = displayPropName
                    };
                    break;
                case "Double":
                    searchProp = new DoubleSearchProperty( propName, this ){
                        DisplayPropName = displayPropName
                    };
                    break;
                case "Decimal":
                    //searchProp = new NumericSearchProperty<TProp>(this) { PropertyName = propName };
                    searchProp = new DoubleSearchProperty( propName, this ){
                        DisplayPropName = displayPropName
                    };
                    break;
                case "Boolean":
                    searchProp = new BooleanSearchproperty( propName, this ){
                        DisplayPropName = displayPropName
                    };
                    break;
                case "String":
                case "Char":
                    searchProp = new StringSearchProperty( propName, this ){
                        DisplayPropName = displayPropName
                    };
                    break;
                case "DateTime":
                    searchProp = new DateTimeSearchProperty( propName, this ){
                        DisplayPropName = displayPropName
                    };
                    break;
                case "Enum":
                    searchProp = new EnumSearchProperty(this, typeProp, propName){
                        DisplayPropName = displayPropName
                    };
                    break;
                case "AssociationSearchProperty":
                    searchProp = new AssociationSearchProperty<T>(this, propName){
                        DisplayPropName = displayPropName
                    };
                    break;
                default:
                    throw new Exception("tipo sconsciuto.yep");
            }
            return searchProp;
        }
        
        private void AddJoinAlias(DetachedCriteria detachedCriteria)
        {
            foreach (var searchProperty in SearchProperties.Values)
            {
                if (!searchProperty.HasCriteria()) continue;
                var searchProp = searchProperty;
                if (searchProp.RequiredJoin == JoinType.None ||
                    detachedCriteria.GetCriteriaByAlias(searchProp.PropertyName) != null) continue;
                detachedCriteria.CreateAlias(searchProp.PropertyName, searchProp.PropertyName, searchProp.RequiredJoin);
                detachedCriteria.SetFetchMode(searchProp.PropertyName, FetchMode.Eager);
                //_callBackOnJoin(searchProp.PropertyName);
            }

            //hascriteria for each associated finder
            foreach (var association in _associations)
            {
                var objectFinder = association.Value;
                var associationName = association.Key;
                if ( !objectFinder.HasCriteria() || objectFinder.JoinType == JoinType.None ) continue;
                var subcriteria = detachedCriteria.GetCriteriaByAlias( associationName + AliasPrefix );
                if ( subcriteria == null )
                {
                    detachedCriteria.CreateCriteria(associationName, associationName + AliasPrefix , objectFinder.JoinType);

                    //detachedCriteria.CreateAlias( associationName, associationName +"_alias", objectFinder.JoinType );
                    
                    if ( objectFinder.NodeLevelHasCriteria )
                    {
                        detachedCriteria.SetFetchMode( associationName, FetchMode.Join );

                        //_callBackOnJoin( associationName );
                    }

                    subcriteria = detachedCriteria.GetCriteriaByAlias( associationName + AliasPrefix );
                }

                objectFinder.AddJoinAlias(subcriteria);
            }
        }

        private bool HasCriteria()
        {
            NodeLevelHasCriteria = false;
            //if root HasCriteria
            if ( SearchProperties.Values.Any(searchProperty => searchProperty.HasCriteria()))
            {
                NodeLevelHasCriteria = true;
                return true;
            }
            //or hascriteria for each associated finder
            return _associations.Values.Any(objectFinder => objectFinder.HasCriteria());
        }
    }
}