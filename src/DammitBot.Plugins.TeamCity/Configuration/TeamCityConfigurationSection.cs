using System;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration
{
    public class TeamCityConfigurationSection : ITeamCityConfigurationSection
    {
        #region Constants

        public const string KEY = "TeamCity";
        private IConfigurationSection _config;

        public TeamCityConfigurationSection(IConfigurationSection config)
        {
            _config = config;
        }

        public struct Keys
        {
            #region Constants

            public const string HOST = "host", LOGIN = "login", PASSWORD = "password";

            #endregion
        }

        #endregion

        #region Properties

        public virtual string Host => _config[Keys.HOST];

        public virtual string Login => _config[Keys.LOGIN];

        public virtual string Password => _config[Keys.PASSWORD];

        #endregion
    }
}
