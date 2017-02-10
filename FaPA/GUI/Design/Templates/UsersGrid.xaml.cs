using System.Windows.Input;
using FaPA.GUI.Utils;

namespace FaPA.GUI.Design.Templates
{
    public partial class UsersGrid
    {
        public UsersGrid()
        {
            InitializeComponent();
            GridControl = UsersGridControl;
            GridControl.PreviewKeyDown += UsersGridControl_OnPreviewKeyDown;
            RecordsToolBar = recordsToolBar;
            EmptyMessage = emptyMessage;
        }

        private void UsersGridControl_OnPreviewKeyDown( object sender, KeyEventArgs e )
        {
            DataGridHelpers.DataGridKeyUpEventHandler( e, UsersGridControl );
        }
    }
}
