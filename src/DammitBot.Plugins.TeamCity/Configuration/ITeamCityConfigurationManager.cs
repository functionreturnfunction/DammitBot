namespace DammitBot.Configuration
{
    public interface ITeamCityConfigurationManager : IConfigurationManager
    {
        TeamCityConfigurationSection TeamCityConfigurationSection {get;}
    }
}