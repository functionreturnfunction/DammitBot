using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace DammitBot.TestLibrary
{
    public abstract partial class InMemoryDatabaseUnitTestBase<TTarget>
    {
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

            protected T TryExecuteThing<T>(string executionType, Func<T> fn)
            {
                // try
                // {
                    return fn();
                // }
                // catch (Exception e)
                // {
                //     throw new Exception($"Error executing {executionType} query '{_command.CommandText}'.", e);
                // }
            }

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
                return TryExecuteThing("non", () => _command.ExecuteNonQuery());
            }

            public IDataReader ExecuteReader()
            {
                return TryExecuteThing("reader", () => _command.ExecuteReader());
            }

            public IDataReader ExecuteReader(CommandBehavior behavior)
            {
                return TryExecuteThing("reader", () => _command.ExecuteReader(behavior));
            }

            public object ExecuteScalar()
            {
                return TryExecuteThing("scalar", () => _command.ExecuteScalar());
            }

            public void Prepare()
            {
                _command.Prepare();
            }
        }
    }
}