namespace DammitBot.Configuration
{
    public interface ITeamCityConfigurationSection
    {
        string Host { get; }
        string Login { get; }
        string Password { get; }
    }
}
