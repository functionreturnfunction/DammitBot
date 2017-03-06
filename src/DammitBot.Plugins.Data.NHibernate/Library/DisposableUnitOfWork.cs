using System;
using DammitBot.Data.Library;
using NHibernate;
using StructureMap;

namespace DammitBot.Data.NHibernate.Library
{
    public class DisposableUnitOfWork : IDisposableUnitOfWork
    {
        #region Private Members

        private readonly IContainer _container;
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        #endregion

        #region Constructors

        public DisposableUnitOfWork(ISession session, IContainer container)
        {
            _session = session;
            _container = container.GetNestedContainer();
            _container.Configure(e => {
                e.For<ISession>().Use(_session);
            });
            _session.FlushMode = FlushMode.Commit;
            _transaction = _session.BeginTransaction();
        }

        #endregion

        #region Exposed Methods

        public IRepository<TEntity> GetRepository<TEntity>()
        {
            return _container.GetInstance<IRepository<TEntity>>();
        }

        public virtual void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _session.Dispose();
            _container.Dispose();
        }

        public IDisposableUnitOfWork Start()
        {
            throw new InvalidOperationException("Already started!");
        }

        #endregion
    }
}