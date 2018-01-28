

using Xunit;

namespace DammitBot.Configuration
{

    public class BotConfigurationSectionTest
    {
        #region Private Members

        private BotConfigurationSection _target;

        #endregion

        #region Setup/Teardown

        public BotConfigurationSectionTest()
        {
            _target = (BotConfigurationSection)System.Configuration.ConfigurationManager.GetSection(BotConfigurationSection.SECTION_NAME);
        }

        #endregion

        [Fact]
        public void TestValuesAreSetProperlyByConfigFile()
        {
            Assert.Equal("(?:dammit )?bot", _target.GoesBy);
        }
    }
}
