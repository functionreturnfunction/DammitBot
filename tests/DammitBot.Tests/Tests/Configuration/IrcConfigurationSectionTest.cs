using DammitBot.Configuration;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Configuration;

public class IrcConfigurationSectionTest : UnitTestBase<IrcConfigurationProvider>
{
    [Fact]
    public void Test_ValuesAreSetProperly_ByConfigFile()
    {
        Assert.Equal("nick", _target.IrcConfigurationSection.Nick);
        Assert.Equal("server", _target.IrcConfigurationSection.Server);
        Assert.Equal("user", _target.IrcConfigurationSection.User);
        Assert.Equal(
            new[] {"#channelA", "#channelB"},
            _target.IrcConfigurationSection.Channels);
    }
}