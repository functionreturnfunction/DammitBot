using System.Linq;
using DammitBot.Abstract;
using DammitBot.CommandHandlers;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
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
        private readonly IPersistenceService _persistenceService;

        #endregion

        #region Constructors

        public CommandMessageHandler(ICommandHandlerFactory handlerFactory, IPersistenceService persistenceService)
        {
            _handlerFactory = handlerFactory;
            _persistenceService = persistenceService;
        }

        #endregion

        #region Exposed Methods

        public override void Handle(MessageEventArgs e)
        {
            using (_persistenceService)
            {
                var nick = LoadNick(e);
                if (nick?.User == null)
                {
                    return;
                }

                var args = new CommandEventArgs(e, nick);
                _handlerFactory.BuildHandler(args).Handle(args);
            }
        }

        private Nick LoadNick(MessageEventArgs e)
        {
            return _persistenceService.Where<Nick>(n => n.Nickname == e.User).SingleOrDefault();
        }

        #endregion
    }
}