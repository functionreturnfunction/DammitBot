using DammitBot.Configuration;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Configuration;

public class BotConfigurationSectionTest : UnitTestBase<ConfigurationProvider>
{
    [Fact]
    public void TestValuesAreSetProperlyByConfigFile()
    {
        Assert.Equal("(?:dammit )?bot", _target.BotConfig.GoesBy);
    }
}