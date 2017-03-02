using System.Configuration;

namespace DammitBot.Protocols.Irc.Configuration
{
    public class IrcConfigurationSection : ConfigurationSection
    {
        #region Constants

        public const string SECTION_NAME = Irc.PROTOCOL_NAME;

        public struct Keys
        {
            #region Constants

            public const string SERVER = "server", NICK = "nick", USER = "user", CHANNELS = "channels";

            #endregion
        }

        #endregion

        #region Properties

        [ConfigurationProperty(Keys.SERVER, IsRequired = true)]
        public virtual string Server => (string) this[Keys.SERVER];

        [ConfigurationProperty(Keys.NICK)]
        public virtual string Nick
        {
            get
            {
                var nick = (string) this[Keys.NICK];
                return string.IsNullOrWhiteSpace(nick) ? User : nick;
            }
        }

        [ConfigurationProperty(Keys.USER, IsRequired = true)]
        public virtual string User => (string) this[Keys.USER];

        [ConfigurationProperty(Keys.CHANNELS, IsRequired = true)]
        public virtual string ChannelsStr => this[Keys.CHANNELS].ToString();

        public virtual string[] Channels => ChannelsStr.Split(',');

        #endregion
    }
}