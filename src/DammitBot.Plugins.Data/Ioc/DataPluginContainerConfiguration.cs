using DammitBot.Abstract;
using DammitBot.Library;
using Lamar;

namespace DammitBot.Ioc;

/// <inheritdoc />
/// <remarks>
/// This implementation registers types used to provide persistent data handling to bot plugins.
/// </remarks>
public class DataPluginContainerConfiguration : ContainerConfigurationBase
{
    /// <inheritdoc/>
    /// <inheritdoc cref="DataPluginContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();
        e.For(typeof(IRepository<>)).Use(typeof(Repository<>));
    }
}