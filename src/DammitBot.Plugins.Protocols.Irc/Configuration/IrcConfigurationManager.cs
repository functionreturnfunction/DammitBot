// ReSharper disable once CheckNamespace
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration
{
    public class IrcConfigurationManager : ConfigurationManager, IIrcConfigurationManager
    {
        public IrcConfigurationManager(IConfigurationBuilder builder) : base(builder) {}

        #region Properties

        public virtual IIrcConfigurationSection IrcConfigurationSection
            => new IrcConfigurationSection(Configuration.GetSection(DammitBot.Configuration.IrcConfigurationSection.KEY));

        #endregion
    }
}