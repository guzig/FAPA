using System.Linq;
using System.Windows.Input;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using FaPA.Infrastructure.Helpers;
using FaPA.Infrastructure.Utils;

namespace FaPA.GUI.Feautures.Fattura
{
    public class AltriDatiViewModel : BaseTabsViewModel<DettaglioLineeType, AltriDatiGestionaliType[]> 
    {
        private bool _isDropDownOpen;
        public bool IsDropDownOpen
        {
            get { return _isDropDownOpen; }
            set
            {
                if (value == _isDropDownOpen) return;
                _isDropDownOpen = value;
                //if (_isDropDownOpen == false)
                //    OnDropDownClosedExecuted();
                NotifyOfPropertyChange(() => IsDropDownOpen);
            }
        }

        private ICommand _onDropDownButtonClosed;
        public ICommand OnDropDownButtonClosed
        {
            get
            {
                if (_onDropDownButtonClosed != null) return _onDropDownButtonClosed;
                _onDropDownButtonClosed = new RelayCommand(OnDropDownClosedExecuted , p => true);
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
        public AltriDatiViewModel( IRepository repository, DettaglioLineeType instance) :
            base( f => f.AltriDatiGestionali, repository, instance, "Altri dati", true )
        {}

        //persist change in the main tab view model
        //protected override void PersitEntity()
        //{
        //    //base.PersitEntity();
        //}

        //persist change in the main tab view model
        //protected override void MakeTransient()
        //{
        //    //if ( !GetDeleteConfirmation() ) return;

        //    RemoveItem();

        //    Init();


        //    AllowDelete = UserCollectionView != null && !UserCollectionView.IsEmpty;
        //}

        protected override void AddItemToUserCollection()
        {
            AddToArray();
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

        public override DettaglioLineeType ReadInstance()
        {
            var root = Repository.Read();
            var current = CurrentPoco?.Unproxy();
            if ( current == null ) return null;
            var list = ( ( Core.Fattura ) root ).DatiBeniServizi.DettaglioLinee;
            if (list == null || !list.Any())
                return null;
            return list.FirstOrDefault(r => r.Unproxy() == current);
        }

    }


}