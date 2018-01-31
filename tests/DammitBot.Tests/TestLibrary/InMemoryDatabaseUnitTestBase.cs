using DammitBot.Configuration;
using DammitBot.Data.Library;
using DammitBot.Data.Migrations.Library;
using DammitBot.Data.Dapper.Library;
using DammitBot.Ioc;
using DammitBot.Utilities;
using DammitBot.Wrappers;

namespace DammitBot.TestLibrary
{
    public abstract partial class InMemoryDatabaseUnitTestBase<TTarget> : UnitTestBase<TTarget>
    {
        protected SqliteConnection _connection;

        #region Private Methods

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            _connection = new SqliteConnection(new Microsoft.Data.Sqlite.SqliteConnection(new SqliteInMemoryConnectionStringService().GetMainAppConnectionString()));
            _connection.Name = "InMemoryDatabaseTest";

            _container.Configure(e => {
                e.For<IInstantiationService>().Use<InstantiationService>();
                e.For<IAssemblyService>().Use<AssemblyService>();

                new DapperPluginContainerConfiguration().Configure(e);
                new ReminderPluginContainerConfiguration().Configure(e);

                e.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                e.For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();
                e.For<IDbConnectionFactory>().Use(_ => new TestDbConnectionFactory(_connection));
                e.For<IUnitOfWork>().Use<TestUnitOfWork>();
                e.For<IDisposableUnitOfWork>().Use<TestDisposableUnitOfWork>();
            });
        }

        public InMemoryDatabaseUnitTestBase()
        {
            var migrationRunner = _container.GetInstance<MigrationRunner>();

            migrationRunner.Up();

            using (var uow = _container.GetInstance<IUnitOfWork>().Start())
            {
                var whatevs = uow.ExecuteReader("SELECT name FROM sqlite_master");
            }
        }

        #endregion

        public override void Dispose()
        {
            base.Dispose();
            _connection.ActuallyDispose();
        }
    }
}