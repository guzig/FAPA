using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using FaPA.Core;
using FaPA.DomainServices.Utils;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Utils;
using NHibernate;

namespace FaPA.GUI.Feautures.Anagrafica
{
    public class EditAnagraficaViewModel : EditViewModel<Core.Anagrafica>
    {
        private IList<Comune> _comuni;
        public IList<Comune> Comuni
        {
            get
            {
                if ( _comuni == null )
                {
                    _comuni = new ReferenceDataFactory().GetReferenceCollection<Comune>();

                }
                return _comuni;
            }
        }

        private IList<Comune> _provincie;
        public IList<Comune> Provincie
        {
            get
            {
                if ( _provincie != null ) return _provincie;

                _provincie = Comuni.DistinctBy( c => c.DenominazioneProvincia ).OrderBy( c => c ).ToList();

                return _provincie;
            }
        }

        private IList<Comune> _comuniProvincia;
        private ICollectionView _comuniProvinciaView;

        public IList<Comune> ComuniProvincia
        {
            get
            {
                return _comuniProvincia;
            }
            set
            {
                _comuniProvincia = value;
                NotifyOfPropertyChange( () => ComuniProvincia );
            }
        }

        public ICollectionView ComuniProvinciaView
        {
            get { return _comuniProvinciaView; }
            set
            {
                _comuniProvinciaView = value;
                NotifyOfPropertyChange( () => ComuniProvinciaView );
            }
        }

     public EditAnagraficaViewModel(IBasePresenter baseCrudPresenter, IList userEntities,
            ICollectionView userCollectionView, ISession session) : base(baseCrudPresenter, userEntities, userCollectionView)
        {
            SetUpSession(session, null);

            //userCollectionView.CurrentChanged += OnCurrentChanged;

            CurrentEntityPropChanged  += OnPropChanged;

            CurrentEntityChanged += OnCurrentChanged;
        }

        public override void OnPageGotFocus()
        {
            base.OnPageGotFocus();

            if ( CurrentEntity != null )
            {
                ComuniProvincia = Comuni.Where( p => p.SiglaProvincia == CurrentEntity.Provincia ).OrderBy( c => c.Denominazione ).ToList();
                ComuniProvinciaView = CollectionViewSource.GetDefaultView( ComuniProvincia );
                ComuniProvinciaView.Refresh();
            }
        }

        private void OnCurrentChanged( Core.Anagrafica currententity )
        {
            if ( currententity != null )
            {
                ComuniProvincia = Comuni.Where( p => p.SiglaProvincia == CurrentEntity.Provincia ).OrderBy( c => c.Denominazione ).ToList();
            }
        }

        private void OnPropChanged(Core.Anagrafica currententity, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName == "Provincia")
            {
                ComuniProvincia = Comuni.Where(p => p.SiglaProvincia == CurrentEntity.Provincia).OrderBy(c => c).ToList();
            }
        }

        public override string EditTemplateName => "AnagraficaTemplate";

        #region Entity event

        public override void PublishAddedNewEntityEvent(BaseEntity dto)
        {
            //EventPublisher.Publish(new CategoriaAdded { Dto = (CentroDiCostoDto)CurrentDtoEntity }, this);
        }

        public override void PublishUpdateEntityEvent(BaseEntity dto)
        {
            //EventPublisher.Publish(new CategoriaUpdated { Dto = (CentroDiCostoDto)CurrentDtoEntity }, this);
        }

        public override void PublishDeletedEntityEvent(BaseEntity dto)
        {
            //EventPublisher.Publish(new CategoriaDeleted { Dto = (CentroDiCostoDto)dto }, this);
        }

        #endregion        

    }
}