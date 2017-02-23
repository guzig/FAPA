using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using FaPA.AppServices.CoreValidation;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.Infrastructure;
using Remotion.Linq.Collections;
using FaPA.Core;
using FaPA.Infrastructure.Helpers;
using NHibernate.Proxy.DynamicProxy;
using NHibernate.Util;

namespace FaPA.GUI.Controls
{
    public abstract class BaseTabsViewModel<T, TProperty> : EditWorkSpaceViewModel<T, TProperty>, IViewModel
    {
        #region data members

        private readonly ObservableCollection<object> _userAddedNewPocos = new 
            ObservableCollection<object>();

        private ICollectionView _userCollectionView;
        public ICollectionView UserCollectionView
        {
            get { return _userCollectionView; }
            set
            {
                _userCollectionView = value;
                NotifyOfPropertyChange( () => UserCollectionView );
            }
        }

        public delegate void OnCurrentChangedhandler( object currententity );

        public event OnCurrentChangedhandler CurrentEntityChanged;

        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get
            {
                return _isEmpty;
            }
            set
            {
                _isEmpty = value;
                NotifyOfPropertyChange( () => IsEmpty );
            }
        }

        #endregion
        
        //ctor
        protected BaseTabsViewModel( Expression<Func<T, TProperty>> getter, IRepository repository, T instance, 
            string dispName, bool isClosable ) : base( repository, instance, getter, dispName, isClosable)
        {}
        
        protected abstract void RemoveItemFromUserCollection();

        protected abstract void AddItemToUserCollection();      
          
        //overrides
        public override void Init()
        {
            if ( Instance != null && UserProperty != null )
            {
                AllowDelete = true;
                InitCollectionView();
                Validate();
            }
            else
            {
                AllowDelete = false;
                CurrentPoco = null;
                UserCollectionView = null;
                IsEmpty = true;
            }
        }

        public override void PersitEntity()
        {
            base.PersitEntity();

            if (_userAddedNewPocos.Contains(UserProperty))
            {
                _userAddedNewPocos.Remove(UserProperty);
            }

            LockMessage = null;

            base.Read();
        }

        public override void MakeTransient()
        {
            if ( !GetDeleteConfirmation() ) return;

            RemoveItem();

            Persist( Instance );

            Instance = ReadInstance();

            UserProperty = GetUserProperty(); 
            CurrentPoco = UserProperty;

            Init();

            AllowDelete = UserCollectionView != null && !UserCollectionView.IsEmpty;

            LockMessage = null;
        }
        
        protected override void OnCancelDelegateExecute()
        {
            if (_userAddedNewPocos.Contains(CurrentPoco))
            {
                RemoveItem();
            }

            base.OnCancelDelegateExecute();
        }
        
        protected override bool CanAddEntity(object obj)
        {
            //var view = UserCollectionView as ListCollectionView;
            //var isa = view == null || ( !view.IsAddingNew && !view.IsEditingItem );
            //return isa;
            return true;
        }

        public override void AddEntity()
        {
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

            var view = UserCollectionView as ListCollectionView;
            if ( view != null && ( view.IsAddingNew || view.IsEditingItem ) )
            {
                return;
                //view.CommitEdit();
                //view.CommitNew();
            }

            AddItemToUserCollection( );

            IsEditing = true;
            AllowDelete = true;
            Validate();
            AllowSave = IsValid;

            InitCollectionView();
            UserCollectionView.MoveCurrentToLast();
        }

        protected override object CreateInstance()
        {
            var elementType = typeof( TProperty ).GetElementType();
            return  Activator.CreateInstance( elementType );
        }
        
        public override void CancelEdit()
        {
            var currentIndx = -1;
            if ( UserCollectionView != null && UserCollectionView.Any() )
                currentIndx = UserCollectionView.CurrentPosition;

            IsEditing = false;

            if ( Repository != null )
            {
                Instance = ReadInstance();
                if ( Instance != null )
                    UserProperty = GetUserProperty();
            }

            Init();

            if ( UserProperty == null ) return;
            if ( currentIndx >= 0 )
            {
                UserCollectionView?.MoveCurrentToPosition( currentIndx );
            }
        }

  
        
        //helpers

        private void InitCollectionView()
        {
            UserCollectionView = CollectionViewSource.GetDefaultView(UserProperty);
            UserCollectionView.MoveCurrentToFirst();
            CurrentPoco = UserCollectionView.CurrentItem;
            ( ( BaseEntity ) CurrentPoco ).IsValidating = true;
            UserCollectionView.CurrentChanged -= OnCurrentChanged;
            UserCollectionView.CurrentChanged += OnCurrentChanged;
            IsEmpty = UserProperty == null || UserCollectionView == null || UserCollectionView.IsEmpty;

            foreach ( var item in UserCollectionView )
            {
                Debug.Assert( item is INotifyPropertyChanged );

                HookChanged( item );

                ( ( BaseEntity ) item ).IsValidating = true;
            }

        }
        

        protected virtual void OnCurrentChanged( object sender, EventArgs eventArgs )
        {
            CurrentPoco = UserCollectionView.CurrentItem;
            if ( CurrentPoco == null) return;
            Debug.Assert( CurrentPoco is IProxy );
            ( ( BaseEntity ) CurrentPoco ).IsValidating = true;

            HookChanged( CurrentPoco );

            Validate();

            AllowSave = IsEditing && IsValid;
            AllowDelete = true;

            OnCurrentChanged( CurrentPoco );
        }

        private void OnCurrentChanged( object sender )
        {
            var handler = CurrentEntityChanged;
            handler?.Invoke( sender );
        }


        protected void RemoveFromFixedArray()
        {
            var current = ((BaseEntity) UserCollectionView.CurrentItem).Unproxy();
            if (current == null) return;

            var source = UserProperty as object[];

            if (source == null) return;

            if (source.Length == 1)
            {
                UserProperty = default(TProperty);
                CurrentPoco = null;
                IsEmpty = true;
                return;
            }

            var arrayLength = source.Length - 1 > 0 ? source.Length - 1 : 1;

            var newArray=Array.CreateInstance(typeof(TProperty).GetElementType(), arrayLength);

            var index = 0;
            foreach (var element in source.Where(e=>e.Unproxy()!=current))
            {
                newArray.SetValue(element,index++);
            }

            UserProperty = (TProperty) (object) newArray;
        }

        protected void RemoveItem()
        {
            RemoveItemFromUserCollection();
            var view = UserCollectionView as ListCollectionView;
            if ( view != null && ( view.IsAddingNew || view.IsEditingItem ) )
            {
                view.CommitEdit();
                view.CommitNew();
            }
            Validate();

        }
        
        protected  void AddToArray()
        {
            Array array = null;
            var elementType = typeof(TProperty).GetElementType();
            TProperty userProperty;
            if (UserProperty == null)
            {
                userProperty = (TProperty)(object)Array.CreateInstance(elementType, 1);
            }
            else //copy and resize
            {
                array = UserProperty as Array;
                if ( array == null ) return;
                var len = array.Length + 1;
                userProperty = (TProperty)(object)Array.CreateInstance(elementType, len);
            }

            var aray = userProperty as object[];
            if (aray == null) return;

            if ( aray.Length > 1 && array != null )
            {
                //append old array items
                int index = 0;
                foreach ( var item in array )
                    aray[index++] = item;
            }

            //append new instance
            var newInstance = CreateInstance();
                

            var proxy = ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntity>( newInstance );
            _userAddedNewPocos.Add( proxy );
            HookChanged( proxy );
            aray[aray.Length - 1] = proxy;

            ( ( IValidatable ) proxy ).Validate();

            UserProperty = userProperty;

        }


        protected void CloseIfNotEmpty()
        {
            if (UserCollectionView != null && !UserCollectionView.IsEmpty)
            {
                const string lockMessage = "Non è possibile chiudere una scheda contenente dati.";
                MessageBox.Show(lockMessage, "Scheda bloccata", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            base.OnRequestClose();
        }

        protected void ValidateAll()
        {
            var sourcePocoList = UserProperty as ICollection<object>;
            if (sourcePocoList == null) return;

            IsValid = true;
            foreach (var poco in sourcePocoList)
            {
                if ( !( ( IValidatable ) poco ).Validate().Success ) continue;
                IsValid = false;
                break;
            }           
        }

    }
}