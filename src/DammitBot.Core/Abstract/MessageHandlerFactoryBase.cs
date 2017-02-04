using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.Wrappers;

namespace DammitBot.Abstract
{
    public abstract class MessageHandlerFactoryBase<TRepository, TEventArgs, THandler, TComposite> : IHandlerFactory<THandler, TEventArgs>
        where TRepository : IMessageHandlerRepository<THandler, TEventArgs>
        where TEventArgs : MessageEventArgs
        where THandler : IMessageHandler<TEventArgs>
        where TComposite : CompositeMessageHandlerBase<THandler, TEventArgs>, THandler
    {
        #region Private Members

        private readonly TRepository _repository;
        private readonly IInstantiationService _instantiationService;

        #endregion

        #region Constructors

        protected MessageHandlerFactoryBase(TRepository repository, IInstantiationService instantiationService)
        {
            _repository = repository;
            _instantiationService = instantiationService;
        }

        #endregion

        protected THandler Instantiate(Type type)
        {
            return (THandler)_instantiationService.GetInstance(type);
        }

        protected IEnumerable<THandler> InstantiateAll(IEnumerable<Type> types)
        {
            return types.Select(Instantiate);
        }

        #region Abstract Methods

        protected abstract TComposite CreateCompositeHandler(IEnumerable<THandler> handlers);

        #endregion

        #region Exposed Methods

        public THandler BuildHandler(TEventArgs message)
        {
            return CreateCompositeHandler(InstantiateAll(_repository.GetMatchingHandlers(message)));
        }

        #endregion
    }
}