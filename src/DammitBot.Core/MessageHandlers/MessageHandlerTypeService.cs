using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.MessageHandlers;

/// <summary>
/// Service which provides inheriting/implementing types of <see cref="IMessageHandler"/> which
/// can handle messages represented by <see cref="MessageEventArgs"/>.
/// </summary>
public class MessageHandlerTypeService
    : MessageHandlerTypeServiceBase<
            HandlesMessageAttribute,
            IMessageHandlerAttributeComparer,
            MessageEventArgs,
            IMessageHandler>,
        IMessageHandlerTypeService
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="MessageHandlerTypeService"/> class.
    /// </summary>
    public MessageHandlerTypeService(
        IAssemblyTypeService assemblyTypeService,
        IMessageHandlerAttributeComparer attributeComparer)
        : base(assemblyTypeService, attributeComparer) {}

    #endregion

    #region Private Methods

    /// <inheritdoc cref="MessageHandlerTypeServiceBase{HandlesMessageAttribute,IMessagHandlerAttributeComparer,MessageEventArgs,IMessageHandler}.GetMessageText"/>
    protected override string? GetMessageText(MessageEventArgs message) => message.Message;

    #endregion
}