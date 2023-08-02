using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

public class DataConfigurationManager : ConfigurationManager, IDataConfigurationManager
{
    public DataConfigurationManager(
        IConfigurationBuilder builder,
        ISettingsPathHelper settingsPathHelper)
        : base(builder, settingsPathHelper) {}

    #region Properties

    public string ConnectionString => Configuration
        .EnsureConfigSection("Dapper")
        .EnsureConfigValue("connection");

    #endregion
}