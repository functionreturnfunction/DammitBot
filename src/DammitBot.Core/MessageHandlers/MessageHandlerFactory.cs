using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Wrappers;

namespace DammitBot.MessageHandlers;

/// <summary>
/// Factory for creating instances of <see cref="IMessageHandler"/> which handle messages
/// represented by instances of <see cref="MessageEventArgs"/>.
/// </summary>
public class MessageHandlerFactory
    : MessageHandlerFactoryBase<
            IMessageHandlerTypeService,
            MessageEventArgs,
            IMessageHandler,
            CompositeMessageHandler>,
        IMessageHandlerFactory
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="MessageHandlerFactory"/> class.
    /// </summary>
    public MessageHandlerFactory(
        IMessageHandlerTypeService handlerTypeService,
        IInstantiationService instantiationService)
        : base(handlerTypeService, instantiationService) {}

    #endregion

    #region Private Methods

    /// <summary>
    /// Create and return a <see cref="CompositeMessageHandler"/> from all matched
    /// <paramref name="handlers"/>.
    /// </summary>
    protected override CompositeMessageHandler CreateCompositeHandler(
        IEnumerable<IMessageHandler> handlers)
    {
        return new CompositeMessageHandler(handlers);
    }

    #endregion
}