using System.Configuration;

namespace DammitBot.Configuration
{
    public class TeamCityConfigurationSection : ConfigurationSection
    {
        #region Constants

        public const string SECTION_NAME = "TeamCity";

        public struct Keys
        {
            #region Constants

            public const string HOST = "host", LOGIN = "login", PASSWORD = "password";

            #endregion
        }

        #endregion

        #region Properties

        [ConfigurationProperty(Keys.HOST, IsRequired = true)]
        public virtual string Host => (string)this[Keys.HOST];

        [ConfigurationProperty(Keys.LOGIN, IsRequired = true)]
        public virtual string Login => (string)this[Keys.LOGIN];

        [ConfigurationProperty(Keys.PASSWORD, IsRequired = true)]
        public virtual string Password => (string)this[Keys.PASSWORD];

        #endregion
    }
}
