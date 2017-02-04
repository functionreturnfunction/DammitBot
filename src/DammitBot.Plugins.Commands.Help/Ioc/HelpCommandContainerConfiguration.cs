using DammitBot.Abstract;
using DammitBot.CommandHandlers;
using StructureMap;

namespace DammitBot.Ioc
{
    public class HelpCommandContainerConfiguration : PluginContainerConfigurationBase
    {
        #region Exposed Methods

        public override void Configure(ConfigurationExpression e)
        {
            e.For<ICommandHandlerRepository>().Use<UnknownCommandHandlerAwareCommandHandlerRepository>();
        }

        #endregion
    }
}
