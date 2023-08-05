using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.Abstract;

public abstract class MessageHandlerTypeServiceBase<
        TMessageAttribute,
        TAttributeService,
        TEventArgs,
        TMessageHandler>
    : IMessageHandlerTypeService<TMessageHandler, TEventArgs>
    where TMessageAttribute : Attribute, IHandlesMessageAttribute
    where TAttributeService : IMessageHandlerAttributeComparer<TMessageAttribute>
    where TEventArgs : MessageEventArgs
    where TMessageHandler : IMessageHandler<TEventArgs>
{
    #region Private Members

    private readonly IAssemblyService _assemblyService;
    private readonly TAttributeService _attributeComparer;

    #endregion

    #region Constructors

    protected MessageHandlerTypeServiceBase(
        IAssemblyService assemblyService,
        TAttributeService attributeComparer)
    {
        _assemblyService = assemblyService;
        _attributeComparer = attributeComparer;
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

    protected abstract string? GetMessageText(TEventArgs message);

    #endregion

    #region Exposed Methods

    public virtual IEnumerable<Type> GetMatchingHandlers(TEventArgs message)
    {
        var messageText = GetMessageText(message);

        if (messageText == null)
        {
            yield break;
        }
        
        foreach (var type in GetTypes())
        {
            if (_attributeComparer.MessageMatches(messageText, type))
            {
                yield return type;
            }
        }
    }

    #endregion
}