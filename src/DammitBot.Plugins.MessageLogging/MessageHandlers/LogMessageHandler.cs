using System.Linq;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.Metadata;

namespace DammitBot.MessageHandlers
{
    [HandlesMessage(REGEX)]
    public class LogMessageHandler : IMessageHandler
    {
        #region Constants

        public const string REGEX = ".*";

        #endregion

        #region Private Members

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        #endregion

        #region Constructors

        public LogMessageHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #endregion

        #region Exposed Methods

        public void Handle(MessageEventArgs e)
        {
            using (var uow = _unitOfWorkFactory.Build())
            {
                var nickRepo = uow.GetRepository<Nick>();
                var messageRepo = uow.GetRepository<Message>();

                var nick = nickRepo.Where(n => n.Nickname == e.PrivateMessage.Nick).FirstOrDefault();

                if (nick == null)
                {
                    nick = new Nick {Nickname = e.PrivateMessage.Nick};
                    nickRepo.Save(nick);
                }

                messageRepo.Save(new Message {From = nick, Text = e.PrivateMessage.Message});

                uow.Commit();
            }
        }

        #endregion
    }
}
