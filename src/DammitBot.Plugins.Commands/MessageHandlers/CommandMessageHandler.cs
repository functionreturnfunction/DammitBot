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
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        #endregion

        #region Constructors

        public CommandMessageHandler(ICommandHandlerFactory handlerFactory, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _handlerFactory = handlerFactory;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #endregion

        #region Exposed Methods

        public override void Handle(MessageEventArgs e)
        {
            using (var uow = _unitOfWorkFactory.Build().Start())
            {
                var nick = LoadNick(uow, e);
                if (nick?.User == null)
                {
                    return;
                }

                var args = new CommandEventArgs(e, nick);
                _handlerFactory.BuildHandler(args).Handle(args);
            }
        }

        private Nick LoadNick(IDisposableUnitOfWork uow, MessageEventArgs e)
        {
            return uow.Query<Nick>().SingleOrDefault(n => n.Nickname == e.User);
        }

        #endregion
    }
}