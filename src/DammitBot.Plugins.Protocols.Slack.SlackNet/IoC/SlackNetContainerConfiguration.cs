using DammitBot.Abstract;
using DammitBot.Library;
using DammitBot.Wrappers;
using Lamar;

namespace DammitBot.IoC;

/// <inheritdoc />
/// <remarks>
/// This implementation registers types used to provide support for the Slack protocol using the 3rd party
/// library SlackNet.
/// </remarks>
public class SlackNetContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods
    
    /// <inheritdoc />
    /// <inheritdoc cref="SlackNetContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.For<ISlackClientFactory>().Use<SlackServiceBuilderWrapper>();
    }
    
    #endregion
}