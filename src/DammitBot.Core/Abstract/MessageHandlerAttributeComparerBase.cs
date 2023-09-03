using System;
using DammitBot.Events;
using DammitBot.Metadata;

namespace DammitBot.Abstract;

/// <inheritdoc cref="IMessageHandlerAttributeComparer{TAttributeBase}" />
public class MessageHandlerAttributeComparerBase<TAttributeBase>
    : IMessageHandlerAttributeComparer<TAttributeBase>
    where TAttributeBase : Attribute, IHandlesMessageAttribute
{
    #region Private Methods

    /// <summary>
    /// Retrieve attribute of type <typeparamref name="TAttributeBase"/> from the specified
    /// <paramref name="handlerType"/>.
    /// </summary>
    protected static TAttributeBase GetAttribute(Type handlerType)
    {
        return (TAttributeBase)Attribute.GetCustomAttribute(handlerType, typeof(TAttributeBase))!;
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc cref="IMessageHandlerAttributeComparer{TAttributeBase}.MessageMatches" />
    public virtual bool MessageMatches(MessageEventArgs message, Type handlerType)
    {
        if (message.Message == null)
        {
            return false;
        }
        
        var attribute = GetAttribute(handlerType);

        if (attribute.AdminOnly && !message.UserIsAdmin)
        {
            return false;
        }

        return attribute.Regex.IsMatch(message.Message);
    }

    #endregion
}