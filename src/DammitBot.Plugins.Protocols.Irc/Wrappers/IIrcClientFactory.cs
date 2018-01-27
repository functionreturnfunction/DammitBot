using DammitBot.Configuration;

namespace DammitBot.Protocols.Irc.Wrappers
{
    public interface IIrcClientFactory
    {
        #region Abstract Methods

        IIrcClient Build(IrcConfigurationSection config);

        #endregion
    }
}