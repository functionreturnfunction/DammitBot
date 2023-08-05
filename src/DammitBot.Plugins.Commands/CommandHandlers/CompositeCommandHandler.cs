using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.CommandHandlers;

/// <summary>
/// Calls <see cref="ICommandHandler.Handle"/> on multiple instances of <see cref="ICommandHandler"/> for
/// a given instance of <see cref="CommandEventArgs"/>.
/// </summary>
public class CompositeCommandHandler
    : CompositeMessageHandlerBase<ICommandHandler, CommandEventArgs>, ICommandHandler
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="CompositeCommandHandler"/> class.
    /// </summary>
    public CompositeCommandHandler(IEnumerable<ICommandHandler> innerHandlers)
        : base(innerHandlers) {}

    #endregion
}