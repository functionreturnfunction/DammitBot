using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration
{
    public class BotConfigurationSection : IBotConfigurationSection
    {
        public const string KEY = "DammitBot";

        private readonly IConfigurationSection _config;

        public string GoesBy =>
            _config["goesBy"] ??
            throw new ConfigurationErrorsException("Configuration key 'goesBy' is missing");

        public BotConfigurationSection(IConfigurationSection config)
        {
            _config = config;
        }
    }
}
