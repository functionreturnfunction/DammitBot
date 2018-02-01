using DammitBot.TestLibrary;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace DammitBot.Configuration
{
    public class TeamCityConfigurationSectionTest : UnitTestBase<ITeamCityConfigurationManager>
    {
        [Fact]
        public void TestValuesAreSetProperlyByConfigFile()
        {
            Assert.Equal("host:8111", _target.TeamCityConfigurationSection.Host);
            Assert.Equal("login", _target.TeamCityConfigurationSection.Login);
            Assert.Equal("password", _target.TeamCityConfigurationSection.Password);
        }
    }
}