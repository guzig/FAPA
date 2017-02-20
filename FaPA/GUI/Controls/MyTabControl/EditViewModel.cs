using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using FaPA.Core;
using FaPA.Data;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;
using FaPA.Infrastructure.Helpers;
using NHibernate;
using NHibernate.Criterion;
using Action = System.Action;

namespace FaPA.GUI.Controls.MyTabControl
{
    public abstract class EditViewModel<T> :  ListViewViewModel, IEditViewModel, 
        IRepository, IDispose where T : BaseEntity
    {
        #region fields

        private readonly BackgroundWorker _searchBackgroundWorker;

        private bool _isOnBind;

        private static readonly NhProxyInspector ProxyInspector = new NhProxyInspector();

        private ISession _session;
        private IStatelessSession _statelessSession;

        #endregion

        public virtual string EditTemplateName { get; }

        public IBasePresenter BasePresenter { get; }

        public delegate void OnCurrentChangedhandler(T currententity);

        public event OnCurrentChangedhandler CurrentEntityChanged;

        public delegate void OnCurrentPropChangedhandler(T currentEntity, PropertyChangedEventArgs eventArgs);

        public event OnCurrentPropChangedhandler CurrentEntityPropChanged;

        //public event OnCurrentChangedhandler CurrentChanging;

        private void OnCurrentChanged( T sender )
        {
            var handler = CurrentEntityChanged;
            handler?.Invoke( sender );
        }

        private void OnCurrentPropChanged(T sender, PropertyChangedEventArgs eventArgs)
        {
            var handler = CurrentEntityPropChanged;
            handler?.Invoke(sender, eventArgs);
        }

        //void OnCurrentChanging(T sender)
        //{
        //    var handler = CurrentChanging;
        //    if (handler != null)
        //        handler(sender);
        //}
       
        #region props
        private bool _isValid=true;
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                if ( value == _isValid ) return;
                _isValid = value;
                NotifyOfPropertyChange( () => IsValid );
            }
        }

        private bool _isInEditing;
        public bool IsInEditing
        {
            get { return _isInEditing; }
            set
            {
                if ( value.Equals( _isInEditing ) ) return;
                _isInEditing = value;
                NotifyOfPropertyChange( () => IsInEditing );
            }
        }

        private T _currentEntity;
        public T CurrentEntity
        {
            get { return _currentEntity; }
            protected set
            {
                _currentEntity = value;
                NotifyOfPropertyChange( () => CurrentEntity );
            }
        }

        private IList _userCollection;
        public IList UserCollection
        {
            get { return _userCollection; }
            protected set
            {
                if ( Equals( value, _userCollection ) ) return;
                _userCollection = value;
                NotifyOfPropertyChange( () => UserCollection );
            }
        }

        private bool _isSearchModality;
        public bool IsSearchModality
        {
            get { return _isSearchModality; }
            set
            {
                _isSearchModality = value;
                NotifyOfPropertyChange(() => IsSearchModality);
            }
        }

        public const string OnEditingLockMessage = "Salvare o annullare le modifiche apportate prima di chiudere la scheda.";
        private const string OnFastSearchLockMessage = "Annullare la ricerca in corso, prima di chiudere la scheda .";
        
        private bool _allowPrint = true;
        public bool AllowPrint
        {
            get { return _allowPrint; }
            set
            {
                if (value.Equals(_allowPrint)) return;
                _allowPrint = value;
                NotifyOfPropertyChange(() => AllowPrint);
            }
        }

        private bool _allowFastSearch = true;
        public bool AllowFastSearch
        {
            get { return _allowFastSearch; }
            set
            {
                if (value.Equals(_allowFastSearch)) return;
                _allowFastSearch = value;
                NotifyOfPropertyChange(() => AllowFastSearch);
            }
        }

        private bool _allowEditing=true;
        public virtual bool AllowEditing
        {
            get { return _allowEditing; }
            private set
            {
                if (value.Equals(_allowEditing)) return;
                _allowEditing = value;
                NotifyOfPropertyChange(() => AllowEditing);
            }
        }
        
        private bool _allowSave;
        public virtual bool AllowSave
        {
            get { return _allowSave; }
            set
            {
                if (value.Equals(_allowSave)) return;
                _allowSave = value;
                NotifyOfPropertyChange(() => AllowSave);
            }
        }
        
        private bool _allowRecordsNavigation;
        public bool AllowRecordsNavigation
        {
            get { return _allowRecordsNavigation; }
            set
            {
                if (value.Equals(_allowRecordsNavigation)) return;
                _allowRecordsNavigation = value;
                NotifyOfPropertyChange(() => AllowRecordsNavigation);
            }
        }
        
        private bool _allowDelete;
        public virtual bool AllowDelete
        {
            get { return _allowDelete; }
            set
            {
                _allowDelete = value;
                NotifyOfPropertyChange(() => AllowDelete);
            }
        }

        private bool _allowInsertNewEntity=true;
        public virtual bool AllowInsertNewEntity
        {
            get { return _allowInsertNewEntity; }
            set
            {
                if (value.Equals(_allowInsertNewEntity)) return;
                _allowInsertNewEntity = value;
                NotifyOfPropertyChange(() => AllowInsertNewEntity);
            }
        }
        
        private bool _isIncrementalSearch;
        public bool IsIncrementalSearch
        {
            get { return _isIncrementalSearch; }
            set
            {
                if (value.Equals(_isIncrementalSearch)) return;
                _isIncrementalSearch = value;
                NotifyOfPropertyChange(() => IsIncrementalSearch);
            }
        }      
        
        #endregion 

        #region Constructor
        protected EditViewModel(IBasePresenter baseCrudPresenter, IList userEntities, ICollectionView userCollectionView)
        {
            BasePresenter = baseCrudPresenter;

            IsSearchModality = false;

            _searchBackgroundWorker = new BackgroundWorker();
            _searchBackgroundWorker.DoWork += SearchOnSitePerform;
            _searchBackgroundWorker.RunWorkerCompleted += SearchOnSiteCompleted;

            IsCloseable = false;

            SetUpCollectionView(userEntities, userCollectionView);
        }
        #endregion

        private void SetUpCollectionView(IList usercollection, ICollectionView listView)
        {
            if (UserEntitiesView != null)
            {
                UserEntitiesView.CurrentChanged -= OnCurrentSelectionChanged;
                RequestClose -= OnClose;
            }

            UserCollection = usercollection;
            UserEntitiesView = listView;
            UserEntitiesView.MoveCurrentToFirst();
            UserEntitiesView.CurrentChanged += OnCurrentSelectionChanged;
            RequestClose += OnClose;
        }

        protected void SetUpSession(ISession session, IStatelessSession statelessSession )
        {
            _session = session;
            _session.FlushMode=FlushMode.Never;
            _statelessSession = statelessSession;
        }
        
        private void OnPropChanged(object sender, PropertyChangedEventArgs argts)
        {
            _onCancelDelegate = DefaultCancelOnEditAction;

            if (_isOnBind )
            {
                OnCurrentPropChanged((T)sender, argts);
                return;
            }            

            AllowSave = CurrentEntity.DomainResult.Success;
            LockMessage = OnEditingLockMessage;

            if ( IsInEditing )
            {
                OnCurrentPropChanged((T)sender, argts);
                return;
            }

            IsInEditing = true;
            AllowRecordsNavigation = false;
            AllowFastSearch = false;
            AllowDelete = false;
            AllowInsertNewEntity = false;

            OnCurrentPropChanged((T)sender, argts);
        }
             
        private void OnCurrentSelectionChanged(object sender, EventArgs args)
        {
            //OnCurrentChanging(CurrentEntity);
            if (BasePresenter.GetActiveWorkSpace() is EditViewModel<T>)
            {
                if ( !IsNewEntity( CurrentEntity ) )
                    LoadAndShowCurrentEntity();
            }
            
        }
        
        #region Validation stuff

        private readonly HashSet<string> _entityLevelPropsValidation = new HashSet<string>();

        public IEditViewModel AddEntityLevelPropValidation<TEntity, TProp>( Expression<Func<TEntity, TProp>> property)
        {
            var propertyName = ReflHelpers.GetPropertyName(property);

            _entityLevelPropsValidation.Add(propertyName);

            return this;
        }

        protected virtual bool IsValidEntity()
        {
            IsValid =  HibHelpers.Validator.IsValid( CurrentEntity );
            return IsValid;
        }

        protected virtual string Validate()
        {
            if ( IsValidEntity() ) return null;

            var errors = HibHelpers.Validator.Validate(CurrentEntity);

            var listError = new List<string> {"ELENCO ERRORI RILEVATI:"};

            var enumerable = errors.Where(e=>!string.IsNullOrWhiteSpace(e.Message)).
                Select(e => e.PropertyName.Replace("DB","").ToUpper() + ": " + e.Message).ToList();

            listError.AddRange(enumerable);

            return string.Join(Environment.NewLine, listError);
        }

        #endregion
        
        protected virtual void SetContextAfterEntityDeleted()
        {          
            var currentPos = UserEntitiesView.CurrentPosition;
            UserCollection.Remove(CurrentEntity);
            var listCount = (UserEntitiesView as ListCollectionView).Count;

            if (listCount <= 0)
            {
                UserEntitiesView.CurrentChanged -= OnCurrentSelectionChanged;
                //BasePresenter.SetActiveWorkSpace(0);
                //BasePresenter.Workspaces.RemoveAt(1);
                return;
            }

            UserEntitiesView.MoveCurrentToPosition(currentPos == 0 ? 0 : currentPos - 1);

            BindCurrent();
        }

        protected virtual void SetContextAfterBindEntity()
        {
            LockMessage = null;
            var listCollectionView = UserEntitiesView as ListCollectionView;
            AllowRecordsNavigation = true;
            AllowInsertNewEntity = CanAddNewEntity( CurrentEntity );
            AllowDelete = CanDeleteEntity( CurrentEntity );
            AllowFastSearch = true;
            AllowRecordsNavigation = listCollectionView != null && listCollectionView.Count > 1;
            IsInEditing = false;
            IsSearchModality = false;
        }

        protected virtual T LoadEntity(long id)
        {
            try
            {
                T instance;
                using (var tx = _session.BeginTransaction())
                {
                    _session.Evict( _session.Get<T>( id ) );
                    instance = _session.Get<T>( id );
                    tx.Commit();
                }
                return instance;
            }
            catch ( Exception e )
            {
                Debug.WriteLine( e.Message );
                throw new Exception();
            }
        }
                
        #region Public Methods   
     
        public virtual void Persist()
        {
            AllowSave = false;
            var isNewEntityAdded = IsNewEntity( _currentEntity );
            _isOnBind = true;

            if ( !TryPersistEntity() )
            {
                _isOnBind = false;
                return;
            }

            Load();

            if (isNewEntityAdded)
            {
                DisplayName = DisplayName.Replace("Crea","Dettaglio");
                BasePresenter.RefreshSharedViewsAfterAddedNew( CurrentEntity );
                PublishAddedNewEntityEvent(CurrentEntity);
                LoadAndShowCurrentEntity();
                SetContextAfterBindEntity();
            }
            else
            {
                PublishUpdateEntityEvent(CurrentEntity); // -> LoadAndShowCurrent
                BasePresenter.RefreshSharedViewsAfterUpdated( CurrentEntity );
            }

            _isOnBind = false;
            IsInEditing = false;
        }

        public bool TryPersistEntity(  )
        {
            var errors = Validate();

            if (!string.IsNullOrEmpty(errors))
            {
                MessageBox.Show(errors, "Validazione fallita, salvataggio non consentito...", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }

            if ( TrySaveCurrentEntity() ) return true;
            WpfHelpers.ShowErrorSavingEntityMsg();
            return false;
        }

        protected virtual bool TrySaveCurrentEntity()
        {
            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    _session.Clear();
                    //var typeName = ProxyInspector.GuessType( CurrentEntity ).FullName;
                    //_session.SaveOrUpdate(typeName, CurrentEntity.Unproxy());
                    _session.SaveOrUpdate(CurrentEntity.Unproxy());
                    _session.Flush();
                    tx.Commit();
                    return true;
                }
                catch ( Exception e )
                {
                    Debug.Print( e.Message );
                    tx.Rollback();
                    _session.Clear();
                    return false;
                }
            }
        }

        public virtual void MakeTransient()
        {
            if (MessageBox.Show("Conferma eliminazione?", "Conferma", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return ;

            ShowCursor.Show();

            if ( !DeleteEntity() ) return ;

            SetContextAfterEntityDeleted();

            PublishDeletedEntityEvent( CurrentEntity );

            BasePresenter.RefreshSharedViewsAfterDelete( CurrentEntity );
            
        }

        public bool DeleteEntity()
        {
            try
            {
                using ( var tx = _session.BeginTransaction() )
                {
                    var typeName = ProxyInspector.GuessType( CurrentEntity ).FullName;
                    var entity = _session.Get<T>( CurrentEntity.Id );
                    _session.Delete( typeName, entity );
                    _session.Flush();
                    tx.Commit();
                }
            }
            catch ( Exception e)
            {
                Debug.WriteLine( e.Message );
                WpfHelpers.ShowErrorDeletingMsg();
                return false;
            }
            return true;
        }

        public virtual void CreateNewEntity()
        {
            try
            {
                CurrentEntity = ( T ) AddPropChangedAndDataErrorInterceptorProxyFactory.Create( typeof( T ), CreateInstance() );
            }
            catch (Exception e)
            {
                Debug.WriteLine( e.Message );
                const string msgFormat = "L'istanza di tipo {0} non puo' essere creata. {1} E' possibile eseguire l'override del metodo <CreateNewEntity()> nel Presenter per facilitare la creazione, ad es. in assenza di costruttori di default.";
                throw new Exception(string.Format(msgFormat, typeof (T).FullName, Environment.NewLine));
            }
        }

        public virtual void AddNew()
        {
            _onCancelDelegate = DefaultCancelOnEditAction;

            IsInEditing = true;
            //IsNewEntity = true;
            CreateNewEntity();
            CurrentEntity.IsValidating = true;
            DecorateEntity();

            //UserEntitiesView.CurrentChanged -= OnCurrentSelectionChanged;

            DisplayName = DisplayName.Replace("Crea", "Dettaglio");
            UserCollection.Add(CurrentEntity);
            UserEntitiesView.MoveCurrentToLast();

            //UserEntitiesView.CurrentChanged += OnCurrentSelectionChanged;
            //UserEntitiesView.Refresh();

            ValidateAndShow( CurrentEntity );

            AllowSave = CurrentEntity == null || CurrentEntity.DomainResult.Success;
            IsSearchModality = false;
            DisplayName = DisplayName.Replace("Dettaglio","Crea");
            LockMessage = OnEditingLockMessage;
            AllowFastSearch = false;
            AllowInsertNewEntity = false;
            AllowRecordsNavigation = false;
            AllowDelete = false;
        }

        private static void ValidateAndShow( IValidatable validtor )
        {
            if ( validtor == null ) return;
            validtor.Validate();
            validtor.HandleValidationResults();
        }

        protected virtual T CreateInstance()
        {
            T entity;

            if ( CurrentEntity == null )
                entity = Activator.CreateInstance<T>();
            else
                entity = (T) Activator.CreateInstance( CurrentEntity.NhUnproxyType() );

            return entity;
        }


        public abstract void PublishAddedNewEntityEvent(BaseEntity dto);

        public abstract void PublishUpdateEntityEvent(BaseEntity dto);

        public abstract void PublishDeletedEntityEvent( BaseEntity dto );

        public virtual void OnPageGotFocus()
        {
            BindCurrent();
            SetContextAfterBindEntity();
        }

        public void LoadAndShowCurrentEntity()
        {
            BindCurrent();
            SetContextAfterBindEntity();
        }

        private void BindCurrent()
        {
            _isOnBind = true;

            IsInEditing = false;

            Load();

            _isOnBind = false;

            _onCancelDelegate = DefaultCancelOnEditAction;

            OnCurrentChanged( CurrentEntity );
        }

        public virtual void Load()
        {
            if (UserEntitiesView.CurrentItem == null) return;

            var currentItem = (BaseEntity) UserCollection[UserEntitiesView.CurrentPosition];

            if ( !UserEntitiesView.IsEmpty )
            {
                //we need to unproxy entity to get the Id;
                if ( TryGetUnproxiedEntity() )
                    currentItem = (BaseEntity)UserCollection[UserEntitiesView.CurrentPosition]; 
                else
                    throw new Exception("fail to unproxied entities");
            }

            object current = LoadEntity(currentItem.Id);

            ( ( IValidatable ) current).HandleValidationResults();
            ( ( BaseEntity ) current).IsValidating = true;


            var notifyPropertyChanged = current as INotifyPropertyChanged;
            if (notifyPropertyChanged == null) return;
            notifyPropertyChanged.PropertyChanged -= OnPropChanged;
            notifyPropertyChanged.PropertyChanged += OnPropChanged;

            CurrentEntity = (T) current;

            DecorateEntity();

        }

        #endregion // Public Methods

        protected virtual void DecorateEntity()
        {
            //CurrentEntity.OnDataErrorInfo += OnDataErrorInfo;

            var notifyPropertyChanged = CurrentEntity as INotifyPropertyChanged;
            if ( notifyPropertyChanged != null )
                notifyPropertyChanged.PropertyChanged += OnPropChanged;
        }

        private void ReplaceSessionAfterError()
        {
            if (_session != null)
            {
                _session.Clear();
                //ReplaceEntitiesLoadedByFaultedSession();
            }
        }

        private void EnterKeyHandler()
        {
            if (IsSearchModality)
            {
                _searchBackgroundWorker.RunWorkerAsync();
            }
        }
       
        private void FreeLock(string lockMessage)
        {
            if (LockMessage!=null && LockMessage.Equals(lockMessage))
                LockMessage = "";
        }

        private static bool IsNewEntity(T entity)
        {
            return entity == null || entity.Id == 0L;
        }

        #region Cancel stuff

        private bool CancelEditCanExecuted()
        {
            return IsNewEntity(CurrentEntity) || IsInEditing || _isSearchModality;
        }

        private delegate void OnCancelAction();

        private OnCancelAction _onCancelDelegate = delegate { };

        private void CancelOnFastSearchAction()
        {
            AllowFastSearch = true;
            FreeLock(OnFastSearchLockMessage);
            BasePresenter.SetActiveWorkSpace(0);
        }

        protected virtual void DefaultCancelOnEditAction()
        {
           _session.Clear();
            
            if (IsNewEntity(CurrentEntity))
            {
                UserCollection.Remove(UserEntitiesView.CurrentItem);
            }
            
            FreeLock(OnEditingLockMessage);
            
            if (UserEntitiesView == null || UserEntitiesView.CurrentItem == null ||
                ((UserEntitiesView.CurrentItem as BaseEntity).Id == 0L && 
                (UserEntitiesView as ListCollectionView).Count == 0))
            {
                BasePresenter.SetActiveWorkSpace(0);
                BasePresenter.Workspaces.RemoveAt(1);
            }
            else
                LoadAndShowCurrentEntity();
        }

        #endregion

        #region Search on site stuff

        private DetachedCriteria _queryByExample;

        private void SwitchToSearchOnSiteMode()
        {
            _onCancelDelegate = CancelOnFastSearchAction;
            CurrentEntity = CreateInstance();
            CurrentEntity.IsValidating = false;
            //CurrentDtoEntity = CreateDtoInstance();
            DisplayName = DisplayName.Replace("Schede", "Cerca");
            AllowFastSearch = false;
            AllowSave = false;
            AllowDelete = false;
            AllowRecordsNavigation = false;
            LockMessage = OnFastSearchLockMessage;
            IsSearchModality = true;
            IsInEditing = true;
        }

        private void SearchOnSitePerform(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            //MapFromDto(CurrentDtoEntity, ref _currentEntity);
            doWorkEventArgs.Result = GetQueryByExample(_currentEntity);
        }

        private void SearchOnSiteCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            //restore default behavoir
            _onCancelDelegate = DefaultCancelOnEditAction;

            var count = runWorkerCompletedEventArgs.Result is int
                ? (int) runWorkerCompletedEventArgs.Result
                : 0;

            if (count > 0 && _queryByExample != null)
            {
                UserEntitiesView.CurrentChanged -= OnCurrentSelectionChanged;
                BasePresenter.CreateNewModel(_queryByExample);
            }

            FreeLock(OnFastSearchLockMessage);
            LoadAndShowCurrentEntity();
            CurrentEntity.IsValidating = true;
            //IsSearchModality = false;

        }

        private int GetQueryByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            _queryByExample = null;
            try
            {
                _queryByExample = DetachedCriteria.For<T>();
                var idValue = ReflHelpers.GetPropertyValue(
                    //typeof( T ).GetProperty( "Id" ).GetType(), 
                    exampleInstance, "Id");

                if (idValue is long && (long) idValue > 0)
                {
                    _queryByExample.Add(Restrictions.IdEq(idValue));
                }
                else
                {
                    var example = Example.Create(exampleInstance).ExcludeNulls().ExcludeZeroes();
                    foreach (PropertyInfo info in typeof (T).GetProperties())
                    {
                        var propName = info.Name;
                        var association = ReflHelpers.GetPropertyValue(
                            //info.PropertyType, 
                            exampleInstance, propName);
                        //add criteria for association type
                        if (association is BaseEntity && ! ( association is BaseEntityFpa ) )
                        {
                            //detachedCriteria.SetFetchMode(propName, FetchMode.Eager);
                            _queryByExample.Add(Restrictions.Eq(propName, association));

                        } //esclude default value for date type
                        else if (info.PropertyType == typeof (DateTime))
                        {
                            var dt = (DateTime) info.GetValue(exampleInstance, info.GetIndexParameters());
                            if (dt <= DateTime.MinValue)
                                example.ExcludeProperty(info.Name);
                        }
                    }
                    foreach (string propertyToExclude in propertiesToExclude)
                    {
                        example.ExcludeProperty(propertyToExclude);
                    }
                    _queryByExample.Add(example);
                }

                //if (SetAssociationCriteria != null)
                //SetAssociationCriteria(criteria, CurrentEntity);

                return CriteriaTransformer.TransformToRowCount(_queryByExample.GetExecutableCriteria(_session))
                    .UniqueResult<int>();
            }
            catch (Exception e)
            {
                Debug.Print( e.Message );
                const string caption = "La ricerca ha generato un errore imprevisto";
                const string msg = "Interrogazione fallita";
                MessageBox.Show(caption, msg, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return -1;
            }
        }

        #endregion

        //public bool TryGetUnproxiedEntity( long maxWait=9000 )
        //{
        //    int pos = UserEntitiesView.CurrentPosition;
        //    var sw = new Stopwatch();
        //    sw.Start();
        //    var instance = UserCollection[pos] as BaseEntity;
        //    //var list = UserCollection as List<BaseEntity>;
        //    //var instance = list[pos].As<BaseEntity>();
        //    UnProxy(instance);
        //    while ( instance is INHibernateProxy && sw.ElapsedMilliseconds < maxWait )
        //    {
        //        Thread.Sleep( 250 );
        //        sw.Stop();
        //        if ( sw.ElapsedMilliseconds > maxWait )
        //            return false;
        //        sw.Start();
        //        instance = (BaseEntity) UserCollection[pos];
        //        UnProxy(instance);
        //    }
        //    sw.Stop();
        //    return true;
        //}

        private bool TryGetUnproxiedEntity(long maxWait=3000)
        {
            ShowCursor.Show();
            int pos = UserEntitiesView.CurrentPosition;
            var sw = new Stopwatch();
            sw.Start();
            var instance = UserCollection[pos] as IFlyFetch;
            if (instance == null) return true;
            while ( !instance.TryUnproxyFlyFetch  && sw.ElapsedMilliseconds < maxWait)
            {
                Thread.Sleep(250);
                sw.Stop();
                if (sw.ElapsedMilliseconds > maxWait)
                    return false;
                sw.Start();
                instance = UserCollection[pos] as IFlyFetch;
            }
            sw.Stop();
            return true;
        }

        private void UnProxy(BaseEntity instance)
        {
            _session.GetSessionImplementation().PersistenceContext.Unproxy(instance);
        }

        public event Action Disposed = delegate { };

        protected new virtual void Dispose()
        {
            base.Dispose();

            _session?.Dispose();

            _statelessSession?.Dispose();

            Disposed();
        }

        private void OnClose( object sender, EventArgs args )
        {
            UserEntitiesView.CurrentChanged -= OnCurrentSelectionChanged;
        }

        #region command

        protected virtual bool CanDeleteEntity(BaseEntity poco)
        {
            return poco != null && poco.Id > 0L;
        }
        
        protected virtual bool CanAddNewEntity( BaseEntity poco )
        {
            return true;
        }

        protected virtual bool CanSaveExecuted()
        {
            return IsInEditing && (bool) AllowSave;
        }
        private ICommand _performCancel;
        public ICommand PerformCancel
        {
            get
            {
                if ( _performCancel != null ) return _performCancel;
                _performCancel = new RelayCommand( param => _onCancelDelegate(), param => CancelEditCanExecuted() );
                return _performCancel;
            }
        }

        private ICommand _deleteEntityCommand;
        public ICommand DeleteEntityCommand
        {
            get
            {
                if ( _deleteEntityCommand != null ) return _deleteEntityCommand;
                _deleteEntityCommand = new RelayCommand(param => MakeTransient(), param => AllowDelete);
                return _deleteEntityCommand;
            }
        }

        private ICommand _saveEntity;
        public ICommand SaveEntity
        {
            get
            {
                if ( _saveEntity != null ) return _saveEntity;

                _saveEntity = new RelayCommand( param =>
                {
                    ShowCursor.Show();

                    Persist();
                },
                
                param => CanSaveExecuted() );
                
                return _saveEntity;
            }

        }

        private ICommand _addNewEntity;
        public ICommand AddNewEntity
        {
            get
            {
                if ( _addNewEntity != null ) return _addNewEntity;
                _addNewEntity = new RelayCommand(param => AddNew(),param => AllowInsertNewEntity );
                return _addNewEntity;
            }

        }

        private ICommand _enterKey;
        public ICommand EnterKey
        {
            get
            {
                if ( _enterKey != null ) return _enterKey;

                _enterKey = new RelayCommand( param => EnterKeyHandler() );
                return _enterKey;
            }

        }

        private ICommand _searchOnSite;
        public ICommand SearchOnSite
        {
            get
            {
                if ( _searchOnSite != null ) return _searchOnSite;
                _searchOnSite = new RelayCommand( param => SwitchToSearchOnSiteMode(), param => true );
                return _searchOnSite;
            }

        }

        #endregion

        #region IRepository

        public object Read()
        {
            CurrentEntity =  LoadEntity( CurrentEntity.Id );
            return CurrentEntity;
        }

        public bool Persist(object entity)
        {
            return TryPersistEntity();
        }

        public bool Delete()
        {
            DeleteEntity();
            return true;
        }

        #endregion

    }
}