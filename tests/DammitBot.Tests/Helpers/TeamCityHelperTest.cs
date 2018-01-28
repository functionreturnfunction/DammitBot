using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using DammitBot.Configuration;
using DammitBot.TestLibrary;

using Moq;
using TeamCitySharper;
using TeamCitySharper.DomainEntities;
using TeamCitySharper.Locators;
using Xunit;

namespace DammitBot.Helpers
{

    public class TeamCityHelperTest : UnitTestBase<TeamCityHelper>
    {
        private Mock<ITeamCityClient> _client;
        private Mock<TeamCityConfigurationSection> _config;

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Inject(out _client, MockBehavior.Strict);
            Inject(out _config);
            _config.SetupGet(x => x.Login).Returns("foo");
            _config.SetupGet(x => x.Password).Returns("bar");
        }

        [Fact]
        public void TestInitializeConnectsAndGetsLastBuildFromClient()
        {
            // TODO: "funcitonality is written but test is being difficult to write"
            //_client.Setup(x => x.Connect("foo", "bar"));
            //_client.Setup(
            //    x => x.Builds.ByBuildLocator(BuildLocator.WithDimensions(null, null, null, null, null, null, null,
            //        null, 1, null, null, null, null, null))).Returns(new List<Build> {new Build()});

            //_target.Initialize();

            //_client.Verify(x => x.Connect("foo", "bar"));
            //_client.Verify(
            //    x =>
            //        x.Builds.ByBuildLocator(BuildLocator.WithDimensions(null, null, null, null, null, null, null,
            //            null, 1, null, null, null, null, null)));
        }
    }
}
