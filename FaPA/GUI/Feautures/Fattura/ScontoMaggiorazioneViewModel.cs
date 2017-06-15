using System.Windows.Input;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using FaPA.Infrastructure.Utils;

namespace FaPA.GUI.Feautures.Fattura
{
    public class ScontoMaggiorazioneViewModel : BaseTabsViewModel<DettaglioLineeType, ScontoMaggiorazioneType[]>
    {
        private bool _isDropDownOpen;
        public bool IsDropDownOpen
        {
            get { return _isDropDownOpen; }
            set
            {
                if ( value == _isDropDownOpen ) return;
                NotifyOfPropertyChange( () => IsDropDownOpen );
            }
        }

        private ICommand _onDropDownButtonClosed;
        public ICommand OnDropDownButtonClosed
        {
            get
            {
                if ( _onDropDownButtonClosed != null ) return _onDropDownButtonClosed;
                _onDropDownButtonClosed = new RelayCommand( OnDropDownClosedExecuted, p => true );
                return _onDropDownButtonClosed;
            }
        }

        private void OnDropDownClosedExecuted( object param )
        {
            var ctrldd = param as Xceed.Wpf.Toolkit.DropDownButton;
            if ( ctrldd == null )
                return;
            ctrldd.IsOpen = false;
            AllowSave = IsValid;
        }

        //ctor 
        public ScontoMaggiorazioneViewModel( IRepository repository, DettaglioLineeType instance ) :
            base( (DettaglioLineeType f) => f.ScontoMaggiorazione, repository, instance, "Sconto/Magg.", true )
        { }

        //persist change in the main tab view model
        public override void PersitEntity()
        {
            //base.PersitEntity();
        }

        //persist change in the main tab view model
        public override void MakeTransient()
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