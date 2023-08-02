using DammitBot.Library;
using Xunit;
using ConfigurationManager = DammitBot.Configuration.ConfigurationManager;

namespace DammitBot.Tests.Configuration;

public class BotConfigurationSectionTest : UnitTestBase<ConfigurationManager>
{
    [Fact]
    public void TestValuesAreSetProperlyByConfigFile()
    {
        Assert.Equal("(?:dammit )?bot", _target.BotConfig.GoesBy);
    }
}