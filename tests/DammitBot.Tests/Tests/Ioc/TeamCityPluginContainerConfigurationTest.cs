using DammitBot.Configuration;
using DammitBot.Helpers;
using DammitBot.Ioc;
using DammitBot.Library;
using StructureMap;
using TeamCitySharper;
using Xunit;

namespace DammitBot.Tests.Ioc
{
    public class TeamCityPluginContainerConfigurationTest : InMemoryDatabaseUnitTestBase<IContainer>
    {
        public TeamCityPluginContainerConfigurationTest()
        {
            _container.Configure (e =>
                new TeamCityPluginContainerConfiguration().Configure(e));
        }

        [Fact]
        public void TestConfigureSetsUpTeamCityHelper ()
        {
            Assert.IsType<TeamCityHelper>(_target.GetInstance<ITeamCityHelper> ());
        }

        [Fact]
        public void TestConfigureSetsUpTeamCityConfigurationSection()
        {
            Assert.IsType<TeamCityConfigurationSection>(_target.GetInstance<ITeamCityConfigurationSection>());
        }

        [Fact]
        public void TestConfigureSetsUpTeamCityClient()
        {
            Assert.IsType<TeamCityClient>(_target.GetInstance<ITeamCityClient>());
        }
    }
}