using Microsoft.Extensions.Configuration;
using Xunit;

namespace DammitBot.Configuration
{
    public class TeamCityConfigurationSectionTest
    {
        private ITeamCityConfigurationSection _target;

        public TeamCityConfigurationSectionTest()
        {
            _target = new TeamCityConfigurationManager(new ConfigurationBuilder()).TeamCityConfigurationSection;
        }

        [Fact]
        public void TestValuesAreSetProperlyByConfigFile()
        {
            Assert.Equal("host:8111", _target.Host);
            Assert.Equal("login", _target.Login);
            Assert.Equal("password", _target.Password);
        }
    }
}