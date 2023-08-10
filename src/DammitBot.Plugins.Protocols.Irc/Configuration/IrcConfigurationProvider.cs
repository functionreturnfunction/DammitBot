using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

/// <inheritdoc cref="IIrcConfigurationSection" />
public class IrcConfigurationProvider : ConfigurationProvider, IIrcConfigurationProvider
{
    #region Properties

    /// <inheritdoc />
    public virtual IIrcConfigurationSection IrcConfigurationSection
        => new IrcConfigurationSection(
            Configuration.GetSection(DammitBot.Configuration.IrcConfigurationSection.KEY));

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="IrcConfigurationProvider"/> class.
    /// </summary>
    public IrcConfigurationProvider(
        IConfigurationBuilder builder,
        ISettingsPathProvider settingsPathProvider)
        : base(builder, settingsPathProvider) {}
    
    #endregion
}