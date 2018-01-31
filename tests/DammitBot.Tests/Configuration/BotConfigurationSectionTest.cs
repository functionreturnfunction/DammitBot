using Microsoft.Extensions.Configuration;
using Xunit;

namespace DammitBot.Configuration
{

    public class BotConfigurationSectionTest
    {
        #region Private Members

        private IBotConfigurationSection _target;

        #endregion

        #region Setup/Teardown

        public BotConfigurationSectionTest()
        {
            _target = new ConfigurationManager(new ConfigurationBuilder()).BotConfig;
        }

        #endregion

        [Fact]
        public void TestValuesAreSetProperlyByConfigFile()
        {
            Assert.Equal("(?:dammit )?bot", _target.GoesBy);
        }
    }
}
