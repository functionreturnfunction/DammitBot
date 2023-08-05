using DammitBot.Abstract;
using DammitBot.Metadata;

namespace DammitBot.MessageHandlers;

public class MessageHandlerAttributeComparer
    : MessageHandlerAttributeComparerBase<HandlesMessageAttribute>, IMessageHandlerAttributeComparer {}