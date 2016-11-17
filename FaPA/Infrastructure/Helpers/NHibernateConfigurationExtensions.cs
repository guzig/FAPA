using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FaPA.Data.ValidationMaps;
using NHibernate.Cfg;
using NHibernate.Mapping;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Cfg.Loquacious;
using NHibernate.Validator.Engine;

namespace FaPA.Infrastructure.Helpers
{
    public static class NHibernateConfigurationExtensions
    {
        private static readonly PropertyInfo TableMappingsProperty =
            typeof(Configuration).GetProperty("TableMappings", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        public static void CreateIndexesForForeignKeys(this Configuration configuration)
        {
            configuration.BuildMappings();
            var tables = (ICollection<Table>)TableMappingsProperty.GetValue(configuration, null);
            foreach (var table in tables)
            {
                foreach (var foreignKey in table.ForeignKeyIterator)
                {
                    var idx = new Index();
                    idx.AddColumns(foreignKey.ColumnIterator);
                    idx.Name = "IDX" + foreignKey.Name.Substring(2);
                    idx.Table = table;
                    table.AddIndex(idx);
                }
            }
        }

        public static void ConfigureNHibernateValidator(this Configuration nhibernateConfiguration,
            ValidatorEngine validatorEngine)
        {
            //Environment.SharedEngineProvider = new NHibernateSharedEngineProvider();

            var validatorConfiguration = new FluentConfiguration();
            validatorConfiguration
                .AddEntityTypeInspector<NhProxyInspector>()
                .SetDefaultValidatorMode(ValidatorMode.UseAttribute)
                .Register(typeof(FatturaSofValidation).Assembly.GetTypes().Where(t => t?.Namespace != null && 
                t.Namespace.Equals("FaPA.Data.ValidationMaps") ).ValidationDefinitions())
                //.Register(new FornitoreSofValidation())
                .SetDefaultValidatorMode(ValidatorMode.UseExternal)
                .IntegrateWithNHibernate
                .ApplyingDDLConstraints()
                .RegisteringListeners();

            validatorEngine.Configure(validatorConfiguration);
            nhibernateConfiguration.Initialize(validatorEngine);
        }
    }
}