using DammitBot.Protocols;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

/// <inheritdoc />
public class IrcConfigurationSection : IIrcConfigurationSection
{
    #region Constants

    /// <summary>
    /// Key value used to look this configuration up.
    /// </summary>
    public const string KEY = Irc.PROTOCOL_NAME;

    /// <summary>
    /// Keys of the various properties this configuration supports/requires.
    /// </summary>
    public struct Keys
    {
        #region Constants

        /// <inheritdoc cref="Keys" />
        public const string
            SERVER = "server",
            NICK = "nick",
            USER = "user",
            CHANNELS = "channels";

        #endregion
    }
    
    #endregion
    
    #region Private Members
    
    private readonly IConfigurationSection _config;
    
    #endregion

    #region Properties

    /// <inheritdoc />
    public virtual string Server => _config.EnsureConfigValue(Keys.SERVER);

    /// <inheritdoc />
    public virtual string Nick
    {
        get
        {
            var nick = _config[Keys.NICK];
            return string.IsNullOrWhiteSpace(nick) ? User : nick;
        }
    }

    /// <inheritdoc />
    public virtual string User => _config.EnsureConfigValue(Keys.USER);

    /// <inheritdoc />
    public virtual string[] Channels => _config.EnsureConfigValue(Keys.CHANNELS).Split(',');

    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="IrcConfigurationSection"/> class.
    /// </summary>
    public IrcConfigurationSection(IConfigurationSection config)
    {
        _config = config;
    }

    #endregion
}