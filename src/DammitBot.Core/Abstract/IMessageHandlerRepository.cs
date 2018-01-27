using System;
using System.Collections.Generic;
using DammitBot.Events;

namespace DammitBot.Abstract
{
    public interface IMessageHandlerRepository<out TMessageHandler, in TEventArgs>
        where TMessageHandler : IMessageHandler<TEventArgs>
        where TEventArgs : MessageEventArgs
    {
        #region Abstract Methods

        IEnumerable<Type> GetMatchingHandlers(TEventArgs message);

        #endregion
    }
}