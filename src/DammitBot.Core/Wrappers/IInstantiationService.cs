using System;

namespace DammitBot.Wrappers
{
    public interface IInstantiationService : IDisposable
    {
        #region Abstract Methods

        object GetInstance(Type type);
        T GetInstance<T>();

        #endregion
    }
}