using DammitBot.Protocols.Irc;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

public class IrcConfigurationSection : IIrcConfigurationSection
{
    #region Constants

    public const string KEY = Irc.PROTOCOL_NAME;
    private IConfigurationSection _config;

    public IrcConfigurationSection(IConfigurationSection config)
    {
        _config = config;
    }


    public struct Keys
    {
        #region Constants

        public const string SERVER = "server", NICK = "nick", USER = "user", CHANNELS = "channels";

        #endregion
    }

    #endregion

    #region Properties

    public virtual string Server => _config.EnsureConfigValue(Keys.SERVER);

    public virtual string Nick
    {
        get
        {
            var nick = _config[Keys.NICK];
            return string.IsNullOrWhiteSpace(nick) ? User : nick;
        }
    }

    public virtual string User => _config.EnsureConfigValue(Keys.USER);

    public virtual string[] Channels => _config.EnsureConfigValue(Keys.CHANNELS).Split(',');

    #endregion
}