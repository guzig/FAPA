using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FaPA.Core;
using FaPA.DomainServices.Utils;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Utils;
using NHibernate;

namespace FaPA.GUI.Feautures.Anagrafica
{
    public class EditAnagraficaViewModel : EditViewModel<Core.Anagrafica>
    {
        //ctor
        public EditAnagraficaViewModel(IBasePresenter baseCrudPresenter, IList userEntities,
            ICollectionView userCollectionView, ISession session) : base(baseCrudPresenter, userEntities, userCollectionView)
        {
            SetUpSession(session, null);

            CurrentEntityPropChanged  += OnPropChanged;

            CurrentEntityChanged += OnCurrentChanged;
        }

        private void OnCurrentChanged(Core.Anagrafica currententity)
        {
            ComuniProvincia = Comuni.Where(p => p.SiglaProvincia == CurrentEntity.Provincia).OrderBy(c => c).ToList();
        }

        private void OnPropChanged(Core.Anagrafica currententity, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName == "Provincia")
            {
                ComuniProvincia = Comuni.Where(p => p.SiglaProvincia == CurrentEntity.Provincia).OrderBy(c => c).ToList();
            }
        }

        public override string EditTemplateName => "AnagraficaTemplate";

        private IList<Comune> _comuni;
        public IList<Comune> Comuni
        {
            get
            {
                if (_comuni == null)
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
                if (_provincie != null) return _provincie;

                _provincie = Comuni.DistinctBy(c => c.DenominazioneProvincia).OrderBy(c => c).ToList();

                return _provincie;
            }
        }

        private IList<Comune> _comuniProvincia;
        public IList<Comune> ComuniProvincia
        {
            get
            {
                return _comuniProvincia;
            }
            set
            {
                _comuniProvincia = value;
                NotifyOfPropertyChange(() => ComuniProvincia);
            }
        }

        //private Comune _provincia;
        //public Comune Provincia
        //{
        //    get { return _provincia; }
        //    set
        //    {
        //        _provincia = value;
        //        ComuniProvincia = Comuni.Where(p => p.SiglaProvincia == CurrentEntity.Provincia).
        //            OrderBy(c => c).ToList();
        //        NotifyOfPropertyChange(() => Provincia);
        //    }
        //}


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