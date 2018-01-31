using System.Data;
using DammitBot.Data.Dapper.Library;
using StructureMap;

namespace DammitBot.TestLibrary
{
    public class TestDisposableUnitOfWork : DisposableUnitOfWork
    {
        public TestDisposableUnitOfWork(IDbConnection connection, IContainer container) : base(connection, container) {}

        public override void Dispose()
        {
            _container.Dispose();
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
        }
    }
}