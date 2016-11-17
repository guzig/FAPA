using System;
using FaPA.GUI.Controls.MyTabControl;

namespace FaPA.GUI.Design.Events
{
    namespace Emule.GUI.Design.Events
    {
        public class RemoveTabView
        {
            public readonly WorkspaceViewModel ViewModel;
            public readonly IEditViewModel ParentViewModel;

            public RemoveTabView(WorkspaceViewModel viewModel, IEditViewModel parentViewModel)
            {
                ViewModel = viewModel;
                ParentViewModel = parentViewModel;
                if ( viewModel == null ) throw new ArgumentNullException(nameof(viewModel));
                if ( parentViewModel == null) throw new ArgumentNullException(nameof(parentViewModel));
            }
        }

    }
}
