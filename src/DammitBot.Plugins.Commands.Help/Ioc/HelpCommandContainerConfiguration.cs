using DammitBot.Abstract;
using DammitBot.CommandHandlers;
using StructureMap;

namespace DammitBot.Ioc
{
    public class HelpCommandContainerConfiguration : ContainerConfigurationBase
    {
        #region Exposed Methods

        public override void Configure(ConfigurationExpression e)
        {
            e.For<ICommandHandlerRepository>().Use<UnknownCommandHandlerAwareCommandHandlerRepository>();
        }

        #endregion
    }
}
