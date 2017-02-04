using DammitBot.Abstract;
using DammitBot.CommandHandlers;
using DammitBot.Events;
using DammitBot.Metadata;

namespace DammitBot.MessageHandlers
{
    [HandlesBotMessage]
    public class CommandMessageHandler : MessageHandlerBase<MessageEventArgs>, IMessageHandler
    {
        #region Constants

        public const string REGEX = @"^(?:dammit )?bot (.+)";

        #endregion

        #region Private Members

        private readonly ICommandHandlerFactory _handlerFactory;

        #endregion

        #region Constructors

        public CommandMessageHandler(ICommandHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        #endregion

        #region Exposed Methods

        public override void Handle(MessageEventArgs e)
        {
            var args = new CommandEventArgs(e);
            _handlerFactory.BuildHandler(args).Handle(args);
        }

        #endregion
    }
}