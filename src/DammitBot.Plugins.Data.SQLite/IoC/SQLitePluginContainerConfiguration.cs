using DammitBot.Abstract;
using DammitBot.Library;
using Lamar;

namespace DammitBot.IoC;

/// <inheritdoc />
/// <remarks>
/// This implementation registers types used to provide functionality to connect to and work with SQLite
/// databases.
/// </remarks>
public class SQLitePluginContainerConfiguration : ContainerConfigurationBase
{
    /// <inheritdoc />
    /// <inheritdoc cref="SQLitePluginContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.For<IDbConnectionFactory>().Use<SQLiteDbConnectionFactory>();
        e.For<IConnectionStringProvider>().Use<SQLiteConnectionStringProvider>();
    }
}