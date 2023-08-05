using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

/// <inheritdoc cref="IBotConfigurationSection"/>
public class BotConfigurationSection : IBotConfigurationSection
{
    #region Constants
    
    /// <summary>
    /// Key at which this configuration section can be found.
    /// </summary>
    public const string KEY = "DammitBot";
    
    #endregion
    
    #region Private Members

    private readonly IConfigurationSection _config;
    
    #endregion
    
    #region Properties

    /// <inheritdoc cref="IBotConfigurationSection.GoesBy"/>
    public string GoesBy => _config.EnsureConfigValue("goesBy");
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="BotConfigurationSection"/> class.
    /// </summary>
    public BotConfigurationSection(IConfigurationSection config)
    {
        _config = config;
    }
    
    #endregion
}