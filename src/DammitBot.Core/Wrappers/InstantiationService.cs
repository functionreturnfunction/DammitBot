using System;
using Lamar;

namespace DammitBot.Wrappers;

public class InstantiationService : IInstantiationService
{
    #region Private Members

    private readonly IContainer _container;

    #endregion

    #region Constructors

    public InstantiationService(IContainer container)
    {
        _container = container;
        // _container.DisposalLock = DisposalLock.Ignore;
    }

    #endregion

    #region Exposed Methods

    public object GetInstance(Type type)
    {
        return _container.GetInstance(type);
    }

    public T GetInstance<T>()
    {
        return _container.GetInstance<T>();
    }

    public void Dispose()
    {
        // _container.DisposalLock = DisposalLock.Unlocked;
        _container.Dispose();
    }

    #endregion
}