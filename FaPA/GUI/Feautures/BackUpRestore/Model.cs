using System.IO;
using Caliburn.Micro;
using FaPA.AppServices;
using FaPA.Infrastructure.Helpers;
using FaPA.Properties;

namespace Emule.GUI.Features.BackUpRestore
{
    public class Model : PropertyChangedBase
    {
        //private readonly ISession _session;

        private GenericsObservable<bool> _isEditingEnabled = new GenericsObservable<bool>(true);
        public GenericsObservable<bool> IsEditingEnabled 
        {
            get { return _isEditingEnabled; }
            set
            {
                if (Equals(value, _isEditingEnabled)) return;
                _isEditingEnabled = value;
                NotifyOfPropertyChange(() => IsEditingEnabled);
            }
        }

        public GenericsObservable<string> DiskPath { get;} = new GenericsObservable<string>();
        public GenericsObservable<string> RestorePath { get; } = new GenericsObservable<string>();

        //ctor
        public Model(  )
        {
            //_session = Session;
            IsEditingEnabled = new GenericsObservable<bool>(true);
            var lastPath = GetLastBackUpPath();
            DiskPath.Value = lastPath ?? StoreAccess.BackUpPath;
        }

        private static string GetLastBackUpPath()
        {
            var lastPath = Settings.Default.LastBackUpPath;

            if ( string.IsNullOrWhiteSpace( lastPath ) ) return null;

            return Directory.Exists( lastPath ) ? lastPath : null;
        }
    }
}
