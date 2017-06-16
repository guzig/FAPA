using System.Windows.Input;
using FaPA.Infrastructure.Utils;

namespace FaPA.GUI.Design.Templates
{
    public partial class UsersGrid
    {
        public UsersGrid()
        {
            InitializeComponent();
            UsersGridControl.PreviewKeyDown += UsersGridControl_OnPreviewKeyDown;
        }

        private void UsersGridControl_OnPreviewKeyDown( object sender, KeyEventArgs e )
        {
            DataGridHelpers.DataGridKeyUpEventHandler( e, UsersGridControl );
        }
    }
}
