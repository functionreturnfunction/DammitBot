using Lamar;

namespace DammitBot.Abstract;

/// <summary>
/// Base class for configuring Lamar from within plugins.
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