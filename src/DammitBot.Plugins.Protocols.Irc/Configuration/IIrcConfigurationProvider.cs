namespace DammitBot.Configuration;

public interface IIrcConfigurationProvider : IConfigurationProvider
{
    #region Abstract Properties

    IIrcConfigurationSection IrcConfigurationSection { get; }

    #endregion
}