using NHibernate;
using WAFEL.Core.CORE.AppServices;
using WAFEL.Core.CORE.DomainServices.Helpers;

namespace FaPA.Infrastructure
{

	public class AddPropertyChangedInterceptor : EmptyInterceptor
    {
        private ISession _session;

        public override void SetSession(ISession session)
        {
            _session = session;
            base.SetSession(session);
        }

		//public override object Instantiate(string clazz, EntityMode entityMode, object id)
		//{
		//    if (entityMode != EntityMode.Poco)
		//        return base.Instantiate(clazz, entityMode, id);

		//    var classMetadata = _session.SessionFactory.GetClassMetadata(clazz);
		//    var entityType = classMetadata.GetMappedClass(entityMode);
		//    var entity = classMetadata.Instantiate(id, entityMode);
		//    return AddPropChangedAndDataErrorInterceptorProxyFactory.Create(entityType, entity);
		//}

		public override object Instantiate(string clazz, object id)
		{
			// Get class metadata
			var classMetadata = _session.SessionFactory.GetClassMetadata(clazz);

			// Get the mapped entity type (use MappedClass property)
			var entityType = classMetadata.MappedClass; // Available in NHibernate 5.2+

			// Instantiate the entity (NHibernate 5.2+ removes entityMode parameter)
			var entity = classMetadata.Instantiate(id); // No entityMode parameter

			// Create proxy with interceptors
			return AddPropChangedAndDataErrorInterceptorProxyFactory.Create(entityType, entity);
		}
	}

}
