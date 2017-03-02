using DammitBot.Configuration;
using DammitBot.Protocols.Irc.Configuration;

namespace DammitBot.Protocols.Irc.Wrappers
{
    public interface IIrcClientFactory
    {
        #region Abstract Methods

        IIrcClient Build(IrcConfigurationSection config);

        #endregion
    }
}