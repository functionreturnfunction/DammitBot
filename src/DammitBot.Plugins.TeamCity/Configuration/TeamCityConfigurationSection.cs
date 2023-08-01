using System.Configuration;
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

        public virtual string Host =>
            _config[Keys.HOST] ??
            throw new ConfigurationErrorsException(
                $"Required configuration key {Keys.HOST} has not been set");

        public virtual string Login =>
            _config[Keys.LOGIN] ??
            throw new ConfigurationErrorsException(
                $"Required configuration key {Keys.LOGIN} has not been set");

        public virtual string Password =>
            _config[Keys.PASSWORD] ??
            throw new ConfigurationErrorsException(
                $"Required configuration key {Keys.PASSWORD} has not been set");

        #endregion
    }
}
