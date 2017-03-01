using DammitBot.Data.Library;
using DammitBot.Data.Models;
using NHibernate;
using StructureMap;

namespace DammitBot.Data.NHibernate.Library
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members

        private readonly ISession _session;
        private readonly IContainer _container;
        private readonly ITransaction _transaction;

        #endregion

        #region Constructors

        public UnitOfWork(ISessionFactory sessionFactory, IContainer container)
        {
            _session = sessionFactory.OpenSession();
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

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _session.Dispose();
            _container.Dispose();
        }

        #endregion
    }
}
