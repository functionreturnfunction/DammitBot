using System.Configuration;

namespace DammitBot.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        public BotConfigurationSection BotConfig
            => GetSection<BotConfigurationSection>(BotConfigurationSection.SECTION_NAME);

        #region Private Methods

        protected TSection GetSection<TSection>(string name)
            where TSection : ConfigurationSection
        {
            return (TSection)System.Configuration.ConfigurationManager.GetSection(name);
        }

        #endregion
    }
}