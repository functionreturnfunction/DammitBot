using System.Collections.Generic;
using DammitBot.Events;

namespace DammitBot.Abstract;

/// <inheritdoc cref="IMessageHandler{TArgs}"/>
/// <remarks>
/// This implementation calls <see cref="IMessageHandler{TArgs}.Handle(TArgs)"/> on multiple instances of
/// <typeparamref name="TMessageHandler"/> in a single pass.
/// </remarks>
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

    /// <inheritdoc cref="IMessageHandler{TArgs}.Handle"/>
    /// <inheritdoc cref="CompositeMessageHandlerBase{TMessageHandler,TEventArgs}" path="remarks"/>
    public void Handle(TEventArgs e)
    {
        foreach (var handler in _innerHandlers)
        {
            handler.Handle(e);
        }
    }

    #endregion
}