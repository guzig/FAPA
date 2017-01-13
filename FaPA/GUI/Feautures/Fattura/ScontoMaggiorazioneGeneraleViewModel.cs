using System.Windows.Input;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class ScontoMaggiorazioneGeneraleViewModel : BaseTabsViewModel<DatiGeneraliDocumentoType, ScontoMaggiorazioneType[]>
    {

        //ctor 
        public ScontoMaggiorazioneGeneraleViewModel(IRepository repository, DatiGeneraliDocumentoType instance) :
            base(f => f.ScontoMaggiorazione, repository, instance, "Sconto/Magg.", true)
        {
        }

        //persist change in the main tab view model
        protected override void PersitEntity()
        {
            //base.PersitEntity();
        }

        //persist change in the main tab view model
        protected override void MakeTransient()
        {
            //if ( !GetDeleteConfirmation() ) return;

            RemoveItem();

            Init();

            AllowDelete = UserCollectionView != null && !UserCollectionView.IsEmpty;
        }

        protected override void AddItemToUserCollection()
        {
            AddToArray();
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

    }
}