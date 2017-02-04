using System.Text.RegularExpressions;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.CommandHandlers
{
    public class CommandHandlerRepository : MessageHandlerRepositoryBase<HandlesCommandAttribute, MessageHandlerAttributeServiceBase<HandlesCommandAttribute>, CommandEventArgs, ICommandHandler>, ICommandHandlerRepository
    {
        #region Constructors

        public CommandHandlerRepository(IAssemblyService assemblyService, MessageHandlerAttributeServiceBase<HandlesCommandAttribute> attributeService) : base(assemblyService, attributeService) {}

        #endregion

        #region Private Methods

        protected override string GetMessage(CommandEventArgs message)
        {
            return Regex.Match(message.PrivateMessage.Message, CommandMessageHandler.REGEX).Groups[1].Value;
        }

        #endregion
    }
}