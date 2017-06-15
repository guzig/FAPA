using System.Collections;
using System.ComponentModel;
using FaPA.Core;
using FaPA.GUI.Controls.MyTabControl;
using NHibernate;

namespace FaPA.GUI.Feautures.Anagrafica
{
    public class EditAnagraficaViewModel : EditViewModel<Core.Anagrafica>
    {
        public EditAnagraficaViewModel( IBasePresenter baseCrudPresenter, IList userEntities,
            ICollectionView userCollectionView, ISession session ) : base( baseCrudPresenter, userEntities, userCollectionView )
        {
            SetUpSession( session, null );
        }

        public override string EditTemplateName => "AnagraficaTemplate";

        #region Entity event *TODO*

        public override void PublishAddedNewEntityEvent( BaseEntity dto )
        {
            //EventPublisher.Publish(new AnagraficaAdded { Dto = (CentroDiCostoDto)CurrentDtoEntity }, this);
        }

        public override void PublishUpdateEntityEvent( BaseEntity dto )
        {
            //EventPublisher.Publish(new AnagraficaUpdated { Dto = (CentroDiCostoDto)CurrentDtoEntity }, this);
        }

        public override void PublishDeletedEntityEvent( BaseEntity dto )
        {
            //EventPublisher.Publish(new AnagraficaDeleted { Dto = (CentroDiCostoDto)dto }, this);
        }

        #endregion        

    }
}