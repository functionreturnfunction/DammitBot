namespace DammitBot.Configuration
{
    public interface IConfigurationManager
    {
        BotConfigurationSection BotConfig { get; }
    }
}