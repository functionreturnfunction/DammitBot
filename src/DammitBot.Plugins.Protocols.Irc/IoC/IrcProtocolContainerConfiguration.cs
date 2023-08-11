using DammitBot.Abstract;
using DammitBot.Configuration;
using Lamar;
using Microsoft.Extensions.DependencyInjection;

namespace DammitBot.IoC;

/// <inheritdoc/>
/// <remarks>
/// This implementation configures types necessary for handling the Irc protocol.
/// </remarks>
public class IrcProtocolContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods
    
    /// <inheritdoc/>
    /// <inheritdoc cref="IrcProtocolContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.AddOptions<IrcConfiguration>()
            .BindConfiguration("Irc")
            .ValidateDataAnnotations();
    }
    
    #endregion
}