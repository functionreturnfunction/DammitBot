using DammitBot.Abstract;
using DammitBot.MessageHandlers;
using Lamar;

namespace DammitBot.Ioc;

/// <inheritdoc />
/// <remarks>
/// This implementation registers types used in processing messages which are considered to be bot
/// commands.
/// </remarks>
public class CommandsPluginContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods

    /// <inheritdoc />
    /// <inheritdoc cref="CommandsPluginContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.For<IMessageHandlerAttributeComparer>().Use<CommandAwareMessageHandlerAttributeComparer>();
    }

    #endregion
}