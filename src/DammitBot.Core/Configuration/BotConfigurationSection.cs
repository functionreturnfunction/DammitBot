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

            public const string GOES_BY = "goesBy";

            #endregion
        }

        #endregion

        #region Properties

        [ConfigurationProperty(Keys.GOES_BY, DefaultValue = Bot.DEFAULT_GOES_BY)]
        public virtual string GoesBy => (string)this[Keys.GOES_BY];

        #endregion
    }
}
