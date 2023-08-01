using DammitBot.Abstract;
using DammitBot.Metadata;

namespace DammitBot.MessageHandlers
{
    public class MessageHandlerAttributeService
        : MessageHandlerAttributeServiceBase<HandlesMessageAttribute>, IMessageHandlerAttributeService {}
}
