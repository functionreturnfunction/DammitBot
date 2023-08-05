using System;

namespace DammitBot.Wrappers;

/// <summary>
/// Service for providing instances of <see cref="Type"/>s.
/// </summary>
public interface IInstantiationService
{
    #region Abstract Methods

    /// <summary>
    /// Get and return an instance of <see cref="Type"/> <paramref name="type"/>.
    /// </summary>
    object GetInstance(Type type);

    /// <summary>
    /// Get and return an instance of <typeparamref name="T"/>.
    /// </summary>
    T GetInstance<T>();

    #endregion
}