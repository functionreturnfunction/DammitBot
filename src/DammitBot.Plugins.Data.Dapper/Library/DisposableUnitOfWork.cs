using System;
using System.Data;
using DammitBot.Data.Library;
using StructureMap;

namespace DammitBot.Data.Dapper.Library
{
    public class DisposableUnitOfWork : IDisposableUnitOfWork
    {
        #region Private Members

        protected readonly IContainer _container;
        protected readonly IDbConnection _connection;
        protected readonly IDbTransaction _transaction;

        #endregion

        #region Constructors

        public DisposableUnitOfWork(IDbConnection connection, IContainer container)
        {
            _connection = connection;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _container = container.GetNestedContainer();
            _container.Configure(e => {
                e.For<IDbConnection>().Use(_connection);
            });
            // _transaction = _connection.BeginTransaction();
        }

        #endregion

        #region Private Methods

        private T WithCommand<T>(Func<IDbCommand, T> fn)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.Transaction = _transaction;
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
            return _container.GetInstance<IRepository<TEntity>>();
        }

        public virtual void Commit()
        {
            // _transaction.Commit();
        }

        public virtual void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
            _container.Dispose();
            _transaction.Dispose();
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

        public IDisposableUnitOfWork Start()
        {
            throw new InvalidOperationException("Already started!");
        }

        #endregion
    }
}
