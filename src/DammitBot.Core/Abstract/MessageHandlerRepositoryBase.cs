using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.Abstract
{
    public abstract class MessageHandlerRepositoryBase<
        TMessageAttribute,
        TAttributeService,
        TEventArgs,
        TMessageHandler>
        : IMessageHandlerRepository<TMessageHandler, TEventArgs>
        where TMessageAttribute : Attribute, IHandlesMessageAttribute
        where TAttributeService : IMessageHandlerAttributeService<TMessageAttribute>
        where TEventArgs : MessageEventArgs
        where TMessageHandler : IMessageHandler<TEventArgs>
    {
        #region Private Members

        private readonly IAssemblyService _assemblyService;
        private readonly TAttributeService _attributeService;

        #endregion

        #region Constructors

        protected MessageHandlerRepositoryBase(
            IAssemblyService assemblyService,
            TAttributeService attributeService)
        {
            _assemblyService = assemblyService;
            _attributeService = attributeService;
        }

        #endregion

        #region Private Methods

        protected IEnumerable<Type> GetTypes()
        {
            var assemblies =  _assemblyService
                .GetAllAssemblies();
            var types = assemblies.GetTypes();
            return types
                .Where(
                    t =>
                        !t.IsAbstract && typeof(TMessageHandler).IsAssignableFrom(t) &&
                        t.HasAttribute<TMessageAttribute>());
        }

        #endregion

        #region Abstract Methods

        protected abstract string GetMessage(TEventArgs message);

        #endregion

        #region Exposed Methods

        public virtual IEnumerable<Type> GetMatchingHandlers(TEventArgs message)
        {
            foreach (var type in GetTypes())
            {
                if (_attributeService.MessageMatches(GetMessage(message), type))
                {
                    yield return type;
                }
            }
        }

        #endregion
    }
}
