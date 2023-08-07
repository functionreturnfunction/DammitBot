using Lamar;

namespace DammitBot.Library;

public class TestDapperUnitOfWork : DapperUnitOfWork
{
    public TestDapperUnitOfWork(
        IDbConnectionFactory connectionFactory,
        IConnectionStringProvider connectionStringProvider,
        IContainer container)
        : base(connectionFactory, connectionStringProvider, container) {}

    public override void Dispose()
    {
        Container.Dispose();
        if (Transaction != null)
        {
            Transaction.Dispose();
        }
    }
}