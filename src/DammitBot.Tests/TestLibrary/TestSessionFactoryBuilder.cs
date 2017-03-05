using DammitBot.Configuration;
using DammitBot.Data.NHibernate.Library;
using FluentNHibernate.Cfg.Db;
using StructureMap;

namespace DammitBot.TestLibrary
{
    public class TestSessionFactoryBuilder : SessionFactoryBuilder
    {
        
        public NHibernate.Cfg.Configuration Configuration { get; protected set; }
        #region Constructors

        public TestSessionFactoryBuilder(IDataConfigurationManager config, IContainer container) : base(config, container) {}

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