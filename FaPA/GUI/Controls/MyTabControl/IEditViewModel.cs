using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace FaPA.GUI.Controls.MyTabControl
{
    public interface IEditViewModel
    {
        void Load();
        void LoadAndShowCurrentEntity();
        object CurrentEntityToObject { get; }
        void Persist();
        void MakeTransient();
        void CreateNewEntity();
        bool TryPersistEntity();
        //IEditViewModel AddCrossCoupledPropValidation<TEntity, TProp>(Expression<Func<TEntity, TProp>> property);
        IEditViewModel AddEntityLevelPropValidation<TEntity, TProp>(Expression<Func<TEntity, TProp>> property);
        //void SetUpCollectionView(IList usercollection, ICollectionView listView);
        ICollectionView UserEntitiesView { get; }
    }
}