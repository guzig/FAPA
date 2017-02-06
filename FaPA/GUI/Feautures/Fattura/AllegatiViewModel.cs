using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using FaPA.AppServices.CoreValidation;
using FaPA.Core;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;
using Microsoft.Win32;

namespace FaPA.GUI.Feautures.Fattura
{
    public class AllegatiViewModel : BaseTabsViewModel<FatturaElettronicaBodyType, AllegatiType[]>
    {
        private string _filePath;

        private ICommand _openPdf;
        public ICommand OpenPdf
        {
            get
            {
                if (_openPdf != null) return _openPdf;
                _openPdf = new RelayCommand(param => OnOpendPdf(), param => true);
                return _openPdf;
            }
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
            return !string.IsNullOrWhiteSpace(FilePath);
        }

        //private void OnOpendPdf()
        //{

        //    var path = Path.GetTempFileName().Replace("tmp", "PDF");

        //    //try
        //    //{
        //    //    using (var fs = File.Create(path))
        //    //    {
        //    //        fs.Write(stream.PdfStream, 0, stream.PdfStream.Length);
        //    //    }
        //    //    Process.Start(path);
        //    //}
        //    //catch
        //    //{}
        //    //finally
        //    //{}
        //}

        private void OnOpendPdf()
        {
            const string filter = "PDF File (*.pdf)|*.pdf"; 
            var dlg = new OpenFileDialog
            {
                //InitialDirectory = Settings.Default.LastImportPath,
                Multiselect = false,
                DefaultExt = ".pdf",
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
            CurrentPoco = File.ReadAllBytes(FilePath);

            IsEditing = true;
            AllowDelete = true;

        }

        protected override bool CanSaveExecuted()
        {
            return IsEditing && !string.IsNullOrWhiteSpace(FilePath);
        }

        protected override void AddEntity()
        {
            FilePath = null;
            base.AddEntity();
        }

        public void OnApriFileLog()
        {
            //Process.Start("notepad.exe", _fileLog);
        }


        //ctor
        public AllegatiViewModel( IRepository repository, FatturaElettronicaBodyType instance) :
            base( f => f.Allegati, repository, instance, "Allegati", false )
        {
            DisplayName = "Allegati";
            IsCloseable = false;
        }

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
    }
}