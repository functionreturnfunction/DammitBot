using System;
using Lamar;

namespace DammitBot.Wrappers;

/// <inheritdoc cref="IInstantiationService"/>
public class InstantiationService : IInstantiationService
{
    #region Private Members

    private readonly IContainer _container;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="InstantiationService"/> class.
    /// </summary>
    /// <param name="container"></param>
    public InstantiationService(IContainer container)
    {
        _container = container;
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc cref="IInstantiationService.GetInstance"/>
    public object GetInstance(Type type)
    {
        return _container.GetInstance(type);
    }

    /// <inheritdoc cref="IInstantiationService.GetInstance"/>
    public T GetInstance<T>()
    {
        return _container.GetInstance<T>();
    }

    #endregion
}