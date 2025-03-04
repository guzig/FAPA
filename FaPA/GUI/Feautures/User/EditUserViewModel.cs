using System.Collections;
using System.ComponentModel;
using FaPA.Core;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Design.Events;
using FaPA.Infrastructure.Helpers;
using FaPA.Infrastructure.Utils;
using NHibernate;

namespace FaPA.GUI.Feautures.User
{  
    public class EditUserViewModel : EditViewModel<UserData>
    {
        public override string EditTemplateName => "UserTemplate";

        public EditUserViewModel(IBasePresenter baseCrudPresenter, IList userEntities, 
            ICollectionView userCollectionView, ISession session) : base(baseCrudPresenter, userEntities, userCollectionView)
        {
                SetUpSession(session, null);
        }

        public override void PublishAddedNewEntityEvent(BaseEntity dto)
        {
            EventPublisher.Publish( new UserAddedNew { Dto = ( UserData ) CurrentEntity }, this );
        }

        public override void PublishUpdateEntityEvent(BaseEntity dto)
        {
            EventPublisher.Publish( new UserUpdated { Dto = ( UserData ) CurrentEntity }, this );
        }

        protected override void PublishDeletedEntityEvent(BaseEntity dto)
        {
            EventPublisher.Publish( new UserDeleted { Dto = ( UserData ) dto }, this );
        }


    }
}