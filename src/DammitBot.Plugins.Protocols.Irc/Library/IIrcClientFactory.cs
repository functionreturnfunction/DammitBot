namespace DammitBot.Library;

/// <summary>
/// Factory responsible for building <see cref="IIrcClient"/> instances.
/// </summary>
public interface IIrcClientFactory
{
    #region Abstract Methods

    /// <summary>
    /// Build and return an <see cref="IIrcClient"/> instance.
    /// </summary>
    IIrcClient Build();

    #endregion
}