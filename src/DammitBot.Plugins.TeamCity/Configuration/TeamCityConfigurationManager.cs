using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration
{
    public class TeamCityConfigurationManager : ConfigurationManager, ITeamCityConfigurationManager
    {
        public TeamCityConfigurationManager(IConfigurationBuilder builder, ISettingsPathHelper settingsPathHelper) : base(builder, settingsPathHelper) {}

        #region Properties

        public virtual ITeamCityConfigurationSection TeamCityConfigurationSection
            => new TeamCityConfigurationSection(Configuration.GetSection(DammitBot.Configuration.TeamCityConfigurationSection.KEY));

        #endregion
    }
}
