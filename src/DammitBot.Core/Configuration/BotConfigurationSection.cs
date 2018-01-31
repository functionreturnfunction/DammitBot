using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration
{
    public class BotConfigurationSection : IBotConfigurationSection
    {
        public const string KEY = "DammitBot";

        private readonly IConfigurationSection _config;

        public string GoesBy => _config["goesBy"];

        public BotConfigurationSection(IConfigurationSection config)
        {
            _config = config;
        }
    }
}
