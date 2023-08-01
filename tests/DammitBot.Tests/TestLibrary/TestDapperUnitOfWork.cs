using DammitBot.Library;
using StructureMap;

namespace DammitBot.TestLibrary
{
    public class TestDapperUnitOfWork : DapperUnitOfWork
    {
        public TestDapperUnitOfWork(IDbConnectionFactory connectionFactory, IConnectionStringService connectionStringService, IContainer container) : base(connectionFactory, connectionStringService, container) {}

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