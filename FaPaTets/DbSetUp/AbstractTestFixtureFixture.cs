using System.ComponentModel;
using FaPA.Core;
using FaPA.DomainServices;
using FaPA.Infrastructure;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;

namespace FaPaTets.DbSetUp
{
    public abstract class AbstractTestFixtureFixture
    {
        protected ISessionFactory _sessionFactory;
        private Configuration _configuration;

        [SetUp]
        public void SetUp()
        {
            BootStrapper.Initialize();
            _sessionFactory = BootStrapper.SessionFactory;
        }

        [TearDown]
        public void TearDown()
        {
            if (_sessionFactory != null)
                _sessionFactory.Dispose();
        }


        public static void Check_entity_handles_PropertyChanged(BaseEntity entity)
        {
            Assert.That(entity, Is.InstanceOf<INotifyPropertyChanged>());

            var eventWasCalled = false;
            var propertyName = string.Empty;
            object sender = null;

            ((INotifyPropertyChanged)entity).PropertyChanged += (s, e) =>
            {
                eventWasCalled = true; sender = s; propertyName = e.PropertyName;
            };

            entity.Id = 99999;
            
            Assert.That(eventWasCalled);
            Assert.That(propertyName, Is.EqualTo("Id"));
            Assert.That(sender, Is.SameAs(entity));
        }

        
    }
}