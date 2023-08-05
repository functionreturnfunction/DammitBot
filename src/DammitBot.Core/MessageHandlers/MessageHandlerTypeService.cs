using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.MessageHandlers;

public class MessageHandlerTypeService
    : MessageHandlerTypeServiceBase<
            HandlesMessageAttribute,
            IMessageHandlerAttributeComparer,
            MessageEventArgs,
            IMessageHandler>,
        IMessageHandlerTypeService
{
    #region Constructors

    public MessageHandlerTypeService(
        IAssemblyService assemblyService,
        IMessageHandlerAttributeComparer attributeComparer)
        : base(assemblyService, attributeComparer) {}

    #endregion

    #region Private Methods

    protected override string? GetMessageText(MessageEventArgs message) => message.Message;

    #endregion
}