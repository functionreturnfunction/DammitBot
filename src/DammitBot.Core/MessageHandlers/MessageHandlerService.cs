using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.MessageHandlers;

public class MessageHandlerService
    : MessageHandlerServiceBase<
            HandlesMessageAttribute,
            IMessageHandlerAttributeComparer,
            MessageEventArgs,
            IMessageHandler>,
        IMessageHandlerService
{
    #region Constructors

    public MessageHandlerService(
        IAssemblyService assemblyService,
        IMessageHandlerAttributeComparer attributeComparer)
        : base(assemblyService, attributeComparer) {}

    #endregion

    #region Private Methods

    protected override string? GetMessageText(MessageEventArgs message) => message.Message;

    #endregion
}