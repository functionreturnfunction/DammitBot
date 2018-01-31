using System;
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
        protected DbConnectionWrapper _connection;

        #region Private Methods

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            _connection = new DbConnectionWrapper(new SqliteConnection(new SqliteInMemoryConnectionStringService().GetMainAppConnectionString()));
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

        public class DbConnectionWrapper : IDbConnection
        {
            protected readonly IDbConnection _connection;

            public DbConnectionWrapper(IDbConnection connection)
            {
                _connection = connection;
            }

            public string Name { get; set; }

            public string ConnectionString
            {
                 get => _connection.ConnectionString;
                 set => _connection.ConnectionString = value;
            }

            public int ConnectionTimeout => _connection.ConnectionTimeout;

            public string Database => _connection.Database;

            public ConnectionState State => _connection.State;

            public IDbTransaction BeginTransaction()
            {
                return _connection.BeginTransaction();
            }

            public IDbTransaction BeginTransaction(IsolationLevel il)
            {
                return _connection.BeginTransaction(il);
            }

            public void ChangeDatabase(string databaseName)
            {
                _connection.ChangeDatabase(databaseName);
            }

            public void Close()
            {
                _connection.Close();
            }

            public IDbCommand CreateCommand()
            {
                return new DbCommandWrapper(_connection.CreateCommand());
            }

            public void Open()
            {
                _connection.Open();
            }

            public void Dispose()
            {
            }

            public void ActuallyDispose()
            {
                _connection.Dispose();
            }
        }

        public class DbCommandWrapper : IDbCommand
        {
            protected readonly IDbCommand _command;

            public DbCommandWrapper(IDbCommand command)
            {
                _command = command;
            }

            public string CommandText
            {
                get => _command.CommandText;
                set => _command.CommandText = value;
            }
            public int CommandTimeout { get => _command.CommandTimeout; set => _command.CommandTimeout = value; }
            public CommandType CommandType { get => _command.CommandType; set => _command.CommandType = value; }
            public IDbConnection Connection { get => _command.Connection; set => _command.Connection = value; }

            public IDataParameterCollection Parameters => _command.Parameters;

            public IDbTransaction Transaction { get => _command.Transaction; set => _command.Transaction = value; }
            public UpdateRowSource UpdatedRowSource { get => _command.UpdatedRowSource; set => _command.UpdatedRowSource = value; }

            public void Cancel()
            {
                _command.Cancel();
            }

            public IDbDataParameter CreateParameter()
            {
                return _command.CreateParameter();
            }

            public void Dispose()
            {
                _command.Dispose();
            }

            public int ExecuteNonQuery()
            {
                return _command.ExecuteNonQuery();
            }

            public IDataReader ExecuteReader()
            {
                return _command.ExecuteReader();
            }

            public IDataReader ExecuteReader(CommandBehavior behavior)
            {
                return _command.ExecuteReader(behavior);
            }

            public object ExecuteScalar()
            {
                return _command.ExecuteScalar();
            }

            public void Prepare()
            {
                _command.Prepare();
            }
        }
    }
}