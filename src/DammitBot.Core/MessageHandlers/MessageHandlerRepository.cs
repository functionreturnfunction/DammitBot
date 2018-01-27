using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.MessageHandlers
{
    public class MessageHandlerRepository : MessageHandlerRepositoryBase<HandlesMessageAttribute, IMessageHandlerAttributeService, MessageEventArgs, IMessageHandler>, IMessageHandlerRepository
    {
        #region Constructors

        public MessageHandlerRepository(IAssemblyService assemblyService, IMessageHandlerAttributeService attributeService) : base(assemblyService, attributeService) {}

        #endregion

        #region Private Methods

        protected override string GetMessage(MessageEventArgs message) => message.Message;

        #endregion
    }
}