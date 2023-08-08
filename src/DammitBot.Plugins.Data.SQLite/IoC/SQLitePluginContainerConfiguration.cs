using DammitBot.Abstract;
using DammitBot.Library;
using Lamar;

namespace DammitBot.IoC;

public class SQLitePluginContainerConfiguration : ContainerConfigurationBase
{
    public override void Configure(ServiceRegistry e)
    {
        e.For<IDbConnectionFactory>().Use<SqliteDbConnectionFactory>();
        e.For<IConnectionStringProvider>().Use<SqliteConnectionStringProvider>();
    }
}