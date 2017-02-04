namespace DammitBot.Wrappers
{
    public interface ISmartInstance<TConcrete, TInterface>
        where TConcrete : TInterface
    { }
}