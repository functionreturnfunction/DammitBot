using DammitBot.Abstract;
using DammitBot.CommandHandlers;
using Lamar;

namespace DammitBot.Ioc;

public class HelpCommandContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods

    public override void Configure(ServiceRegistry e)
    {
        e.For<ICommandHandlerTypeService>().Use<UnknownCommandHandlerTypeAwareCommandHandlerTypeService>();
    }

    #endregion
}