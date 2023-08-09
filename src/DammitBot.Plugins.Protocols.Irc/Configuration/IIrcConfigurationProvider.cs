namespace DammitBot.Configuration;

/// <inheritdoc />
/// <remarks>
/// This implementation provides an <see cref="IIrcConfigurationSection"/>, which contains configuration
/// values for connecting to an Irc server and some of its channels.
/// </remarks>
public interface IIrcConfigurationProvider : IConfigurationProvider
{
    #region Abstract Properties

    /// <summary>
    /// Configuration section
    /// </summary>
    IIrcConfigurationSection IrcConfigurationSection { get; }

    #endregion
}