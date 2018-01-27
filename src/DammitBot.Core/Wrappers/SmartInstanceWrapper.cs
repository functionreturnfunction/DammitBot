using System.Diagnostics.CodeAnalysis;
using StructureMap.Pipeline;

namespace DammitBot.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class SmartInstanceWrapper<TConcrete, TInterface> : ISmartInstance<TConcrete, TInterface>
        where TConcrete : TInterface
    {
        #region Private Members

        private readonly SmartInstance<TConcrete, TInterface> _innerInstance;

        #endregion

        #region Constructors

        public SmartInstanceWrapper(SmartInstance<TConcrete, TInterface> instance)
        {
            _innerInstance = instance;
        }

        #endregion
    }
}