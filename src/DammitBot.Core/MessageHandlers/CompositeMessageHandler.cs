using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.MessageHandlers
{
    public class CompositeMessageHandler : CompositeMessageHandlerBase<IMessageHandler, MessageEventArgs>, IMessageHandler
    {
        #region Constructors

        public CompositeMessageHandler(IEnumerable<IMessageHandler> innerHandlers) : base(innerHandlers) {}

        #endregion
    }
}