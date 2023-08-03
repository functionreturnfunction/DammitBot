using Lamar;

namespace DammitBot.Abstract;

/// <summary>
/// Base class for configuring StructureMap from within plugins.
/// </summary>
public abstract class ContainerConfigurationBase
{
    #region Abstract Methods

    public abstract void Configure(ServiceRegistry e);

    #endregion
}