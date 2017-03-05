using DammitBot.Configuration;
using DammitBot.Data.NHibernate.Library;
using FluentNHibernate.Cfg.Db;

namespace DammitBot.TestLibrary
{
    public class TestSessionFactoryBuilder : SessionFactoryBuilder
    {
        #region Properties

        public NHibernate.Cfg.Configuration Configuration { get; protected set; }

        #endregion

        #region Constructors

        public TestSessionFactoryBuilder(IDataConfigurationManager config, PreSaveEventListener preSaveEventListener, IMappingConfigurationService mappingConfigurationService) : base(config, preSaveEventListener, mappingConfigurationService) {}

        #endregion

        #region Private Methods

        protected override IPersistenceConfigurer ConfigureDatabase()
        {
            return SQLiteConfiguration.Standard.InMemory().ShowSql();
        }

        protected override void ConfigureConfiguration(NHibernate.Cfg.Configuration cfg)
        {
            base.ConfigureConfiguration(cfg);
            Configuration = cfg;
        }

        #endregion
    }
}