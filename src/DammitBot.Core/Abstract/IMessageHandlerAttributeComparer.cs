using System;
using DammitBot.Events;
using DammitBot.Metadata;

namespace DammitBot.Abstract;

/// <summary>
/// Service which determines whether a given <see cref="Type"/> has a
/// <typeparamref name="TAttributeBase"/> which matches a given message.
/// </summary>
public interface IMessageHandlerAttributeComparer<TAttributeBase>
    where TAttributeBase : Attribute, IHandlesMessageAttribute
{
    #region Abstract Methods

    /// <summary>
    /// Returns a boolean indicating whether or not the supplied <paramref name="handlerType"/> has a
    /// <typeparamref name="TAttributeBase"/> which matches the supplied <paramref name="message"/>.
    /// </summary>
    bool MessageMatches(MessageEventArgs message, Type handlerType);
    
    #endregion
}