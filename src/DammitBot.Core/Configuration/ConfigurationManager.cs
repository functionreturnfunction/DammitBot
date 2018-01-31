using System.IO;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        protected readonly IConfigurationBuilder _builder;

        public IConfigurationRoot Configuration => _builder.Build();

        public IBotConfigurationSection BotConfig => new BotConfigurationSection(Configuration.GetSection(BotConfigurationSection.KEY));

        public ConfigurationManager(IConfigurationBuilder builder)
        {
            _builder = builder.AddJsonFile(Path.GetFullPath(Path.Combine("..", "..", "..", "appsettings.json")));
        }
    }
}