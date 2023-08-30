using DammitBot.Abstract;
using DammitBot.Configuration;
using Lamar;
using Microsoft.Extensions.DependencyInjection;

namespace DammitBot.IoC;

/// <inheritdoc/>
/// <remarks>
/// This implementation configures types necessary for handling the Slack protocol.
/// </remarks>
public class SlackProtocolContainerConfiguration : ContainerConfigurationBase
{
    /// <inheritdoc/>
    /// <inheritdoc cref="SlackProtocolContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.AddOptions<SlackConfiguration>()
            .BindConfiguration("Slack")
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}