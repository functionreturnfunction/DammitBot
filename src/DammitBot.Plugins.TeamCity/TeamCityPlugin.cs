using DammitBot.Abstract;
using DammitBot.Helpers;

namespace DammitBot
{
    public class TeamCityPlugin : IPlugin
    {
        #region Private Members

        private readonly ITeamCityHelper _helper;

        #endregion

        #region Constructors

        public TeamCityPlugin(ITeamCityHelper helper)
        {
            _helper = helper;
        }

        #endregion

        #region Exposed Methods

        public void Initialize()
        {
            _helper.Initialize();
        }

        public void Cleanup() {}

        #endregion
    }
}
