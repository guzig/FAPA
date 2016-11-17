using System.Collections.ObjectModel;
using FaPA.Core;
using NHibernate.Criterion;

namespace FaPA.GUI.Controls.MyTabControl
{
    public interface IBasePresenter
    {
        void SetActiveWorkSpace(int index);
        ObservableCollection<WorkspaceViewModel> Workspaces { get; }
        WorkspaceViewModel GetActiveWorkSpace();
        void RefreshSharedViewsAfterAddedNew(BaseEntity updatedEntity);
        void RefreshSharedViewsAfterDelete(BaseEntity deletedEntity);
        void RefreshSharedViewsAfterUpdated(BaseEntity dto);
        //void CreateNewModel(DetachedCriteria criteria, bool checkIsIncrementalSearch);
        //void CreateNewModel(IList<BaseEntity> entities, bool checkIsIncrementalSearch);
        void CreateNewModel( DetachedCriteria queryByExample );
        void CreateNewModel( int activeWorkSpace );
    }

}