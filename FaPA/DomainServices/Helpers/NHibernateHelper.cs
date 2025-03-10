﻿using System;
using System.Collections.Generic;
using FaPA.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Proxy.DynamicProxy;

namespace FaPA.DomainServices.Helpers
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static Configuration _configuration;
        private static HbmMapping _mapping;

        public static ISession OpenSession()
        {
            //Open and return the nhibernate session
            return SessionFactory.OpenSession();
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                if ( _sessionFactory == null )
                {
                    //Create the session factory
                    _sessionFactory = Configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static Configuration Configuration
        {
            get
            {
                if ( _configuration == null )
                {
                    //Create the nhibernate configuration
                    _configuration = CreateConfiguration();
                }
                return _configuration;
            }
        }

        public static HbmMapping Mapping
        {
            get
            {
                if ( _mapping == null )
                {
                    //Create the mapping
                    _mapping = CreateMapping();
                }
                return _mapping;
            }
        }

        private static Configuration CreateConfiguration()
        {
            var configuration =  new Configuration()
            .DataBaseIntegration( db =>
            {
                db.ConnectionString = "Data Source=:memory:;Version=3;New=True;";//Uid=nhibernate;Pwd=nhibernate;
                db.Dialect<SQLiteDialect>();
            } );
            //Loads properties from hibernate.cfg.xml
            //configuration.Configure();
            //Loads nhibernate mappings 
            configuration.AddDeserializedMapping( Mapping, null );

            return configuration;
        }

        private static HbmMapping CreateMapping()
        {
            var mapper = new ModelMapper();
            //Add the person mapping to the model mapper
            mapper.AddMappings( new List<System.Type> { typeof( FatturaMap ) } );
            //Create and return a HbmMapping of the model mapping in code
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        public static Type NhUnproxyType( this object input )
        {
            if ( !( input is IProxy ) ) return input.GetType();
            var proxyInspector = new NhProxyInspector();
            return proxyInspector.GuessType( input );
        }

        public static object Unproxy(this object input)
        {
            if (!(input is IProxy)) return input;
            var interceptor = ( ( IProxy ) input ).Interceptor as PropChangedAndDataErrorDynProxyInterceptor;
            if (interceptor == null) return input;
            return interceptor.Proxy;
        }
    }
}