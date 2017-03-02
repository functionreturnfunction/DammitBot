using StructureMap;

namespace DammitBot.Abstract
{
    /// <summary>
    /// Base class for configuring StructureMap from within plugins.
    /// </summary>
    public abstract class PluginContainerConfigurationBase
    {
        #region Abstract Methods

        public abstract void Configure(ConfigurationExpression configurationExpression);

        #endregion
    }
}
