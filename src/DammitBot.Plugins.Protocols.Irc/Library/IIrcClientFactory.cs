namespace DammitBot.Library;

public interface IIrcClientFactory
{
    #region Abstract Methods

    IIrcClient Build();

    #endregion
}