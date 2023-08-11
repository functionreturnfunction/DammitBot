using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

/// <inheritdoc cref="IConfigurationProvider"/>
public class ConfigurationProvider : IConfigurationProvider
{
    #region Private Properties
 
    /// <summary>
    /// <see cref="IConfiguration"/> from which specific <see cref="IConfigurationSection"/>s can be
    /// provided.
    /// </summary>
    protected IConfiguration Configuration { get; }
    
    #endregion
    
    #region Exposed Properties

    /// <inheritdoc cref="IConfigurationProvider.BotConfig"/>
    public IBotConfigurationSection BotConfig =>
        new BotConfigurationSection(Configuration.GetSection(BotConfigurationSection.KEY));
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="ConfigurationProvider"/> class.
    /// </summary>
    // needs to be public for Lamar
    // ReSharper disable once MemberCanBeProtected.Global
    public ConfigurationProvider(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    #endregion
}