using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.Abstract;

/// <inheritdoc cref="IMessageHandlerTypeService{TMessageHandler,TMessageEventArgs}"/>
/// <remarks>
/// This implementation uses a <see cref="IAssemblyTypeService"/> to gather all available assemblies to find
/// types from, and a <typeparamref name="TAttributeComparer"/> to filter those types based on the text of
/// a given message. 
/// </remarks>
public abstract class MessageHandlerTypeServiceBase<
        TMessageAttribute,
        TAttributeComparer,
        TMessageEventArgs,
        TMessageHandler>
    : IMessageHandlerTypeService<TMessageHandler, TMessageEventArgs>
    where TMessageAttribute : Attribute, IHandlesMessageAttribute
    where TAttributeComparer : IMessageHandlerAttributeComparer<TMessageAttribute>
    where TMessageEventArgs : MessageEventArgs
    where TMessageHandler : IMessageHandler<TMessageEventArgs>
{
    #region Private Members

    private readonly IAssemblyTypeService _assemblyTypeService;
    private readonly TAttributeComparer _attributeComparer;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the
    /// <see cref="MessageHandlerTypeServiceBase{TMessageAttribute,TAttributeComparer,TMessageEventArgs,TMessageHandler}"/>
    /// class
    /// </summary>
    protected MessageHandlerTypeServiceBase(
        IAssemblyTypeService assemblyTypeService,
        TAttributeComparer attributeComparer)
    {
        _assemblyTypeService = assemblyTypeService;
        _attributeComparer = attributeComparer;
    }

    #endregion

    #region Private Methods

    private IEnumerable<Type> GetTypes()
    {
        var types =  _assemblyTypeService
            .GetTypesFromAllAssemblies();
        return types
            .Where(
                t =>
                    !t.IsAbstract && typeof(TMessageHandler).IsAssignableFrom(t) &&
                    t.HasAttribute<TMessageAttribute>());
    }

    #endregion

    #region Abstract Methods

    /// <summary>
    /// Return the text of the supplied <paramref name="message"/>. 
    /// </summary>
    protected abstract string? GetMessageText(TMessageEventArgs message);

    #endregion

    #region Exposed Methods

    /// <inheritdoc cref="IMessageHandlerTypeService{TMessageHandler,TMessageEventArgs}.GetMatchingHandlerTypes"/>
    public virtual IEnumerable<Type> GetMatchingHandlerTypes(TMessageEventArgs message)
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