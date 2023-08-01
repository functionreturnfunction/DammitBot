using System;
using DammitBot.Data.Migrations.Library;
using DammitBot.Ioc;
using DammitBot.Utilities;
using DammitBot.Wrappers;

namespace DammitBot.Library
{
    public abstract class InMemoryDatabaseUnitTestBase<TTarget> : UnitTestBase<TTarget>
    {
        protected SqliteConnection _connection;
        protected bool _migrationsRun;

        #region Private Methods

        protected override void ExtraSetup()
        {
            RunMigrations();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            _connection = new SqliteConnection(
                new Microsoft.Data.Sqlite.SqliteConnection(
                    new SqliteInMemoryConnectionStringService().GetMainAppConnectionString()));
            _connection.Name = "InMemoryDatabaseTest";

            _container.Configure(e =>
            {
                e.For<IInstantiationService>().Use<InstantiationService>();
                e.For<IAssemblyService>().Use<AssemblyService>();

                new DapperPluginContainerConfiguration().Configure(e);
                new RemindersDapperPluginContainerConfiguration().Configure(e);

                e.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                e.For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();
                e.For<IDbConnectionFactory>()
                    .Use(_ => new TestDbConnectionFactory(_connection));
                e.For<IUnitOfWork>().Use<TestDapperUnitOfWork>();
            });
        }

        protected virtual void RunMigrations()
        {
            var migrationRunner = _container.GetInstance<MigrationRunner>();

            migrationRunner.Up(seed: false);
        }

        protected virtual void WithUnitOfWork(Action<IUnitOfWork> fn)
        {
            using (var uow = _container.GetInstance<IUnitOfWorkFactory>().Build())
            {
                fn(uow);
            }
        }

        #endregion

        public override void Dispose()
        {
            _connection.ActuallyDispose();
        }
    }
}