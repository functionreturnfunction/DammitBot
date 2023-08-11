using DammitBot.Configuration;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Configuration;

public class BotConfigurationSectionTest : UnitTestBase<ConfigurationProvider>
{
    [Fact]
    public void Test_ValuesAreSetProperly_ByConfigFile()
    {
        Assert.Equal("(?:dammit )?bot", _target.BotConfig.GoesBy);
    }
}