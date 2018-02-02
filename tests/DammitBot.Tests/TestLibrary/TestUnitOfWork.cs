using System.Data;
using DammitBot.Data.Library;
using DammitBot.Data.Dapper.Library;
using StructureMap;

namespace DammitBot.TestLibrary
{
    public class TestUnitOfWork : UnitOfWork
    {
        public TestUnitOfWork(IDbConnectionFactory connectionFactory, IConnectionStringService connectionStringService, IContainer container) : base(connectionFactory, connectionStringService, container) {}

        public override void Dispose()
        {
            Container.Dispose();
            if (Transaction != null)
            {
                Transaction.Dispose();
            }
        }
    }
}