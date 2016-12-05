namespace FaPA.Data
{
    using NHibernate;

    public class AddPropertyChangedInterceptor : EmptyInterceptor
    {
        private ISession _session;

        public override void SetSession(ISession session)
        {
            _session = session;
            base.SetSession(session);
        }

        public override object Instantiate(string clazz, EntityMode entityMode, object id)
        {
            if (entityMode != EntityMode.Poco)
                return base.Instantiate(clazz, entityMode, id);

            var classMetadata = _session.SessionFactory.GetClassMetadata(clazz);
            var entityType = classMetadata.GetMappedClass(entityMode);
            var entity = classMetadata.Instantiate(id, entityMode);
            return AddPropChangedAndDataErrorInterceptorProxyFactory.Create(entityType, entity);
        }
    }

}
