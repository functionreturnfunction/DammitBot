using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.MessageHandlers;

/// <inheritdoc cref="CompositeMessageHandlerBase{IMessageHandler,MEssageEventArgs}"/>
public class CompositeMessageHandler
    : CompositeMessageHandlerBase<IMessageHandler, MessageEventArgs>, IMessageHandler
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="CompositeMessageHandler"/> class.
    /// </summary>
    public CompositeMessageHandler(IEnumerable<IMessageHandler> innerHandlers)
        : base(innerHandlers) {}

    #endregion
}