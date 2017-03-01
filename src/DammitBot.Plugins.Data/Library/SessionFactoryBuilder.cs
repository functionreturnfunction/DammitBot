using System;
using DammitBot.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DammitBot.Data.Library
{
    public class SessionFactoryBuilder : ISessionFactoryBuilder
    {
        private readonly IDataConfigurationManager _config;

        public NHibernate.Cfg.Configuration Configuration { get; private set; }

        public SessionFactoryBuilder(IDataConfigurationManager config)
        {
            _config = config;
        }

        public ISessionFactory Build()
        {
            ISessionFactory sessionFactory = null;

            try
            {
                sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(_config.ConnectionString))
                    .Mappings(m => { m.FluentMappings.AddFromAssemblyOf<SessionFactoryBuilder>(); })
                    .ExposeConfiguration(config => {
                        Configuration = config;
                        new SchemaExport(config).Execute(true, true, false);
                    })
                    .Diagnostics(d => d.Enable()).BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while configuring the database connection.", ex);
            }

            return sessionFactory;
        }
    }
}
