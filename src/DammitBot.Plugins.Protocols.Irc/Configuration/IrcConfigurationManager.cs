using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration
{
    public class IrcConfigurationManager : ConfigurationManager, IIrcConfigurationManager
    {
        public IrcConfigurationManager(
            IConfigurationBuilder builder,
            ISettingsPathHelper settingsPathHelper)
            : base(builder, settingsPathHelper) {}

        #region Properties

        public virtual IIrcConfigurationSection IrcConfigurationSection
            => new IrcConfigurationSection(
                Configuration.GetSection(DammitBot.Configuration.IrcConfigurationSection.KEY));

        #endregion
    }
}