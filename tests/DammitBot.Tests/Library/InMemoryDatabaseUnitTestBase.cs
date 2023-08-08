using System;
using System.Data;
using DammitBot.IoC;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using Lamar;

namespace DammitBot.Library;

public abstract class InMemoryDatabaseUnitTestBase<TTarget> : UnitTestBase<TTarget>
{
    protected SqliteConnection? _connection;
    protected bool _migrationsRun;

    #region Private Methods

    protected override void ExtraSetup()
    {
        RunMigrations();
    }

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);
        _connection = new SqliteConnection(
            new Microsoft.Data.Sqlite.SqliteConnection(
                new SqliteInMemoryConnectionStringProvider().GetMainAppConnectionString()));
        _connection.Name = "InMemoryDatabaseTest";

        serviceRegistry.For<IInstantiationService>().Use<InstantiationService>();
        serviceRegistry.For<IAssemblyService>().Use<AssemblyService>();

        new SQLitePluginContainerConfiguration().Configure(serviceRegistry);
        new DapperPluginContainerConfiguration().Configure(serviceRegistry);
        new RemindersDapperPluginContainerConfiguration().Configure(serviceRegistry);

        serviceRegistry.For(typeof(IRepository<>)).Use(typeof(Repository<>));
        serviceRegistry.For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();
        serviceRegistry.For<IDbConnectionFactory>()
            .Use(_ => new TestDbConnectionFactory(_connection));
        serviceRegistry.For<IDbConnection>().Use(_connection);
        serviceRegistry.For<IUnitOfWork>().Use<TestDapperUnitOfWork>();
    }

    protected virtual void RunMigrations()
    {
        var migrationRunner = _container.GetInstance<MigrationRunner>();

        migrationRunner.Up(seed: false);
    }

    protected virtual void WithUnitOfWork(Action<IUnitOfWork> fn)
    {
        using var uow = _container.GetInstance<IUnitOfWorkFactory>().Build();
        fn(uow);
    }

    #endregion

    public override void Dispose()
    {
        _connection!.ActuallyDispose();
    }
}