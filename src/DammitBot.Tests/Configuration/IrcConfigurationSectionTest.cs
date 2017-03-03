using Xunit;

namespace DammitBot.Configuration
{
    public class IrcConfigurationSectionTest
    {
        private IrcConfigurationSection _target;

        public IrcConfigurationSectionTest()
        {
            _target =
                (IrcConfigurationSection)
                System.Configuration.ConfigurationManager.GetSection(IrcConfigurationSection.SECTION_NAME);
        }

        [Fact]
        public void TestValuesAreSetProperlyByConfigFile()
        {
            Assert.Equal("nick", _target.Nick);
            Assert.Equal("#channelA,#channelB", _target.ChannelsStr);
            Assert.Equal("server", _target.Server);
            Assert.Equal("user", _target.User);
            Assert.Equal(new[] {"#channelA", "#channelB"}, _target.Channels);
        }
    }
}