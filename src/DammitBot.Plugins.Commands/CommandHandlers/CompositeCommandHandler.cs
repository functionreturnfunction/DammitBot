using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.MessageHandlers;

namespace DammitBot.CommandHandlers;

public class CompositeCommandHandler
    : CompositeMessageHandlerBase<ICommandHandler, CommandEventArgs>, ICommandHandler
{
    #region Constructors

    public CompositeCommandHandler(IEnumerable<ICommandHandler> innerHandlers)
        : base(innerHandlers) {}

    #endregion
}