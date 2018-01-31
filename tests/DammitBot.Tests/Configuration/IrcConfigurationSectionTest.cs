using Microsoft.Extensions.Configuration;
using Xunit;

namespace DammitBot.Configuration
{
    public class IrcConfigurationSectionTest
    {
        private IIrcConfigurationSection _target;

        public IrcConfigurationSectionTest()
        {
            _target = new IrcConfigurationManager(new ConfigurationBuilder()).IrcConfigurationSection;
        }

        [Fact]
        public void TestValuesAreSetProperlyByConfigFile()
        {
            Assert.Equal("nick", _target.Nick);
            Assert.Equal("server", _target.Server);
            Assert.Equal("user", _target.User);
            Assert.Equal(new[] {"#channelA", "#channelB"}, _target.Channels);
        }
    }
}