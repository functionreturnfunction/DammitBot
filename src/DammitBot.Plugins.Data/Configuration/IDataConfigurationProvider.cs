namespace DammitBot.Configuration;

/// <summary>
/// Provider of configuration data specific to dealing with persistence, specifically a
/// <see cref="ConnectionString"/>.
/// </summary>
public interface IDataConfigurationProvider : IConfigurationProvider
{
    /// <summary>
    /// Connection string value configured for use within the application.
    /// </summary>
    string ConnectionString { get; }
}