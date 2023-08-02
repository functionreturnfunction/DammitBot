namespace DammitBot.Wrappers;

public interface ICreatePluginFamilyExpression<TInterface>
{
    #region Abstract Methods

    ISmartInstance<TConcrete, TInterface> Use<TConcrete>() where TConcrete : TInterface;

    #endregion
}