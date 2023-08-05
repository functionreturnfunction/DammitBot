using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

public class IrcConfigurationProvider : ConfigurationProvider, IIrcConfigurationProvider
{
    public IrcConfigurationProvider(
        IConfigurationBuilder builder,
        ISettingsPathProvider settingsPathProvider)
        : base(builder, settingsPathProvider) {}

    #region Properties

    public virtual IIrcConfigurationSection IrcConfigurationSection
        => new IrcConfigurationSection(
            Configuration.GetSection(DammitBot.Configuration.IrcConfigurationSection.KEY));

    #endregion
}