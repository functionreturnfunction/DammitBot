using System;
using System.Data;
using Lamar;

namespace DammitBot.Library;

/// <inheritdoc />
/// <remarks>
/// This implementation creates and injects an <see cref="IDbConnection"/> for sending data to and
/// retrieving data from the database, with the intention that <see cref="Dapper"/> extensions will be
/// called on it by repository implementations.
/// </remarks>
public class DapperUnitOfWork : IUnitOfWork
{
    #region Private Members
    
    private readonly IDbConnection _connection;
    
    #endregion
    
    #region Exposed Properties

    /// <summary>
    /// Implicit <see cref="IDbTransaction"/> which any data operations performed through this instance
    /// will be associated with.
    /// </summary>
    protected IDbTransaction Transaction { get; }
    /// <summary>
    /// <see cref="IContainer"/> instance with an injected <see cref="IDbConnection"/> for performing
    /// database operations.
    /// </summary>
    protected INestedContainer Container { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="DapperUnitOfWork"/> class.
    /// </summary>
    /// <param name="connectionFactory"></param>
    /// <param name="connectionStringProvider"></param>
    /// <param name="container"></param>
    public DapperUnitOfWork(
        IDbConnectionFactory connectionFactory,
        IConnectionStringProvider connectionStringProvider,
        IContainer container)
    {
        _connection = connectionFactory
            .Build(connectionStringProvider.GetMainAppConnectionString());
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        Transaction = _connection.BeginTransaction();
        Container = container.GetNestedContainer();
        Container.Inject(_connection);
    }

    #endregion

    #region Private Methods

    private T WithCommand<T>(Func<IDbCommand, T> fn)
    {
        using var cmd = _connection.CreateCommand();
        cmd.Transaction = Transaction;
        return fn(cmd);
    }

    private T WithTextCommand<T>(string sql, Func<IDbCommand, T> fn)
    {
        return WithCommand(cmd => {
            cmd.CommandText = sql;
            return fn(cmd);
        });
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    public IRepository<TEntity> GetEntityRepository<TEntity>()
        where TEntity : class
    {
        return GetRepository<IRepository<TEntity>, TEntity>();
    }

    /// <inheritdoc />
    public TRepository GetRepository<TRepository, TEntity>()
        where TRepository : IRepository<TEntity>
        where TEntity : class
    {
        return Container.GetInstance<TRepository>();
    }

    /// <inheritdoc />
    public virtual void Commit()
    {
        Transaction.Commit();
    }

    /// <inheritdoc />
    public virtual void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
        Container.Dispose();
        Transaction.Dispose();
    }

    /// <inheritdoc />
    public int ExecuteNonQuery(string sql)
    {
        return WithTextCommand(sql,
            cmd => cmd.ExecuteNonQuery());
    }

    /// <inheritdoc />
    public object? ExecuteScalar(string sql)
    {
        return WithTextCommand(sql,
            cmd => cmd.ExecuteScalar());
    }

    #endregion
}