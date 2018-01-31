using System.Data;
using DammitBot.Data.Library;
using StructureMap;

namespace DammitBot.Data.Dapper.Library
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members

        protected readonly IDbConnectionFactory _connectionFactory;
        protected readonly IConnectionStringService _connectionStringService;
        protected readonly IContainer _container;

        #endregion

        #region Constructors

        public UnitOfWork(IDbConnectionFactory connectionFactory, IConnectionStringService connectionStringService, IContainer container)
        {
            _connectionFactory = connectionFactory;
            _connectionStringService = connectionStringService;
            _container = container;
        }

        #endregion

        #region Exposed Methods

        public virtual IDisposableUnitOfWork Start()
        {
            return new DisposableUnitOfWork(_connectionFactory.Build(_connectionStringService.GetMainAppConnectionString()), _container);
        }

        #endregion
    }
}
