using DammitBot.Abstract;
using DammitBot.CommandHandlers;
using Lamar;

namespace DammitBot.Ioc;

/// <inheritdoc />
/// <remarks>
/// This implementation registers types used in providing helpful information to users attempting to use
/// commands.
/// </remarks>
public class HelpCommandContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods

    /// <inheritdoc />
    /// <inheritdoc cref="HelpCommandContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.For<ICommandHandlerTypeService>()
            .Use<UnknownCommandHandlerTypeAwareCommandHandlerTypeService>();
    }

    #endregion
}