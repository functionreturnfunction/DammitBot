using DammitBot.Configuration;

namespace DammitBot.Protocols.Irc.Configuration
{
    public class IrcConfigurationManager : ConfigurationManager, IIrcConfigurationManager
    {
        public IrcConfigurationSection IrcConfigurationSection
            => GetSection<IrcConfigurationSection>(IrcConfigurationSection.SECTION_NAME);
    }
}