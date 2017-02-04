using System;
using DammitBot.Metadata;

namespace DammitBot.Abstract
{
    public interface IMessageHandlerAttributeService<TAttributeBase>
        where TAttributeBase : Attribute, IHandlesMessageAttribute
    {
        bool MessageMatches(string message, Type handlerType);
    }
}