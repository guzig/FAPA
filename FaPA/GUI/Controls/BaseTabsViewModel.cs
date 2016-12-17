using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using FaPA.AppServices.CoreValidation;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.Infrastructure;
using Remotion.Linq.Collections;
using FaPA.Core;
using NHibernate.Util;

namespace FaPA.GUI.Controls
{
    public abstract class BaseTabsViewModel<T, TProperty> : EditWorkSpaceViewModel<T, TProperty>
    {
        #region data members

        private readonly ObservableCollection<object> _userAddedNewPocos = new 
            ObservableCollection<object>();

        private ICollectionView _userCollectionView;
        public ICollectionView UserCollectionView
        {
            get { return _userCollectionView; }
            private set
            {
                _userCollectionView = value;
                NotifyOfPropertyChange( () => UserCollectionView );
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
            if (UserProperty != null)
            {
                InitCollectionView();

                foreach (var item in UserCollectionView)
                {
                    HookOnChanged(item);
                }

                Validate();
            }
            else
            {
                UserCollectionView = null;
            }
        }

        protected override void PersitEntity()
        {
            base.PersitEntity();

            if (_userAddedNewPocos.Contains(UserProperty))
            {
                _userAddedNewPocos.Remove(UserProperty);
            }
        }

        protected override void MakeTransient()
        {
            if ( !GetDeleteConfirmation() ) return;

            RemoveItem();

            Persist( Instance );

            UserProperty = ( TProperty ) base.Read();

            Init();

            AllowDelete = UserCollectionView != null && !UserCollectionView.IsEmpty;
        }
        
        protected override void OnCancelDelegateExecute()
        {
            if (_userAddedNewPocos.Contains(UserProperty))
            {
                RemoveItem();
            }

            base.OnCancelDelegateExecute();
        }
        
        protected override bool CanAddEntity(object obj)
        {
            var view = UserCollectionView as ListCollectionView;
            return view == null || ( !view.IsAddingNew && !view.IsEditingItem );
        }

        protected override void AddEntity()
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

            InitCollectionView();
            UserCollectionView.MoveCurrentToLast();
            CurrentPoco = UserCollectionView.CurrentItem;

            IsEditing = true;
            AllowDelete = false;
            AllowSave = true;
        }
        
        
        //helpers
        protected void RemoveFromFixedArray()
        {
            var current = UserCollectionView.CurrentItem;
            if (current == null) return;

            var source = UserProperty as object[];

            if (source == null) return;

            if (source.Length == 1)
            {
                UserProperty = default(TProperty);
                return;
            }

            var arrayLength = source.Length - 1 > 0 ? source.Length - 1 : 1;

            var newArray=Array.CreateInstance(typeof(TProperty).GetElementType(), arrayLength);

            var index = 0;
            foreach (var element in source.Where(e=>e!=current))
            {
                newArray.SetValue(element,index);
            }

            UserProperty = (TProperty) (object) newArray;
        }

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

        private void InitCollectionView()
        {
            UserCollectionView = CollectionViewSource.GetDefaultView(UserProperty);
            CurrentPoco = UserCollectionView.CurrentItem;
            UserCollectionView.CurrentChanged -= OnCurrentChanged;
            UserCollectionView.CurrentChanged += OnCurrentChanged;
            IsEmpty = UserCollectionView == null || UserCollectionView.IsEmpty;
        }

        protected virtual void OnCurrentChanged( object sender, EventArgs e)
        {
            CurrentPoco = UserCollectionView.CurrentItem;
            if ( CurrentPoco == null) return;
            Validate();
            AllowDelete = IsValid;
            HookOnChanged( CurrentPoco );
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
            //base.OnCurrentChanged(UserProperty, new PropertyChangedEventArgs("UserProperty"));
        }

        protected void ValidateAll()
        {
            var sourcePocoList = UserProperty as ICollection<object>;
            if (sourcePocoList == null) return;
            var index = 0;
            IsValid = true;
            foreach (var poco in sourcePocoList)
            {
                if ( !( ( IValidatable ) poco ).Validate().Success ) continue;
                IsValid = false;
                break;
            }           
        }

        protected  void AddToArray()
        {
            Array array = null;
            var elementType = typeof(TProperty).GetElementType();
            if (UserProperty == null)
            {
                UserProperty = (TProperty)(object)Array.CreateInstance(elementType, 1);
            }
            else //copy and resize
            {
                array = UserProperty as Array;
                if ( array == null ) return;
                var len = array.Length + 1;
                UserProperty = (TProperty)(object)Array.CreateInstance(elementType, len);
            }

            var aray = UserProperty as object[];
            if (aray == null) return;

            if ( aray.Length > 1 && array != null )
            {
                //copy old array items
                int index = 0;
                foreach ( var item in array )
                    aray[index++] = item;
            }

            //append new instance
            var newInstance = Activator.CreateInstance( elementType );
            ObjectExplorer.TryProxiedAllInstances<FaPA.Core.BaseEntity>( ref newInstance, "FaPA.Core" );
            aray[aray.Length - 1] = newInstance;

            _userAddedNewPocos.Add( newInstance );
            HookOnChanged( newInstance );
        }

        protected override void CancelEdit()
        {
            if ( _userAddedNewPocos.Contains( UserCollectionView.CurrentItem ) )
            {
                _userAddedNewPocos.Remove( UserCollectionView.CurrentItem );
            }

            IsEditing = false;

            UserProperty = ( TProperty ) base.Read();

            Init();

        }

    }
}