using System.Collections.Generic;
using DammitBot.Events;

namespace DammitBot.Abstract;

/// <summary>
/// Base class for calling <see cref="IMessageHandler{TArgs}.Handle(TArgs)"/> on multiple instances of
/// <typeparamref name="TMessageHandler"/>.
/// </summary>
/// <inheritdoc cref="IMessageHandler{TArgs}"/>
public abstract class CompositeMessageHandlerBase<TMessageHandler, TEventArgs>
    where TMessageHandler : IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs
{
    #region Private Members

    private readonly IEnumerable<TMessageHandler> _innerHandlers;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="CompositeMessageHandlerBase{TMessageHandler,TEventArgs}"/> class.
    /// </summary>
    protected CompositeMessageHandlerBase(IEnumerable<TMessageHandler> innerHandlers)
    {
        _innerHandlers = innerHandlers;
    }

    #endregion

    #region Exposed Methods

    /// <summary>
    /// Handle the event represented by <paramref name="e"/> using each available
    /// <typeparamref name="TMessageHandler"/>.
    /// </summary>
    public void Handle(TEventArgs e)
    {
        foreach (var handler in _innerHandlers)
        {
            handler.Handle(e);
        }
    }

    #endregion
}