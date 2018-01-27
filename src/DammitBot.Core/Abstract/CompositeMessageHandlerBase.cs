using System.Collections.Generic;
using DammitBot.Events;

namespace DammitBot.Abstract
{
    public abstract class CompositeMessageHandlerBase<TMessageHandler, TEventArgs>
        where TMessageHandler : IMessageHandler<TEventArgs>
        where TEventArgs : MessageEventArgs
    {
        #region Private Members

        protected readonly IEnumerable<TMessageHandler> _innerHandlers;

        #endregion

        #region Constructors

        protected CompositeMessageHandlerBase(IEnumerable<TMessageHandler> innerHandlers)
        {
            _innerHandlers = innerHandlers;
        }

        #endregion

        #region Exposed Methods

        public void Handle(TEventArgs e)
        {
            foreach (var handler in _innerHandlers)
            {
                handler.Handle(e);
            }
        }

        #endregion
    }
}