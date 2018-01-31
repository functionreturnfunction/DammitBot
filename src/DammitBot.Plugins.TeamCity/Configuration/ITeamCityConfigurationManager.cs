namespace DammitBot.Configuration
{
    public interface ITeamCityConfigurationManager : IConfigurationManager
    {
        ITeamCityConfigurationSection TeamCityConfigurationSection { get; }
    }
}