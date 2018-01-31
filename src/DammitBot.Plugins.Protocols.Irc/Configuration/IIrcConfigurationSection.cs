namespace DammitBot.Configuration
{
    public interface IIrcConfigurationSection
    {
        string Server { get; }
        string Nick { get; }
        string User { get; }
        string[] Channels { get; }
    }
}
