using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

/// <inheritdoc cref="IDataConfigurationProvider"/>
public class DataConfigurationProvider : ConfigurationProvider, IDataConfigurationProvider
{
    #region Constructors
    
    /// <summary>
    /// Constructor for the <see cref="DataConfigurationProvider"/> class.
    /// </summary>
    public DataConfigurationProvider(IConfiguration configuration) : base(configuration) {}

    #endregion
    
    #region Properties

    /// <inheritdoc />
    public string ConnectionString => Configuration
        .GetRequiredSection("Data")
        .EnsureConfigValue("connectionString");

    #endregion
}