using System.Linq;
using System.Text;
using DammitBot.Helpers;
using DammitBot.Scheduling.Metadata;
using Quartz;
using TeamCitySharper.DomainEntities;

namespace DammitBot.Jobs
{
    [Secondly(15)]
    public class TeamCityJob : IJob
    {
        #region Private Members

        private readonly ITeamCityHelper _helper;
        private readonly IBot _bot;

        #endregion

        #region Constructors

        public TeamCityJob(ITeamCityHelper helper, IBot bot)
        {
            _helper = helper;
            _bot = bot;
        }

        #endregion

        #region Private Methods

        private string DescribeBuild(Build build)
        {
            var sb = new StringBuilder();
            sb.Append($"Build {build.BuildTypeId} {build.Number} ");
            sb.Append(build.Status == "SUCCESS" ? "was successful" : "failed");
            sb.Append($": {build.WebUrl}");
            return sb.ToString();
        }

        #endregion

        #region Exposed Methods

        public void Execute(IJobExecutionContext context)
        {
            // TODO: figure out why this needs to happen
            if (!_helper.Initialized)
            {
                _helper.Initialize();
            }

            foreach (var build in _helper.GetLatestBuilds().ToList())
            {
                _bot.SayInChannel(DescribeBuild(build));
            }
        }

        #endregion
    }
}
