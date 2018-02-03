using System;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration
{
    public class TeamCityConfigurationSection : ITeamCityConfigurationSection
    {
        public const string KEY = "TeamCity";

        public struct Keys
        {
            public const string HOST = "host", LOGIN = "login", PASSWORD = "password";
        }

        private IConfigurationSection _config;

        public TeamCityConfigurationSection(IConfigurationSection config)
        {
            _config = config;
        }

        #region Properties

        public virtual string Host => _config[Keys.HOST];

        public virtual string Login => _config[Keys.LOGIN];

        public virtual string Password => _config[Keys.PASSWORD];

        #endregion
    }
}
