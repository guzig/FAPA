using System;
using System.Xml.Serialization;
using FaPA.Data;
using NHibernate.Mapping.ByCode;
using NUnit.Framework;

namespace FaPaTets.DbSetUp
{
    [TestFixture]
    public class MappingTest
    {
        [Test]
        public void CanGenerateXmlMapping()
        {
            var mapper = new ModelMapper();
            mapper.AddMapping<FatturaMap>();

            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            var xmlSerializer = new XmlSerializer( mapping.GetType() );

            xmlSerializer.Serialize( Console.Out, mapping );
        }
    }
}