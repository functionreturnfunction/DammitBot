using System;
using System.Data;
using DammitBot.Data.Library;
using StructureMap;

namespace DammitBot.Data.Dapper.Library
{
    public class DisposableUnitOfWork : IDisposableUnitOfWork
    {
        #region Private Members

        private readonly IContainer _container;
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        #endregion

        #region Constructors

        public DisposableUnitOfWork(IDbConnection connection, IContainer container)
        {
            _connection = connection;
            _connection.Open();
            _container = container.GetNestedContainer();
            _container.Configure(e => {
                e.For<IDbConnection>().Use(_connection);
            });
            _transaction = _connection.BeginTransaction();
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
            _transaction.Commit();
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
            _container.Dispose();
            _transaction.Dispose();
        }

        public int ExecuteNonQuery(string sql)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteNonQuery();
        }

        public object ExecuteScalar(string sql)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteScalar();
        }

        public IDisposableUnitOfWork Start()
        {
            throw new InvalidOperationException("Already started!");
        }

        #endregion
    }
}
