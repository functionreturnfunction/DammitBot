using System.IO;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        protected readonly ISettingsPathHelper _settingsPathHelper;
        protected readonly IConfigurationBuilder _builder;
 
        public IConfigurationRoot Configuration => _builder.Build();

        public IBotConfigurationSection BotConfig => new BotConfigurationSection(Configuration.GetSection(BotConfigurationSection.KEY));

        public virtual string SettingsPath => _settingsPathHelper.SettingsPath;

        public ConfigurationManager(IConfigurationBuilder builder, ISettingsPathHelper settingsPathHelper)
        {
            _settingsPathHelper = settingsPathHelper;
            _builder = builder.AddJsonFile(SettingsPath);
        }
    }
}