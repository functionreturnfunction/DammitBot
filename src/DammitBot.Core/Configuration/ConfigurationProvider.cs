using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

/// <inheritdoc cref="IConfigurationProvider"/>
public class ConfigurationProvider : IConfigurationProvider
{
    #region Private Members
    
    private readonly IConfigurationBuilder _builder;
    
    #endregion
    
    #region Private Properties
 
    /// <summary>
    /// <see cref="IConfigurationRoot"/> from which specific <see cref="IConfigurationSection"/>s can be
    /// provided.
    /// </summary>
    public IConfigurationRoot Configuration => _builder.Build();
    
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
    public ConfigurationProvider(IConfigurationBuilder builder, ISettingsPathProvider settingsPathProvider)
    {
        _builder = builder.AddJsonFile(settingsPathProvider.SettingsPath);
    }
    
    #endregion
}