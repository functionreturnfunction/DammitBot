using DammitBot.Abstract;
using DammitBot.MessageHandlers;
using StructureMap;

namespace DammitBot.Ioc
{
    public class CommandsPluginContainerConfiguration : PluginContainerConfigurationBase
    {
        #region Exposed Methods

        public override void Configure(ConfigurationExpression e)
        {
            e.For<IMessageHandlerAttributeService>().Use<CommandAwareMessageHandlerAttributeService>();
        }

        #endregion
    }
}
