using DammitBot.Configuration;

namespace DammitBot.Wrappers;

public interface IIrcClientFactory
{
    #region Abstract Methods

    IIrcClient Build(IIrcConfigurationSection config);

    #endregion
}