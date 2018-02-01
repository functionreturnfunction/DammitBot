using DammitBot.TestLibrary;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace DammitBot.Configuration
{
    public class IrcConfigurationSectionTest : UnitTestBase<IrcConfigurationManager>
    {
        [Fact]
        public void TestValuesAreSetProperlyByConfigFile()
        {
            Assert.Equal("nick", _target.IrcConfigurationSection.Nick);
            Assert.Equal("server", _target.IrcConfigurationSection.Server);
            Assert.Equal("user", _target.IrcConfigurationSection.User);
            Assert.Equal(new[] {"#channelA", "#channelB"}, _target.IrcConfigurationSection.Channels);
        }
    }
}