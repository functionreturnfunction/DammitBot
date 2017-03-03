// ReSharper disable once CheckNamespace
namespace DammitBot.Configuration
{
    public class IrcConfigurationManager : ConfigurationManager, IIrcConfigurationManager
    {
        #region Properties

        public IrcConfigurationSection IrcConfigurationSection
            => GetSection<IrcConfigurationSection>(IrcConfigurationSection.SECTION_NAME);

        #endregion
    }
}