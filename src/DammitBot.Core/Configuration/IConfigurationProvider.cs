namespace DammitBot.Configuration;

/// <summary>
/// Provider of configuration data.  All configuration providers will include the base
/// <see cref="IBotConfigurationSection"/> for the bot itself.
/// </summary>
public interface IConfigurationProvider
{
    /// <inheritdoc cref="IBotConfigurationSection" />
    IBotConfigurationSection BotConfig { get; }
}