using DammitBot.Configuration;
using DammitBot.Data.Library;
using DammitBot.Data.NHibernate.Library;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DammitBot.TestLibrary
{
    public abstract class InMemoryDatabaseUnitTestBase<TTarget> : UnitTestBase<TTarget>
    {
        #region Private Methods

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            _container.Configure(e => {
                e.For<IInstantiationService>().Use<InstantiationService>();
                e.For<IMappingConfigurationService>().Use<MappingConfigurationService>();
                e.For<IAssemblyService>().Use<AssemblyService>();
                e.For<IDataConfigurationManager>().Use<DataConfigurationManager>();
                e.For<ISessionFactoryBuilder>().Use<TestSessionFactoryBuilder>().Singleton();
                e.For<IDataCommandHelper>().Use<DataCommandHelper>();
                e.For<ISessionFactory>().Use(ctx => ctx.GetInstance<ISessionFactoryBuilder>().Build());
                e.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                e.For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();
                e.For<IUnitOfWork>().Use<UnitOfWork>();
            });

            var builder = (TestSessionFactoryBuilder)_container.GetInstance<ISessionFactoryBuilder>();
            var session = builder.Build().OpenSession();

            new SchemaExport(builder.Configuration).Execute(true, true, false, session.Connection, null);

            _container.Configure(e => {
                e.For<ISession>().Use(ctx => ctx.GetInstance<ISessionFactory>().OpenSession(session.Connection));
            });
        }

        #endregion
    }
}