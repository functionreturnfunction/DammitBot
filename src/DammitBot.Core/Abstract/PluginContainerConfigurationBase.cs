using Lamar;

namespace DammitBot.Abstract;

/// <summary>
/// Class for configuring Lamar type registrations.
/// </summary>
public abstract class ContainerConfigurationBase
{
    #region Abstract Methods

    /// <summary>
    /// Register any necessary services.
    /// </summary>
    public abstract void Configure(ServiceRegistry e);

    #endregion
}