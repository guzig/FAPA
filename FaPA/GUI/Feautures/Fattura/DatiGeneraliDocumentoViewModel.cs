using System;
using System.Windows;
using System.Windows.Data;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiGeneraliDocumentoViewModel : EditWorkSpaceViewModel<Core.Fattura, DatiGeneraliDocumentoType>
    {
        //ctor
        public DatiGeneraliDocumentoViewModel(IRepository repository, Core.Fattura instance) : 
            base( repository, instance, (Core.Fattura f) => f.DatiGeneraliDocumento,"DatiGenerali", false )
        { }

        protected override void HookOnChanged(object poco)
        {
            var entity = poco as DatiGeneraliDocumentoType;
            if (entity == null) return;

            Hook(entity);

            if (entity.DatiRitenuta != null)
            {
                Hook( entity.DatiRitenuta );
            }

            if (entity.DatiBollo != null)
            {
                Hook(entity.DatiBollo);
            }

            if (entity.DatiCassaPrevidenziale != null)
            {
                Hook(entity.DatiCassaPrevidenziale);
            }

            if (entity.ScontoMaggiorazione != null)
            {
                foreach (var dettaglio in entity.ScontoMaggiorazione)
                {
                    Hook(dettaglio);
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