using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Wrappers;

namespace DammitBot.MessageHandlers;

public class MessageHandlerFactory
    : MessageHandlerFactoryBase<
            IMessageHandlerService,
            MessageEventArgs,
            IMessageHandler,
            CompositeMessageHandler>,
        IMessageHandlerFactory
{
    #region Constructors

    public MessageHandlerFactory(
        IMessageHandlerService handlerService,
        IInstantiationService instantiationService)
        : base(handlerService, instantiationService) {}

    #endregion

    #region Private Methods

    protected override CompositeMessageHandler CreateCompositeHandler(
        IEnumerable<IMessageHandler> handlers)
    {
        return new CompositeMessageHandler(handlers);
    }

    #endregion
}