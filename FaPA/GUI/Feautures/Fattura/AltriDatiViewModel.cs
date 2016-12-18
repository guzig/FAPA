using System.Windows.Input;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;

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
                if (_isDropDownOpen == false)
                    OnDropDownClosedExecuted();
                NotifyOfPropertyChange(() => IsDropDownOpen);
            }
        }

        private ICommand _onDropDownButtonClosed;
        public ICommand OnDropDownButtonClosed
        {
            get
            {
                if (_onDropDownButtonClosed != null) return _onDropDownButtonClosed;
                _onDropDownButtonClosed = new RelayCommand(p1 => IsDropDownOpen = false, p => true);
                return _onDropDownButtonClosed;
            }
        }

        private void OnDropDownClosedExecuted()
        {
            AllowSave = IsValid;
        }

        //ctor 
        public AltriDatiViewModel( IRepository repository, DettaglioLineeType instance) :
            base( (DettaglioLineeType f) => f.AltriDatiGestionali, repository, instance, "Altri dati", true )
        {}

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

            //Persist( Instance );
            //Read();

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

        //protected override bool CanAddEntity( object obj )
        //{
            //var viewModel = MainViewModel as EditFatturaViewModel;
            //var hasParentErrors = false;
            //if (viewModel?.CurrentEntity?.DettaglioLinee == null) return !HasError; 
            //foreach (var poco in viewModel.CurrentEntity.DettaglioLinee)
            //{
            //    if (!CoreValidatorService.HasErrors(poco)) continue;
            //    hasParentErrors = true;
            //    break;
            //}

            //return !HasError && !hasParentErrors;

            //return true;
       // }

    }


}