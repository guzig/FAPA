using System.Windows;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiDocumentoViewModel : EditWorkSpaceViewModel<Core.Fattura, DatiGeneraliDocumentoType>
    {
        private ScontoMaggiorazioneGeneraleViewModel _scontoMaggiorazioneGeneraleView;
        public ScontoMaggiorazioneGeneraleViewModel ScontoMaggiorazioneGeneraleView
        {
            get { return _scontoMaggiorazioneGeneraleView; }
            set
            {
                if (Equals(value, _scontoMaggiorazioneGeneraleView)) return;
                _scontoMaggiorazioneGeneraleView = value;
                NotifyOfPropertyChange(() => ScontoMaggiorazioneGeneraleView);
            }
        }

        //ctor
        public DatiDocumentoViewModel(IRepository repository, Core.Fattura instance) :
            base(repository, instance, (Core.Fattura f) => f.DatiGeneraliDocumento, "Dati documento", false)
        {
            ScontoMaggiorazioneGeneraleView = new ScontoMaggiorazioneGeneraleViewModel(repository, 
                instance.DatiGeneraliDocumento);
            ScontoMaggiorazioneGeneraleView.Init();
        }



        protected override void HookOnChanged(object poco)
        {
            var entity = poco as DatiGeneraliDocumentoType;
            if (entity == null) return;

            HookChanged(entity);

            if (entity.DatiRitenuta != null)
            {
                HookChanged( entity.DatiRitenuta );
            }

            if (entity.DatiBollo != null)
            {
                HookChanged(entity.DatiBollo);
            }

            if (entity.DatiCassaPrevidenziale != null)
            {
                HookChanged(entity.DatiCassaPrevidenziale);
            }

            if (entity.ScontoMaggiorazione != null)
            {
                foreach (var dettaglio in entity.ScontoMaggiorazione)
                {
                    HookChanged(dettaglio);
                }
            }


        }

        protected override void OnRequestClose()
        {
            if (UserProperty != null)
            {
                const string lockMessage = "Non è possibile chiudere una scheda contenente dati.";
                MessageBox.Show(lockMessage, "Scheda bloccata", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            base.OnRequestClose();
        }

    }
}