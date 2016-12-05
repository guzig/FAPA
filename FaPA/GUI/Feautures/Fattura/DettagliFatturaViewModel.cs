using System;
using System.Collections.Generic;
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
                if (Equals(value, _altridatiViewModel)) return;
                _altridatiViewModel = value;
                NotifyOfPropertyChange(() => AltridatiViewModel);
            }
        }

        readonly Dictionary<DettaglioLineeType, AltriDatiViewModel> _viemModelChilds = 
            new Dictionary<DettaglioLineeType, AltriDatiViewModel>();

        public DettagliFatturaViewModel( IRepository repository, Core.Fattura instance ) :
            base( repository, instance, ( Core.Fattura f ) => f.DettaglioLinee, "", false )
        {}

        protected override void AddItemToUserCollection()
        {
            AddToArray();
            var aray = UserProperty as DettaglioLineeType[];
            if ( aray == null ) return;
            var lastAdded= aray[aray.Length - 1];
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
            var dettaglio = cview.CurrentItem as DettaglioLineeType;
            if ( dettaglio == null ) return;

            InitAltriDatiVm( dettaglio );
        }

        private void InitAltriDatiVm( DettaglioLineeType dettaglio )
        {
            if ( _viemModelChilds.ContainsKey( dettaglio ) )
                _altridatiViewModel = _viemModelChilds[dettaglio];
            else
            {
                _altridatiViewModel = new AltriDatiViewModel( this, dettaglio );
                _altridatiViewModel.Init();
                _altridatiViewModel.CurrentEntityChanged += OnAltriDatiPropertyChanged;
                _viemModelChilds.Add( dettaglio, _altridatiViewModel );
            }

            AltridatiViewModel = _altridatiViewModel;
            AllowSave = IsValidate();
        }

        private void OnAltriDatiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;
            IsInEditing = true;
            AllowSave = IsValidate();
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

    }

}