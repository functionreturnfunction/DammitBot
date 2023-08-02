using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace DammitBot.Library;

// Dommel guesses the connection type based on type name,
// so our wrapper class needs to have the same name.
public abstract class DbConnectionWrapper<TConnection> : IDbConnection
    where TConnection : class, IDbConnection
{
    protected readonly TConnection _connection;

    public DbConnectionWrapper(TConnection connection)
    {
        _connection = connection;
    }

    public string? Name { get; set; }

    [AllowNull]
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

// Dommel guesses the connection type based on type name,
// so our wrapper class needs to have the same name.
public class SqliteConnection : DbConnectionWrapper<Microsoft.Data.Sqlite.SqliteConnection>
{
    public SqliteConnection(Microsoft.Data.Sqlite.SqliteConnection connection) : base(connection) { }
}