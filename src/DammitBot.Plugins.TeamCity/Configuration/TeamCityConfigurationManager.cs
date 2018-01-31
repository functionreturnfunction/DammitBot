using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration
{
    public class TeamCityConfigurationManager : ConfigurationManager, ITeamCityConfigurationManager
    {
        public TeamCityConfigurationManager(IConfigurationBuilder builder) : base(builder) {}

        #region Properties

        public virtual ITeamCityConfigurationSection TeamCityConfigurationSection
            => new TeamCityConfigurationSection(Configuration.GetSection(DammitBot.Configuration.TeamCityConfigurationSection.KEY));

        #endregion
    }
}
