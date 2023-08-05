using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.Wrappers;

namespace DammitBot.Abstract;

/// <inheritdoc cref="IMessageHandlerFactory{TMessageHandler,TMessageEventArgs}"/>
/// <remarks>
/// This implementation uses a <typeparamref name="TMessageHandlerTypeService"/> instance to find all
/// available/matching <typeparamref name="TMessageHandler"/> types, and a
/// <see cref="IInstantiationService"/> to instantiate them.  The instances are then combined into a
/// <typeparamref name="TCompositeMessageHandler"/> so that multiple available/matching handlers can
/// handle the message in a single pass.
/// </remarks>
public abstract class MessageHandlerFactoryBase<
        TMessageHandlerTypeService,
        TMessageEventArgs,
        TMessageHandler,
        TCompositeMessageHandler>
    : IMessageHandlerFactory<TMessageHandler, TMessageEventArgs>
    where TMessageHandlerTypeService : IMessageHandlerTypeService<TMessageHandler, TMessageEventArgs>
    where TMessageEventArgs : MessageEventArgs
    where TMessageHandler : IMessageHandler<TMessageEventArgs>
    where TCompositeMessageHandler
        : CompositeMessageHandlerBase<TMessageHandler, TMessageEventArgs>, TMessageHandler
{
    #region Private Members

    private readonly TMessageHandlerTypeService _handlerTypeService;
    private readonly IInstantiationService _instantiationService;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the
    /// <see cref="MessageHandlerFactoryBase{TMessageHandlerTypeService,TMessageEventArgs,TMessageHandler,TComposite}"/>
    /// class.
    /// </summary>
    protected MessageHandlerFactoryBase(
        TMessageHandlerTypeService handlerTypeService,
        IInstantiationService instantiationService)
    {
        _handlerTypeService = handlerTypeService;
        _instantiationService = instantiationService;
    }

    #endregion
    
    #region Private Methods

    private TMessageHandler Instantiate(Type type)
    {
        return (TMessageHandler)_instantiationService.GetInstance(type);
    }

    private IEnumerable<TMessageHandler> InstantiateAll(IEnumerable<Type> types)
    {
        return types.Select(Instantiate);
    }
    
    #endregion

    #region Abstract Methods

    /// <summary>
    /// Create and return a <typeparamref name="TCompositeMessageHandler"/> from all matched
    /// <paramref name="handlers"/>.
    /// </summary>
    protected abstract TCompositeMessageHandler CreateCompositeHandler(
        IEnumerable<TMessageHandler> handlers);

    #endregion

    #region Exposed Methods

    /// <inheritdoc cref="IMessageHandlerFactory{TMessageHandlerFactory,TMessageEventArgs}.BuildHandler"/>
    public TMessageHandler BuildHandler(TMessageEventArgs message)
    {
        return CreateCompositeHandler(
            InstantiateAll(_handlerTypeService.GetMatchingHandlerTypes(message)));
    }

    #endregion
}