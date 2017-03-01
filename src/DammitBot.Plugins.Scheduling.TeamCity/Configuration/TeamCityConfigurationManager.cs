namespace DammitBot.Configuration
{
    public class TeamCityConfigurationManager : ConfigurationManager, ITeamCityConfigurationManager
    {
        #region Properties

        public TeamCityConfigurationSection TeamCityConfigurationSection
            => GetSection<TeamCityConfigurationSection>(TeamCityConfigurationSection.SECTION_NAME);

        #endregion
    }
}
