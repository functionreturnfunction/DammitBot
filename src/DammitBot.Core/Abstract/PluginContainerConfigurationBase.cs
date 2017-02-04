using DammitBot.Wrappers;
using StructureMap;

namespace DammitBot.Abstract
{
    public abstract class PluginContainerConfigurationBase
    {
        #region Abstract Methods

        public abstract void Configure(ConfigurationExpression configurationExpression);

        #endregion
    }
}
