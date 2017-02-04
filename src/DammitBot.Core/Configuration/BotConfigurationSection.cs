using System.ComponentModel;
using System.Configuration;

namespace DammitBot.Configuration
{
    public class BotConfigurationSection : ConfigurationSection
    {
        #region Constants

        public const string SECTION_NAME = "DammitBot";

        public struct Keys
        {
            #region Constants

            public const string SERVER = "server", NICK = "nick", USER = "user", CHANNEL = "channel", GOES_BY = "goesBy";

            #endregion
        }

        #endregion

        #region Properties

        [ConfigurationProperty(Keys.SERVER, IsRequired = true)]
        public virtual string Server => (string)this[Keys.SERVER];

        [ConfigurationProperty(Keys.NICK)]
        public virtual string Nick
        {
            get
            {
                var nick = (string)this[Keys.NICK];
                return string.IsNullOrWhiteSpace(nick) ? User : nick;
            }
        }

        [ConfigurationProperty(Keys.USER, IsRequired = true)]
        public virtual string User => (string)this[Keys.USER];

        [ConfigurationProperty(Keys.CHANNEL, IsRequired = true)]
        public virtual string Channel => (string)this[Keys.CHANNEL];

        [ConfigurationProperty(Keys.GOES_BY, DefaultValue = Bot.DEFAULT_GOES_BY)]
        public virtual string GoesBy => (string)this[Keys.GOES_BY];

        #endregion
    }
}
