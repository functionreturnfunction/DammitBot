using System.Diagnostics.CodeAnalysis;
using ChatSharp;
using DammitBot.Configuration;
using DammitBot.Protocols.Irc.Configuration;
using DammitBot.Wrappers;

namespace DammitBot.Protocols.Irc.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class IrcClientFactory : IIrcClientFactory
    {
        #region Exposed Methods

        public IIrcClient Build(IrcConfigurationSection config)
        {
            return new IrcClientWrapper(new IrcClient(config.Server, new IrcUser(config.Nick, config.User)));
        }

        #endregion
    }
}