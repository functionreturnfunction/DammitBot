using System;
using System.Data;
using StructureMap;

namespace DammitBot.Library;

public class DapperUnitOfWork : IUnitOfWork
{
    #region Exposed Properties

    public IDbConnection Connection { get; }
    public IDbTransaction Transaction { get; }
    public IContainer Container { get; }

    #endregion

    #region Constructors

    public DapperUnitOfWork(
        IDbConnectionFactory connectionFactory,
        IConnectionStringService connectionStringService,
        IContainer container)
    {
        Connection = connectionFactory
            .Build(connectionStringService.GetMainAppConnectionString());
        if (Connection.State != ConnectionState.Open)
        {
            Connection.Open();
        }
        Transaction = Connection.BeginTransaction();
        Container = container.GetNestedContainer();
        Container.Configure(e => {
            e.For<IDbConnection>().Use(Connection);
        });
    }

    #endregion

    #region Private Methods

    private T WithCommand<T>(Func<IDbCommand, T> fn)
    {
        using (var cmd = Connection.CreateCommand())
        {
            cmd.Transaction = Transaction;
            return fn(cmd);
        }
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

    public IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class
    {
        return Container.GetInstance<IRepository<TEntity>>();
    }

    public virtual void Commit()
    {
        Transaction.Commit();
    }

    public virtual void Dispose()
    {
        Connection.Close();
        Connection.Dispose();
        Container.Dispose();
        Transaction.Dispose();
    }

    public int ExecuteNonQuery(string sql)
    {
        return WithTextCommand(sql,
            cmd => cmd.ExecuteNonQuery());
    }

    public object ExecuteScalar(string sql)
    {
        return WithTextCommand(sql,
            cmd => cmd.ExecuteScalar());
    }

    public IDataReader ExecuteReader(string sql)
    {
        return WithTextCommand(sql,
            cmd => cmd.ExecuteReader());
    }

    public IUnitOfWork Start()
    {
        throw new InvalidOperationException("Already started!");
    }


    #endregion
}