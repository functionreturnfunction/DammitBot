using System;
using DammitBot.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using StructureMap;

namespace DammitBot.Data.NHibernate.Library
{
    public class SessionFactoryBuilder : ISessionFactoryBuilder
    {
        #region Private Members

        private readonly IDataConfigurationManager _config;
        private readonly IContainer _container;

        #endregion

        #region Constructors

        public SessionFactoryBuilder(IDataConfigurationManager config, IContainer container)
        {
            _config = config;
            _container = container;
        }

        #endregion

        #region Exposed Methods

        public ISessionFactory Build()
        {
            try
            {
                return Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(_config.ConnectionString))
                    .Mappings(ConfigureMappings)
                    .ExposeConfiguration(ConfigureConfiguration)
                    .Diagnostics(d => d.Enable()).BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while configuring the database connection.", ex);
            }
        }

        private void ConfigureConfiguration(global::NHibernate.Cfg.Configuration cfg)
        {
            var listener = _container.GetInstance<PreSaveEventListener>();
            cfg.AppendListeners(ListenerType.PreInsert, new IPreInsertEventListener[] {listener});
            cfg.AppendListeners(ListenerType.PreUpdate, new IPreUpdateEventListener[] {listener});
        }

        private static void ConfigureMappings(MappingConfiguration m)
        {
            m.FluentMappings.AddFromAssemblyOf<SessionFactoryBuilder>()
                .Conventions.Add(
                    Table.Is(x => Inflector.Inflector.Pluralize(x.EntityType.Name)),
                    PrimaryKey.Name.Is(_ => "Id"),
                    ForeignKey.EndsWith("Id"));
        }

        #endregion
    }
}
