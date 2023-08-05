using System;
using System.Collections.Generic;
using DammitBot.Events;

namespace DammitBot.Abstract;

/// <summary>
/// Service which provides inheriting/implementing types of <typeparamref name="TMessageHandler"/> which
/// can handle messages represented by <typeparamref name="TMessageEventArgs"/>.
/// </summary>
public interface IMessageHandlerTypeService<out TMessageHandler, in TMessageEventArgs>
    where TMessageHandler : IMessageHandler<TMessageEventArgs>
    where TMessageEventArgs : MessageEventArgs
{
    #region Abstract Methods

    /// <summary>
    /// Returns all available types inheriting/implementing <typeparamref name="TMessageHandler"/> which
    /// can handle the supplied <typeparamref name="TMessageEventArgs"/> <paramref name="message"/>.
    /// </summary>
    IEnumerable<Type> GetMatchingHandlerTypes(TMessageEventArgs message);

    #endregion
}