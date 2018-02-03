using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DammitBot.Helpers;
using DammitBot.TestLibrary;

using Moq;
using TeamCitySharper.DomainEntities;
using Xunit;

namespace DammitBot.Jobs
{

    public class TeamCityJobTest : UnitTestBase<TeamCityJob>
    {
        private Mock<ITeamCityHelper> _helper;
        private Mock<IBot> _bot;

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Inject(out _helper);
            Inject(out _bot);
        }

        [Fact]
        public async void TestExecuteInitializesHelperIfNotAlreadyInitialized()
        {
            await _target.Execute(null);

            _helper.Verify(x => x.Initialize());
        }

        [Fact]
        public async void TestExecuteDoesNotInitializeHelperIfAlreadyInitialized()
        {
            _helper.SetupGet(x => x.Initialized).Returns(true);

            await _target.Execute(null);

            _helper.Verify(x => x.Initialize(), Times.Never);
        }

        [Fact]
        public async void TestExecuteDescribesAllLatestBuildsToChannel()
        {
            var builds = new[] {
                new Build {BuildTypeId = "foo", Number = "1", Status = "SUCCESS", WebUrl = "http://fooblah"},
                new Build {BuildTypeId = "bar", Number = "2", Status = "vOv", WebUrl = "http://barblah"}
            };
            _helper.Setup(x => x.GetLatestBuilds()).Returns(builds);

            await _target.Execute(null);

            _bot.Verify(x => x.SayToAll($"Build foo 1 was successful: http://fooblah"));
            _bot.Verify(x => x.SayToAll($"Build bar 2 failed: http://barblah"));
        }
    }
}
