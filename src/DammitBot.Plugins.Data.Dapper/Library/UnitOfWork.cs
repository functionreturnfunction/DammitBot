using System.Data;
using DammitBot.Data.Library;
using StructureMap;

namespace DammitBot.Data.Dapper.Library
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members

        protected readonly IDbConnection _connection;
        protected readonly IContainer _container;

        #endregion

        #region Constructors

        public UnitOfWork(IDbConnection connection, IContainer container)
        {
            _connection = connection;
            _container = container;
        }

        #endregion

        #region Exposed Methods

        public virtual IDisposableUnitOfWork Start()
        {
            return new DisposableUnitOfWork(_connection, _container);
        }

        #endregion
    }
}
