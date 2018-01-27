using System.Data;
using DammitBot.Data.Library;
using StructureMap;

namespace DammitBot.Data.Dapper.Library
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members

        private readonly IDbConnection _connection;
        private readonly IContainer _container;

        #endregion

        #region Constructors

        public UnitOfWork(IDbConnection connection, IContainer container)
        {
            _connection = connection;
            _container = container;
        }

        #endregion

        #region Exposed Methods

        public IDisposableUnitOfWork Start()
        {
            return new DisposableUnitOfWork(_connection, _container);
        }

        #endregion
    }
}
