using System.Diagnostics.CodeAnalysis;
using ChatSharp;
using DammitBot.Configuration;

namespace DammitBot.Wrappers;

[ExcludeFromCodeCoverage]
public class IrcClientFactory : IIrcClientFactory
{
    #region Exposed Methods

    public IIrcClient Build(IIrcConfigurationSection config)
    {
        return new IrcClientWrapper(
            new IrcClient(config.Server, new IrcUser(config.Nick, config.User)));
    }

    #endregion
}