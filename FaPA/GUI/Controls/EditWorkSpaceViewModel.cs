using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;
using FaPA.Core;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Feautures.Fattura;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;
using Action = System.Action;

namespace FaPA.GUI.Controls
{
    public abstract class EditWorkSpaceViewModel<T, TProperty> : WorkspaceViewModel, IRepository, IDispose
    {
        //ctor
        protected EditWorkSpaceViewModel(IRepository repository, T instance,
            Expression<Func<T, TProperty>> getter, string dispName, bool isClosable) 
        {
            SetterProp = ReflectionHelpers.GetSetter(getter);
            GetterProp = getter.Compile();
            Repository = repository;
            Instance = instance;
            DisplayName = dispName;
            IsCloseable = isClosable;
        }

        #region data members

        //public bool IsOnInit { get; protected set; }

        protected readonly Action<T, TProperty> SetterProp;
        protected readonly Func<T, TProperty> GetterProp;
        protected readonly IRepository Repository;

        protected T Instance { get; set; }

        public TProperty UserProperty
        {
            get
            {
                return GetterProp(Instance);
            }

            set
            {
                SetterProp(Instance, (TProperty)value);
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

        public bool AllowInsertNew { get; set; }

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

        protected virtual void AddEntity()
        {
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

            UserProperty = Activator.CreateInstance<TProperty>();

            IsEditing = false;
            AllowDelete = true;
            AllowInsertNew = false;
        }       
        #endregion

        public virtual void Init()
        {
            AllowDelete = UserProperty != null;
            AllowInsertNew = true;

            if ( UserProperty == null ) return;
            CurrentPoco = UserProperty;
            HookOnChanged( UserProperty );
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

        protected virtual void MakeTransient()
        {
            if ( !GetDeleteConfirmation() ) return;
            UserProperty = default(TProperty);
            IsEditing = false;
            PersitEntity();
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
            IsEditing = false;
            LockMessage = null;
            AllowSave = false;
            AllowDelete = UserProperty != null;
            AllowInsertNew = true;
        }

        protected void OnPropChanged(object sender, PropertyChangedEventArgs eventArg)
        {
            if ( !( sender is BaseEntity ) ) return;

            var validatable = (IValidatable)sender;

             IsEditing = true;
            
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

            IsValid = validatable.DomainResult.Success;
            AllowSave = IsValid;
            OnCurrentChanged(sender, eventArg);
        }

        protected virtual void HookOnChanged( object poco )
        {
            var notifyPropertyChanged = poco as INotifyPropertyChanged;
            if ( notifyPropertyChanged == null ) return;
            notifyPropertyChanged.PropertyChanged -= OnPropChanged;
            notifyPropertyChanged.PropertyChanged += OnPropChanged;
        }

        protected static bool GetDeleteConfirmation()
        {
            return MessageBox.Show("Conferma eliminazione?", "Conferma", MessageBoxButton.YesNo, 
                MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public event Action Disposed = delegate { };
        public delegate void OnCurrentChangedhandler(object sender, PropertyChangedEventArgs eventArg);
        public event OnCurrentChangedhandler CurrentEntityChanged;

        private void OnCurrentChanged(object sender, PropertyChangedEventArgs eventArg)
        {
            var handler = CurrentEntityChanged;
            handler?.Invoke(sender, eventArg);
        }

        protected virtual void PersitEntity()
        {
            ShowCursor.Show();

            if ( !Repository.Persist(Instance) ) return;

            LockMessage = null;
            AllowSave = false;
            IsEditing = false;
        }

        #region IEditable

        //private BaseEntity _backupCopy;


        protected virtual void CancelEdit()
        {
            if (!IsEditing ) return;
            _isEditing = false;

            if (Repository == null) return;

            var instance = ( T ) Repository.Read();

            UserProperty =  GetterProp( instance ) ;
        }

        #endregion

        #region IRepository

        public virtual object Read()
        {
            var read = Repository.Read();
            var instance = (T) read;
            var userProp = GetterProp(instance);
            return userProp;
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