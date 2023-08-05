namespace DammitBot.Configuration;

/// <summary>
/// Provides the path at which the configuration file can be found.
/// </summary>
public interface ISettingsPathProvider
{
    /// <summary>
    /// Path at which the configuration file can be found.
    /// </summary>
    string SettingsPath { get; }
}