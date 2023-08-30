namespace DammitBot.Abstract;

/// <summary>
/// Factory responsible for building <typeparamref name="TClient"/> instances.
/// </summary>
public interface IProtocolClientFactory<TClient>
    where TClient : IProtocolClient
{
    #region Abstract Methods

    /// <summary>
    /// Build and return a <typeparamref name="TClient"/> instance.
    /// </summary>
    TClient Build();

    #endregion
}