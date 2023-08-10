using DammitBot.Abstract;
using DammitBot.Library;
using DammitBot.Wrappers;
using Lamar;

namespace DammitBot.IoC;

/// <inheritdoc />
/// <remarks>
/// This implementation registers types used to provide support for the Irc protocol using the 3rd party
/// library IrcDotNet.
/// </remarks>
public class IrcProtocolContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods
    
    /// <inheritdoc />
    /// <inheritdoc cref="IrcProtocolContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.For<IIrcClient>().Use<IrcClientWrapper>();
    }
    
    #endregion
}