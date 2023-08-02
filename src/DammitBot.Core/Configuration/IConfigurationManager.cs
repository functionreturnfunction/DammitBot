namespace DammitBot.Configuration;

public interface IConfigurationManager
{
    IBotConfigurationSection BotConfig { get; }
}