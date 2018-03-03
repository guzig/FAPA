using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using FaPA.Infrastructure.Utils;
using Microsoft.Win32;

namespace FaPA.GUI.Feautures.Fattura
{
    public class AllegatiViewModel : CrudListViewModel<FatturaElettronicaBodyType, AllegatiType[]>
    {
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            private set
            {
                if (value == _filePath) return;
                _filePath = value;
                NotifyOfPropertyChange(() => FilePath);
            }
        }

        private ICommand _showAttach;
        public ICommand ShowAttach
        {
            get
            {
                if (_showAttach != null) return _showAttach;
                _showAttach = new RelayCommand(param => OnShowAttach(), param => CanShowAttachExecuted() );
                return _showAttach;
            }
        }

        private ICommand _openPdf;
        public ICommand OpenPdf
        {
            get
            {
                if (_openPdf != null) return _openPdf;
                _openPdf = new RelayCommand(param => OnOpendPdf(), param => CanChooseFile() );
                return _openPdf;
            }
        }

        private bool CanChooseFile()
        {
            var current = CurrentPoco as AllegatiType;
            return current != null && current.Attachment == null;
        }

        private ICommand _loadBytes;
        public ICommand LoadBytes
        {
            get
            {
                if (_loadBytes != null) return _loadBytes;
                _loadBytes = new RelayCommand(param => LoadFileBytes(), param => CanLoadFileBytes());
                return _loadBytes;
            }
        }

        private bool CanLoadFileBytes()
        {
            return !string.IsNullOrWhiteSpace(FilePath) && IsEditing;
        }
        
        private void OnOpendPdf()
        {
            const string filter = "Tutti i File (*.*)|*.*"; 
            var dlg = new OpenFileDialog
            {
                //InitialDirectory = Settings.Default.LastImportPath,
                Multiselect = false,
                DefaultExt = ".PDF",
                Filter = filter
            };

            var result = dlg.ShowDialog();

            if (result != true) return;

            if (dlg.FileNames.Any())
            {
                FilePath = dlg.FileName;
            }

        }

        private void LoadFileBytes()
        {
            var current = CurrentPoco as AllegatiType;
            if (current != null)
            {
                try
                {
                    current.Attachment = File.ReadAllBytes( FilePath );
                    current.FormatoAttachment = Path.GetExtension( FilePath );
                    current.NomeAttachment = Path.GetFileName( FilePath );
                }catch( System.Exception e)
                {
                    const string caption = "Fattura PA: Errore nel caricamento del documento allegato";
                    Xceed.Wpf.Toolkit.MessageBox.Show( e.Message, caption, MessageBoxButton.OK, MessageBoxImage.Hand );
                }
            }

            IsEditing = true;
            AllowDelete = true;

        }
        
        private void OnShowAttach()
        {
            var current = CurrentPoco as AllegatiType;

            if ( current == null ) return;

            var path = Path.GetTempFileName().Replace("tmp", current.FormatoAttachment);

            try
            {
                using (var fs = File.Create(path))
                {
                    fs.Write( current.Attachment, 0, current.Attachment.Length);
                }

                Process.Start(path);
            }
            catch
            { }
        }


        //ctor
        public AllegatiViewModel( IRepository repository, FatturaElettronicaBodyType instance) :
            base( f => f.Allegati, repository, instance, "Allegati", false )
        {
            DisplayName = "Allegati";
            IsCloseable = false;
        }


        //overrides
        protected override void AddItemToUserCollection()
        {
            AddToArray();
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

        public override FatturaElettronicaBodyType ReadInstance()
        {
            var root = Repository.Read();
            return ( ( Core.Fattura ) root ).FatturaElettronicaBody;
        }

        protected override bool CanSaveExecuted()
        {
            var current = CurrentPoco as AllegatiType;
            return current?.Attachment != null && IsEditing;
        }

        private bool CanShowAttachExecuted()
        {
            var current = CurrentPoco as AllegatiType;
            return current?.Attachment != null ;
        }

        protected override bool CanAddEntity(object obj)
        {
            return !IsEditing;
        }

        public override void AddEntity()
        {
            FilePath = null;
            base.AddEntity();
        }

        protected override void OnCancelDelegateExecute()
        {
            base.OnCancelDelegateExecute();
            FilePath = null;
        }

    }
}