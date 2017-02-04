using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Configuration;
using log4net;
using TeamCitySharper;
using TeamCitySharper.DomainEntities;
using TeamCitySharper.Locators;

namespace DammitBot.Helpers
{
    public class TeamCityHelper : ITeamCityHelper
    {
        #region Private Members

        private readonly ITeamCityClient _client;
        private readonly TeamCityConfigurationSection _config;
        private readonly ILog _log;
        private Build _lastBuild;

        #endregion

        #region Properties

        public bool Initialized { get; private set; }

        #endregion

        #region Constructors

        public TeamCityHelper(ITeamCityClient client, TeamCityConfigurationSection config, ILog log)
        {
            _client = client;
            _config = config;
            _log = log;
        }

        #endregion

        #region Exposed Methods

        public void Initialize()
        {
            _client.Connect(_config.Login, _config.Password);
            _lastBuild = _client.Builds.ByBuildLocator(BuildLocator.WithDimensions(maxResults: 1)).Single();
            Initialized = true;
        }

        public IEnumerable<Build> GetLatestBuilds()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("Helper not yet initialized.");
            }

            var ret =
                _client.Builds.ByBuildLocator(BuildLocator.WithDimensions(sinceBuild: BuildLocator.WithId(Convert.ToInt32(_lastBuild.Id))));
            _log.Debug($"Found {ret.Count} builds since {_lastBuild.Id}");
            _lastBuild = ret.Any() ? ret.Last() : _lastBuild;
            return ret;
        }

        #endregion
    }
}