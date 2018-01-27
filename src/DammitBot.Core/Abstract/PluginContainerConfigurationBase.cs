using Microsoft.Extensions.DependencyInjection;

namespace DammitBot.Abstract
{
    /// <summary>
    /// Base class for configuring IOC from within plugins.
    /// </summary>
    public abstract class PluginContainerConfigurationBase
    {
        #region Abstract Methods

        public abstract void Configure(IServiceCollection serviceCollection);

        #endregion
    }
}
