using System.Data;
using DammitBot.Data.Library;
using NHibernate;
using StructureMap;

namespace DammitBot.Data.NHibernate.Library
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members

        private readonly ISessionFactory _sessionFactory;
        private readonly IDbConnection _connection;
        private readonly IContainer _container;

        #endregion

        #region Constructors

        public UnitOfWork(ISessionFactory sessionFactory, IDbConnection connection, IContainer container)
        {
            _sessionFactory = sessionFactory;
            _connection = connection;
            _container = container;
        }

        #endregion

        #region Exposed Methods

        public IDisposableUnitOfWork Start()
        {
            return new DisposableUnitOfWork(_sessionFactory.OpenSession(_connection), _container);
        }

        #endregion
    }
}
