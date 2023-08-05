using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

public class DataConfigurationProvider : ConfigurationProvider, IDataConfigurationManager
{
    public DataConfigurationProvider(
        IConfigurationBuilder builder,
        ISettingsPathProvider settingsPathProvider)
        : base(builder, settingsPathProvider) {}

    #region Properties

    public string ConnectionString => Configuration
        .EnsureConfigSection("Dapper")
        .EnsureConfigValue("connection");

    #endregion
}