using System;
using System.Collections.Generic;
using DammitBot.Events;

namespace DammitBot.Abstract;

/// <summary>
/// Service which provides inheriting/implementing types of <typeparamref name="TMessageHandler"/> which
/// can handle messages represented by <typeparamref name="TEventArgs"/>.
/// </summary>
public interface IMessageHandlerService<out TMessageHandler, in TEventArgs>
    where TMessageHandler : IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs
{
    #region Abstract Methods

    /// <summary>
    /// Returns all available types inheriting/implementing <typeparamref name="TMessageHandler"/> which
    /// can handle the supplied <typeparamref name="TEventArgs"/> <paramref name="message"/>.
    /// </summary>
    IEnumerable<Type> GetMatchingHandlers(TEventArgs message);

    #endregion
}