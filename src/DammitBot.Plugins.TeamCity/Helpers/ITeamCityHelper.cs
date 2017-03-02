using System.Collections.Generic;
using TeamCitySharper.DomainEntities;

namespace DammitBot.Helpers
{
    public interface ITeamCityHelper
    {
        bool Initialized { get; }

        #region Abstract Methods

        void Initialize();
        IEnumerable<Build> GetLatestBuilds();

        #endregion
    }
}