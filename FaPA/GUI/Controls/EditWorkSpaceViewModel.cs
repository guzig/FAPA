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

        private bool _isInEditing;
        public bool IsInEditing
        {
            get { return _isInEditing; }
            set
            {
                if (value == _isInEditing) return;
                _isInEditing = value;
                NotifyOfPropertyChange(() => IsInEditing);
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

            IsInEditing = false;
            AllowDelete = true;
            AllowInsertNew = false;
        }       
        #endregion

        public virtual void Init<TP,TD>()
        {
            //PocoType = typeof(TP);
            AllowDelete = UserProperty != null;
            AllowInsertNew = true;

            if ( UserProperty == null ) return;
            CurrentPoco = UserProperty;
            HookOnChanged( UserProperty );
            BeginEdit();
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
            IsInEditing = false;
            PersitEntity();
            AllowInsertNew = true;
        }
        
        protected virtual bool CanSaveExecuted()
        {
            return Repository!= null && AllowSave;
        }
        
        protected virtual bool CancelEditCanExecuted()
        {
            return IsInEditing; 
        }

        protected virtual void OnCancelDelegateExecute()
        {
            CancelEdit();
            BeginEdit();
            IsInEditing = false;
            LockMessage = null;
            AllowSave = false;
            AllowDelete = UserProperty != null;
            AllowInsertNew = true;
        }

        protected void OnPropChanged(object sender, PropertyChangedEventArgs eventArg)
        {
            if ( !( sender is BaseEntity ) ) return;

            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;
            IsInEditing = true;
            //OnCurrentChanged(sender, eventArg);
            var validatable = sender as IValidatable;
            AllowSave = validatable == null || validatable.DomainResult.Success;
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

        protected virtual void PersitEntity()
        {
            ShowCursor.Show();

            if ( !Repository.Persist(Instance) ) return;

            Instance = ( T ) Repository.Read();

            UserProperty = GetterProp( Instance );

            LockMessage = null;
            AllowSave = false;
            IsInEditing = false;
        }

        #region IEditable

        protected bool IsEditing;
        //private BaseEntity _backupCopy;

        protected void BeginEdit()
        {
            if (IsEditing) return;
            IsEditing = true;
        }

        protected virtual void CancelEdit()
        {
            if (!IsEditing) return;
            IsEditing = false;

            if (Repository == null) return;

            var instance = ( T ) Repository.Read();

            UserProperty =  GetterProp( instance ) ;
        }

        #endregion

        public virtual object Read()
        {
            return GetterProp((T) Repository.Read());
        }

        public virtual bool Persist(object entity)
        {
            return Repository.Persist(Instance);
        }

        public virtual bool Delete()
        {
            return Repository.Persist(Instance);
        }

    }
}