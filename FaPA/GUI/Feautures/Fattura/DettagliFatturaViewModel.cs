using System;
using System.ComponentModel;
using System.Windows.Data;
using FaPA.Core;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DettagliFatturaViewModel : BaseTabsViewModel<Core.Fattura, DettaglioLineeType[]>
    {
        private AltriDatiViewModel _altridatiViewModel ;
        public AltriDatiViewModel AltridatiViewModel
        {
            get { return _altridatiViewModel; }
            set
            {
                _altridatiViewModel = value;
                NotifyOfPropertyChange(() => AltridatiViewModel);
            }
        }

        public DettagliFatturaViewModel( IRepository repository, Core.Fattura instance ) :
            base( ( Core.Fattura f ) => f.DettaglioLinee, repository, instance, "", false )
        {}

        protected override void AddItemToUserCollection()
        {
            AddToArray();
            if ( UserProperty == null ) return;
            var lastAdded= UserProperty[UserProperty.Length - 1];
            InitAltriDatiVm( lastAdded );
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

        protected override void OnCurrentChanged(object  sender, EventArgs e)
        {
            base.OnCurrentChanged(sender, e);

            var cview = sender as ListCollectionView;
            var dettaglio = cview?.CurrentItem as DettaglioLineeType;
            if ( dettaglio == null ) return;

            InitAltriDatiVm( dettaglio );
        }
        
        private void InitAltriDatiVm( DettaglioLineeType dettaglio )
        {
            AltridatiViewModel = new AltriDatiViewModel( this, dettaglio );
            AltridatiViewModel.Init();
            AltridatiViewModel.CurrentEntityChanged += OnAltriDatiPropertyChanged;

            AllowSave = IsValidate();
        }

        private void OnAltriDatiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;
            IsEditing = true;
            AllowSave = IsValidate();

            var dettaglio = UserCollectionView.CurrentItem as DettaglioLineeType;
            if (dettaglio == null) return;

            ( ( IValidatable ) dettaglio).ValidatePropertyValue( "AltriDatiGestionali" );

            base.OnPropChanged(dettaglio, e);

        }

        public override bool Delete()
        {
            SetterProp( Instance, null);
            return base.Persist(Instance);
        }

        private bool IsValidate()
        {
            var isValidAltriDati = AltridatiViewModel == null || AltridatiViewModel.IsValid;
            return isValidAltriDati ;
        }

        //public override void RefreshView()
        //{
        //    base.RefreshView();
        //    AltridatiViewModel.RefreshView( );
        //}

    }

}