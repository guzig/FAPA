using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;
using FaPA.AppServices.CoreValidation;
using FaPA.Core;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;
using Action = System.Action;

namespace FaPA.GUI.Controls
{
    public abstract class EditWorkSpaceViewModel<T, TProperty> : WorkspaceViewModel, IRepository, IViewModel, IDispose 
    {
        //ctor
        protected EditWorkSpaceViewModel(IRepository repository, T instance,
            Expression<Func<T, TProperty>> getter, string dispName, bool isClosable) 
        {
            SetterPropExp = ReflectionHelpers.GetSetter(getter);
            GetterPropExp = getter.Compile();
            Repository = repository;
            Instance = instance;
            DisplayName = dispName;
            IsCloseable = isClosable;
        }

        #region data members

        //public bool IsOnInit { get; protected set; }

        protected readonly Action<T, TProperty> SetterPropExp;
        protected readonly Func<T, TProperty> GetterPropExp;
        protected readonly IRepository Repository;

        public T Instance { get; set; }

        public TProperty UserProperty
        {
            get
            {
                return GetUserProperty();
            }

            set
            {
                SetUserProperty( value );
                NotifyOfPropertyChange(() => UserProperty);
            }
        }

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                if (value == _isValid) return;
                _isValid = value;
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        public bool AllowInsertNew { get; set; } = true;

        private bool _allowSave;
        public bool AllowSave
        {
            get { return _allowSave; }
            set
            {
                if (value == _allowSave) return;
                _allowSave = value;
                NotifyOfPropertyChange(() => AllowSave);
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

        private bool _isEditing;
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                if (value == _isEditing) return;
                _isEditing = value;
                NotifyOfPropertyChange(() => IsEditing);
            }
        }

        protected delegate void OnCancelAction();
        protected OnCancelAction OnCancelDelegate = delegate { };

        private ICommand _performCancel;
        public ICommand PerformCancel
        {
            get
            {
                if (_performCancel != null) return _performCancel;
                _performCancel = new RelayCommand(param => OnCancelDelegateExecute(), param => CancelEditCanExecuted());
                return _performCancel;
            }
        }

        private ICommand _deleteEntity;
        public ICommand DeleteEntityCommand
        {
            get
            {
                if (_deleteEntity != null) return _deleteEntity;
                _deleteEntity = new RelayCommand(param => MakeTransient(), CanDeleteEntity); //AllowDelete
                return _deleteEntity;
            }
        }

        protected virtual bool CanDeleteEntity(object obj)
        {
            return AllowDelete;
        }
        
        private ICommand _saveEntity;
        public ICommand SaveEntity
        {
            get
            {
                if ( _saveEntity != null) return _saveEntity;

                _saveEntity = new RelayCommand(param =>
                {
                    ShowCursor.Show();

                    PersitEntity();
                },
                param => CanSaveExecuted());

                return _saveEntity;
            }

        }

         private ICommand _addItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand != null) return _addItemCommand;
                _addItemCommand = new RelayCommand(param => AddEntity(), CanAddEntity);
                return _addItemCommand;
            }
        }

        protected virtual bool CanAddEntity(object obj)
        {
           return AllowInsertNew;
        }

        public virtual void AddEntity()
        {
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

            var userProperty = CreateInstance();

            ( ( IValidatable ) userProperty ).Validate();

            CurrentPoco = ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntity>( userProperty ); 

            HookChanged( ( INotifyPropertyChanged) CurrentPoco );

            UserProperty = (TProperty)CurrentPoco;

            IsEditing = true;
            AllowDelete = true;
            AllowSave = ( ( IValidatable ) userProperty ).IsValid();
            AllowInsertNew = false;
        }

        protected virtual object CreateInstance()
        {
            return Activator.CreateInstance<TProperty>();
        }

        #endregion

        protected void SetUserProperty( TProperty value)
        {
            if ( Instance != null )
                SetterPropExp( Instance, value );
            NotifyOfPropertyChange( () => UserProperty );
        }

        protected TProperty GetUserProperty()
        {
            if ( Instance != null )
                return GetterPropExp( Instance );

            return default ( TProperty );
        }


        public virtual void Init()
        {
            AllowDelete = UserProperty != null;
            AllowInsertNew = true;

            if ( UserProperty == null ) return;
            CurrentPoco = UserProperty;
            HookChanged( ( INotifyPropertyChanged) UserProperty );
            ( ( BaseEntity ) CurrentPoco ).IsValidating = true;
        }

        protected virtual void Validate()
        {
            if (CurrentPoco == null)
            {
                IsValid = true;
                return;
            }
            ( ( IValidatable ) CurrentPoco ).Validate();
            IsValid = ( ( BaseEntity ) CurrentPoco ).DomainResult.Success;
        }

        private object _currentPoco;
        public virtual object CurrentPoco
        {
            get
            {
                return _currentPoco;
            }
            set
            {
                _currentPoco = value;
                NotifyOfPropertyChange( () => CurrentPoco );
            }
        }

        public virtual void MakeTransient()
        {
            if ( !GetDeleteConfirmation() ) return;

            UserProperty = default(TProperty);
            CurrentPoco = null;
            IsEditing = false;

            PersitEntity();

            Instance = ReadInstance();
            UserProperty = GetUserProperty();

            //Debug.Assert(UserProperty==null);

            Init();
            AllowInsertNew = true;
        }
        
        protected virtual bool CanSaveExecuted()
        {
            return Repository!= null && AllowSave;
        }
        
        protected virtual bool CancelEditCanExecuted()
        {
            return IsEditing; 
        }

        protected virtual void OnCancelDelegateExecute()
        {
            CancelEdit();
            Init();
            IsEditing = false;
            LockMessage = null;
            AllowSave = false;
            AllowDelete = UserProperty != null;
            AllowInsertNew = true;
        }

        protected void OnPropChanged(object sender, PropertyChangedEventArgs eventArg)
        {
            if ( ProcessChangedEvent( sender ) ) return;

            OnPropertyChanged(sender, eventArg);
        }

        protected bool ProcessChangedEvent( object sender )
        {
            if ( !( sender is BaseEntity ) ) return true;

            var validatable = ( IValidatable ) sender;

            IsEditing = true;
            IsCloseable = false;

            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

            IsValid = validatable.DomainResult.Success;
            AllowSave = IsValid;
            return false;
        }

        protected virtual void HookChanged( INotifyPropertyChanged poco )
        {
            if (poco == null) return;
            poco.PropertyChanged -= OnPropChanged;
            poco.PropertyChanged += OnPropChanged;
            ( ( BaseEntity ) poco ).IsValidating = true;
        }

        protected static bool GetDeleteConfirmation()
        {
            return MessageBox.Show("Conferma eliminazione?", "Conferma", MessageBoxButton.YesNo, 
                MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public event Action Disposed = delegate { };

        #region INPC stuff

        public delegate void OnCurrentChangedhandler(object sender, PropertyChangedEventArgs eventArg);

        public event OnCurrentChangedhandler CurrentEntityPropChanged;

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs eventArg)
        {
            var handler = CurrentEntityPropChanged;
            handler?.Invoke(sender, eventArg);
        }

        #endregion

        public virtual void PersitEntity()
        {
            ShowCursor.Show();

            if ( !Repository.Persist(Instance) ) return;

            LockMessage = null;
            AllowSave = false;
            IsEditing = false;
            AllowDelete = true;
        }

        #region IEditable

        //private BaseEntity _backupCopy;


        public virtual void CancelEdit()
        {
            if (!IsEditing ) return;
            _isEditing = false;

            if (Repository == null) return;

            Reload();
        }

        public virtual void Reload()
        {
            Instance = ReadInstance();
            UserProperty = GetUserProperty();
            CurrentPoco = UserProperty;
        }

        #endregion

        public virtual T ReadInstance()
        {
            return ( T ) Read();
        }

        #region IRepository

        public object Read()
        {
            return Repository.Read();
        }

        public virtual bool Persist(object entity)
        {
            var esito = Repository.Persist(Instance);
            
            return esito;
        }

        public virtual bool Delete()
        {
            return Repository.Persist(Instance);
        }

        #endregion


    }
}