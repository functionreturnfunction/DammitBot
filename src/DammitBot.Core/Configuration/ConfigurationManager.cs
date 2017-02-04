using System.Configuration;
using StructureMap;

namespace DammitBot.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        #region Properties

        public BotConfigurationSection BotConfig
            => GetSection<BotConfigurationSection>(BotConfigurationSection.SECTION_NAME);

        #endregion

        #region Private Methods

        protected TSection GetSection<TSection>(string name)
            where TSection : ConfigurationSection
        {
            return (TSection)System.Configuration.ConfigurationManager.GetSection(name);
        }

        #endregion
    }
}
