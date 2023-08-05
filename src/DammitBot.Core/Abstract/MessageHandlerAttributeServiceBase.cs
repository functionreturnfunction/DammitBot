using System;
using DammitBot.Metadata;

namespace DammitBot.Abstract;

public class MessageHandlerAttributeServiceBase<TAttributeBase>
    : IMessageHandlerAttributeService<TAttributeBase>
    where TAttributeBase : Attribute, IHandlesMessageAttribute
{
    #region Private Methods

    protected static TAttributeBase GetAttribute(Type handlerType)
    {
        return (TAttributeBase)Attribute.GetCustomAttribute(handlerType, typeof(TAttributeBase))!;
    }

    #endregion

    #region Exposed Methods

    public virtual bool MessageMatches(string message, Type handlerType)
    {
        var attribute = GetAttribute(handlerType);

        return attribute.Regex.IsMatch(message);
    }

    #endregion
}