using System.Data;
using DammitBot.Data.Library;
using DammitBot.Data.Dapper.Library;
using StructureMap;

namespace DammitBot.TestLibrary
{
    public class TestUnitOfWork : UnitOfWork
    {
        public TestUnitOfWork(IDbConnection connection, IContainer container) : base(connection, container) {}

        public override IDisposableUnitOfWork Start()
        {
            return new TestDisposableUnitOfWork(_connection, _container);
        }
    }
}