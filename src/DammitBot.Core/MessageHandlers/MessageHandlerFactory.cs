using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Wrappers;

namespace DammitBot.MessageHandlers
{
    public class MessageHandlerFactory : MessageHandlerFactoryBase<IMessageHandlerRepository, MessageEventArgs, IMessageHandler, CompositeMessageHandler>, IMessageHandlerFactory
    {
        #region Constructors

        public MessageHandlerFactory(IMessageHandlerRepository repository, IInstantiationService instantiationService) : base(repository, instantiationService) {}

        #endregion

        #region Private Methods

        protected override CompositeMessageHandler CreateCompositeHandler(IEnumerable<IMessageHandler> handlers)
        {
            return new CompositeMessageHandler(handlers);
        }

        #endregion
    }
}