using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emule.GUI.Features.BackUpRestore;
using FaPA.AppServices;
using FaPA.DomainServices.Utils;
using FaPA.Infrastructure;
using FaPA.Infrastructure.Helpers;
using FaPA.Infrastructure.Utils;
using FaPA.Properties;
using MessageBox = System.Windows.Forms.MessageBox;

namespace FaPA.GUI.Feautures.BackUpRestore
{
    public class Presenter : AbstractPresenter<Model, View>
    {

        //ctor

        public Presenter()
        {
            //View.Presenter = this;

            //var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;

            //if ( customPrincipal == null || customPrincipal.Identity is AnonymousIdentity )
            //    throw new ArgumentException( "Privilegi insufficenti" );

            Model = new Model( );

            View.Closing += OnCloseView;
        }

        private void OnCloseView( object sender, CancelEventArgs e )
        {

            if ( Model?.IsEditingEnabled == null || Model.IsEditingEnabled.Value==true ) return;
            e.Cancel = true;
            const string msg = "Attendere la fine delle operazioni...";
            MessageBox.Show( msg, "Trasferimento dati in corso, attendere..");
        }

        private static void BackUp( DirectoryInfo backUpPath )
        {
            var fullBackUpPath = backUpPath + @"\FEPA.bak";

            var sql = @"BACKUP DATABASE FEPA TO DISK = '" + fullBackUpPath  + "'";

            if ( !SqlActionUtil.RunSql( sql, 0 ).ToLower().Contains( "successfully") )
            {
                throw new Exception();
            }
        }

        private static string Restore(string backUpFullPath)
        {
            string sql = @"USE master  " +
                         @"ALTER DATABASE [FEPA] SET Single_User WITH Rollback Immediate " +
                         @"RESTORE DATABASE FEPA FROM DISK = N'" + backUpFullPath + "' WITH REPLACE,  " +
                         @"MOVE 'FEPA_Data' TO '" + StoreAccess.DbFullPath + "' , " +
                         @"MOVE 'FEPA_Log' TO '" + StoreAccess.DbLogFullPath + "' , " +
                         //@"MOVE 'FEPA_STREAMS' TO '" + StoreAccess.StreamFullPath + "' , FILE=1 " +
                         @" ALTER DATABASE [FEPA] SET Multi_User";

            //Debug.WriteLine(sql);

            return SqlActionUtil.RunSql(sql, 0 );
        }

        public Fact CanSelezionaBackUpDirectory
        {
            get
            {
                return new Fact( Model.IsEditingEnabled, () => Model.IsEditingEnabled );
            }
        }

        public void OnSelezionaBackUpDirectory()
        {
            Model.IsEditingEnabled.Value = false;

            string selectedPath = null;

            using ( var dialog = new FolderBrowserDialog() )
            {
                dialog.Description = "Seleziona la cartella di destinazione in cui memorizzare la copia dei dati";
                dialog.ShowNewFolderButton = false;
                dialog.SelectedPath = Settings.Default.LastImportPath;
                //dialog.RootFolder = Environment.SpecialFolder.Desktop;

                if ( dialog.ShowDialog() == DialogResult.OK )
                {
                    Model.IsEditingEnabled.Value = true;
                    selectedPath = dialog.SelectedPath;
                    Model.DiskPath.Value = selectedPath;
                    Settings.Default.LastBackUpPath = selectedPath;
                    Settings.Default.Save();
                    return;
                }

            }

            Model.IsEditingEnabled.Value = true;
        }

        public Fact CanDoBackUp
        {
            get
            {
                return new Fact( Model.DiskPath, () => !string.IsNullOrWhiteSpace( Model.DiskPath.Value ) &&  
                Model.IsEditingEnabled );
            }
        }

        public void OnDoBackUp()
        {
            Model.IsEditingEnabled.Value = false;
            ShowCursor.Show();
            var outPath = new StringBuilder().Append( Model.DiskPath ).Append( @"\BackUp_" ).
                Append( DateTime.Now.ToShortDateString() ).Replace( "/", "-" ).
                Append( "_" ).Append( DateTime.Now.ToLongTimeString().Replace( ":", "-" ) ).ToString();

            Directory.CreateDirectory( outPath );
            var backUpPath = IO.GetOrCreateFolder( outPath );

            Task.Factory.StartNew( ()  =>
            {
                try
                {
                    BackUp( backUpPath );
                    return string.Empty;
                }
                catch ( Exception e )
                {
                    return e.Message;
                }
            } ).ContinueWith(obj =>
            {
                Model.IsEditingEnabled.Value = true;
                var result = (string) obj.Result;
                if ( string.IsNullOrWhiteSpace(result) )
                {
                    var msg = "Backup terminato.";
                    MessageBox.Show( msg, "Backup completato", MessageBoxButtons.OK );
                }
                else
                {
                    string msg = "Si è verificato un errore durante le operazioni di backup!" + Environment.NewLine +
                                 "Assicurarsi che MSSQLSERVER disponga dei diritti di scrittura sulla cartella di destinazione."
                                 + Environment.NewLine + result;

                    MessageBox.Show( msg, "Backup fallito...", MessageBoxButtons.OK );
                }

            }, TaskScheduler.FromCurrentSynchronizationContext() );

        }


        //ripristino
        public Fact CanSelezionaFileBackUp
        {
            get
            {
                return new Fact( Model.IsEditingEnabled, () => Model.IsEditingEnabled );
            }
        }

        public void OnSelezionaFileBackUp()
        {
            Model.IsEditingEnabled.Value = false;
            using ( var dialog = new OpenFileDialog() )
            {
                dialog.Filter = "BackUp Files (.bak)|*.bak";
                dialog.FilterIndex = 1;
                dialog.Multiselect = false;

                if ( dialog.ShowDialog() == DialogResult.OK )
                {
                    Model.IsEditingEnabled.Value = true;
                    Model.RestorePath.Value = dialog.FileName;
                    return;
                }                                
            }
            Model.IsEditingEnabled.Value = true;
        }

        public Fact CanDoRestore
        {
            get
            {
                return new Fact( Model.RestorePath, () => !string.IsNullOrWhiteSpace( Model.RestorePath.Value ) &&
                Model.RestorePath.Value.ToLower().EndsWith(".bak") && Model.IsEditingEnabled );
            }
        }

        public void OnDoRestore()
        {
            Model.IsEditingEnabled.Value = false;
            Task.Factory.StartNew( () => DoRestore( Model.RestorePath.Value )).ContinueWith(obj =>
            {
                Model.IsEditingEnabled.Value = true;
                var result = (string) obj.Result;
                if ( !string.IsNullOrWhiteSpace(result) && result.ToLower().Contains( "successfully" ) )
                    MessageBox.Show( "La copia è stata ripristinata correttamente", "Ripristino terminato",
                        MessageBoxButtons.OK, MessageBoxIcon.Information );
                else
                    MessageBox.Show( result, "Ripristino fallito...", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }, TaskScheduler.FromCurrentSynchronizationContext() );
        }

        private static string DoRestore(string backUpFileNamePath)
        {
            string msg = "Il ripristino di una copia del database deve essere effettuato dalla macchina server. " + Environment.NewLine + "Conferma operazione di ripristino della copia del database con quello corrente?";

            var dialogResult = MessageBox.Show( msg, "Conferma ripristino...", MessageBoxButtons.YesNo );

            return dialogResult == DialogResult.Yes ? Restore( backUpFileNamePath ) : null;
        }
    }
}
