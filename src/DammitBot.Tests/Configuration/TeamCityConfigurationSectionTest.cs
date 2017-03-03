using Xunit;

namespace DammitBot.Configuration
{
    public class TeamCityConfigurationSectionTest
    {
        private TeamCityConfigurationSection _target;

        public TeamCityConfigurationSectionTest()
        {
            _target =
                (TeamCityConfigurationSection)
                System.Configuration.ConfigurationManager.GetSection(TeamCityConfigurationSection.SECTION_NAME);
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