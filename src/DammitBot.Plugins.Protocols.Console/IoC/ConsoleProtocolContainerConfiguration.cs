using DammitBot.Abstract;
using DammitBot.Utilities;
using Lamar;

namespace DammitBot.IoC;

/// <inheritdoc />
/// <remarks>
/// This implementation registers types used to provide support for Console I/O as a messaging protocol.
/// </remarks>
public class ConsoleProtocolContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods
    
    /// <inheritdoc />
    /// <inheritdoc cref="ConsoleProtocolContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.For<IConsoleMainLoop>().Use<ConsoleMainLoop>().Singleton();
        e.For<IMainLoop>().Use(ctx => ctx.GetInstance<IConsoleMainLoop>());
    }
    
    #endregion
}