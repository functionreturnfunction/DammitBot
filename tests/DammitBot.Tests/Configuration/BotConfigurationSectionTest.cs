using DammitBot.TestLibrary;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace DammitBot.Configuration
{

    public class BotConfigurationSectionTest : UnitTestBase<ConfigurationManager>
    {
        [Fact]
        public void TestValuesAreSetProperlyByConfigFile()
        {
            Assert.Equal("(?:dammit )?bot", _target.BotConfig.GoesBy);
        }
    }
}
