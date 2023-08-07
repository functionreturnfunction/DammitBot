using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

/// <inheritdoc cref="IDataConfigurationProvider"/>
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

    /// <inheritdoc />
    public string ConnectionString => Configuration
        .EnsureConfigSection("Data")
        .EnsureConfigValue("connectionString");

    #endregion
}