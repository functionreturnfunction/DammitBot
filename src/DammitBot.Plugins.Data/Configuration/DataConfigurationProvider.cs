using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

/// <summary>
/// Provider of configuration data specific to dealing with persistence, specifically a
/// <see cref="ConnectionString"/>.
/// </summary>
public class DataConfigurationProvider : ConfigurationProvider, IDataConfigurationProvider
{
    #region Constructors
    
    /// <summary>
    /// Constructor for the <see cref="DataConfigurationProvider"/> class.
    /// </summary>
    public DataConfigurationProvider(
        IConfigurationBuilder builder,
        ISettingsPathProvider settingsPathProvider)
        : base(builder, settingsPathProvider) {}

    #endregion
    
    #region Properties

    /// <summary>
    /// Connection string value configured for use within the application.
    /// </summary>
    public string ConnectionString => Configuration
        .EnsureConfigSection("Data")
        .EnsureConfigValue("connectionString");

    #endregion
}