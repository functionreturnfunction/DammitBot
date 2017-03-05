using System;
using DammitBot.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Instances;
using NHibernate;
using NHibernate.Event;
using StructureMap;

namespace DammitBot.Data.NHibernate.Library
{
    public class SessionFactoryBuilder : ISessionFactoryBuilder
    {
        #region Private Members

        private readonly IDataConfigurationManager _config;
        private readonly PreSaveEventListener _preSaveEventListener;
        private readonly IMappingConfigurationService _mappingConfigurationService;

        #endregion

        #region Constructors

        public SessionFactoryBuilder(IDataConfigurationManager config, PreSaveEventListener preSaveEventListener, IMappingConfigurationService mappingConfigurationService)
        {
            _config = config;
            _preSaveEventListener = preSaveEventListener;
            _mappingConfigurationService = mappingConfigurationService;
        }

        #endregion

        #region Private Methods

        protected virtual IPersistenceConfigurer ConfigureDatabase()
        {
            return MsSqlConfiguration.MsSql2008.ConnectionString(_config.ConnectionString);
        }

        protected virtual void ConfigureConfiguration(global::NHibernate.Cfg.Configuration cfg)
        {
            cfg.AppendListeners(ListenerType.PreInsert, new IPreInsertEventListener[] {_preSaveEventListener});
            cfg.AppendListeners(ListenerType.PreUpdate, new IPreUpdateEventListener[] {_preSaveEventListener});
        }

        private void ConfigureMappings(MappingConfiguration m)
        {
            _mappingConfigurationService.Configure(m);
        }

        #endregion

        #region Exposed Methods

        public ISessionFactory Build()
        {
            try
            {
                return Fluently.Configure()
                    .Database(ConfigureDatabase())
                    .Mappings(ConfigureMappings)
                    .ExposeConfiguration(ConfigureConfiguration)
                    .Diagnostics(d => d.Enable())
                    .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while configuring the database connection.", ex);
            }
        }

        #endregion
    }
}
