using System.Data;
using DammitBot.Configuration;
using DammitBot.Data.Library;
using DammitBot.Data.Migrations.Library;
using DammitBot.Data.Dapper.Library;
using DammitBot.Ioc;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using Microsoft.Data.Sqlite;

namespace DammitBot.TestLibrary
{
    public abstract class InMemoryDatabaseUnitTestBase<TTarget> : UnitTestBase<TTarget>
    {
        #region Private Methods

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            var connection = new SqliteConnection(new SqliteConnectionStringBuilder {
                Mode = SqliteOpenMode.Memory
            }.ToString());

            _container.Configure(e => {
                e.For<IInstantiationService>().Use<InstantiationService>();
                e.For<IAssemblyService>().Use<AssemblyService>();
                e.For<IDataCommandHelper>().Use<DataCommandHelper>();
                e.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                e.For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();
                e.For<IUnitOfWork>().Use<TestUnitOfWork>();
                e.For<IDisposableUnitOfWork>().Use<TestDisposableUnitOfWork>();

                new DapperPluginContainerConfiguration().Configure(e);
                new ReminderPluginContainerConfiguration().Configure(e);

                e.For<IDbConnection>().Use(_ => connection).Singleton();
            });

            // var builder = (TestSessionFactoryBuilder)_container.GetInstance<ISessionFactoryBuilder>();
            // var session = builder.Build().OpenSession();

            // _container.Inject(session.Connection);
            // new SchemaExport(builder.Configuration).Execute(true, true, false, session.Connection, null);
        }

        public InMemoryDatabaseUnitTestBase()
        {
            var migrationRunner = _container.GetInstance<MigrationRunner>();

            migrationRunner.Up();
        }

        #endregion
    }
}