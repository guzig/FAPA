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
using FaPA.Core.FaPa;
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
        protected BaseTabsViewModel( IRepository repository, T instance,
            Expression<Func<T, TProperty>> getter, string dispName, bool isClosable) :
                base( repository, instance, getter, dispName, isClosable)
        {}
        
        protected abstract void RemoveItemFromUserCollection();

        protected abstract void AddItemToUserCollection();      
          
        //overrides
        public override void Init<TP, TD>(  )
        {
            Init();
        }

        public override object CurrentPoco
        {
            get
            {
                return UserCollectionView?.CurrentItem;
            }
        }

        protected override void Persist()
        {
            base.Persist();
            if ( _userAddedNewPocos.Contains( UserProperty ) )
                _userAddedNewPocos.Remove( UserProperty );
        }

        protected override void MakeTransient()
        {
            if ( !GetDeleteConfirmation() ) return;

            RemoveItem();

            Persist();

            AllowDelete = !UserCollectionView.IsEmpty;
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

            var objects = UserProperty as object[];
            _userAddedNewPocos.Add(objects.Last());
            
            if ( UserCollectionView != null )
            {
                UserCollectionView.CurrentChanged -= (sender, e) => OnCurrentChanged(sender, e);
                UserCollectionView.Refresh();
                UserCollectionView.CurrentChanged += (sender, e) => OnCurrentChanged(sender, e);
                UserCollectionView.MoveCurrentToLast();
            }
            else
            {
                InitCollectionView();
            }

            IsInEditing = true;
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

            var newArray=Array.CreateInstance(typeof(TProperty).GetElementType(), source.Length - 1);

            var index = 0;
            foreach (var element in source.Where(e=>e!=current))
            {
                newArray.SetValue(element,index);
            }

            UserProperty = (TProperty) (object) newArray;

        }

        private void Init()
        {
            if (UserProperty != null )
            {
                InitCollectionView();

                foreach (var item in UserCollectionView)
                {
                    HookOnChanged(item);
                }
            }
            Validate();
            BeginEdit();
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

            UserCollectionView.CurrentChanged -= OnCurrentChanged;
            UserCollectionView.CurrentChanged += OnCurrentChanged;
            IsEmpty = UserCollectionView == null || UserCollectionView.IsEmpty;
        }

        protected virtual void OnCurrentChanged( object sender, EventArgs e)
        {
            if ( !( sender is T ) ) return;

            var poco = UserCollectionView.CurrentItem;
            if (poco == null) return;
            Validate();
            AllowDelete = IsValid;
            HookOnChanged( poco );
        }

        private void RemoveItem()
        {
            RemoveItemFromUserCollection();
            var view = UserCollectionView as ListCollectionView;
            if ( view != null && ( view.IsAddingNew || view.IsEditingItem ) )
            {
                view.CommitEdit();
                view.CommitNew();
            }
            UserCollectionView.Refresh();
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
            var elementType = typeof(TProperty).GetElementType();
            var instance = Activator.CreateInstance(elementType);
            if (UserProperty == null)
            {
                UserProperty = (TProperty)(object)Array.CreateInstance(elementType, 1);
            }
            else
            {
                var array = UserProperty as Array;
                if (array == null) return;
                var len = array.Length + 1;
                UserProperty = (TProperty)(object)Array.CreateInstance(elementType, len);
            }

            var aray = UserProperty as object[];
            if (aray == null) return;
            aray[aray.Length - 1] = instance;
        }

        protected override void CancelEdit()
        {
            var currentIndx=-1;
            if ( UserCollectionView != null && UserCollectionView.Any() )
                currentIndx = UserCollectionView.CurrentPosition;

            IsEditing = false;

            if ( Repository != null )
            {
                var instance = Repository.Read();
                UserProperty = instance != null ? GetterProp( (T) instance ) : default ( TProperty );
            }

            if ( UserProperty != null )
            {
                InitCollectionView();

                if (currentIndx >= 0)
                {
                    UserCollectionView?.MoveCurrentToPosition(currentIndx);
                }
            }
            
            BeginEdit();
        }

        public override object Read()
        {
            var indx = UserCollectionView?.CurrentPosition;

            //refresh collection froom DB
            var userProp = base.Read() as object[];

            if (userProp != null)
            {
                if (indx != null && indx >= 0 && indx < userProp.Length)
                    return userProp[(int)indx];
                
                //collection changed in the meanwhile
                if ( userProp.Length >= 0 )
                    return userProp[0];
            }

            return null;
        }
    }
}