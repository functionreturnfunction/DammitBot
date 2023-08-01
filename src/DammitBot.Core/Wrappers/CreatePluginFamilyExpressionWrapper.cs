using System.Diagnostics.CodeAnalysis;
using StructureMap.Configuration.DSL.Expressions;

namespace DammitBot.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class CreatePluginFamilyExpressionWrapper<TInterface>
        : ICreatePluginFamilyExpression<TInterface>
    {
        #region Private Members

        private readonly CreatePluginFamilyExpression<TInterface> _innerExpression;

        #endregion

        #region Constructors

        public CreatePluginFamilyExpressionWrapper(CreatePluginFamilyExpression<TInterface> e)
        {
            _innerExpression = e;
        }

        #endregion

        #region Exposed Methods

        public ISmartInstance<TConcrete, TInterface> Use<TConcrete>()
            where TConcrete : TInterface
        {
            return new SmartInstanceWrapper<TConcrete, TInterface>(_innerExpression.Use<TConcrete>());
        }

        #endregion
    }
}