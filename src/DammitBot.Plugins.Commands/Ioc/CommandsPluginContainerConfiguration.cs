using DammitBot.Abstract;
using DammitBot.MessageHandlers;
using Lamar;

namespace DammitBot.Ioc;

public class CommandsPluginContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods

    public override void Configure(ServiceRegistry e)
    {
        e.For<IMessageHandlerAttributeComparer>().Use<CommandAwareMessageHandlerAttributeComparer>();
    }

    #endregion
}