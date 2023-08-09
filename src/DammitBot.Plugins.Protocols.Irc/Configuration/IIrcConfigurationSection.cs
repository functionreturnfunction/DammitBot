namespace DammitBot.Configuration;

/// <summary>
/// Configuration section containing values for connecting to an Irc <see cref="Server"/> and its
/// <see cref="Channels"/>. 
/// </summary>
public interface IIrcConfigurationSection
{
    /// <summary>
    /// Hostname of the server to connect to.
    /// </summary>
    string Server { get; }
    /// <summary>
    /// Nickname to use when connecting.
    /// </summary>
    string Nick { get; }
    /// <summary>
    /// User name to use when connecting.
    /// </summary>
    string User { get; }
    /// <summary>
    /// Channels to join after successfully connecting.
    /// </summary>
    string[] Channels { get; }
}