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
            using (var uow = _unitOfWorkFactory.Build().Start())
            {
                var nick = uow.Query<Nick>().SingleOrDefault(n => n.Nickname == e.User);

                if (nick == null)
                {
                    nick = new Nick {Nickname = e.User};
                    uow.Insert(nick);
                }

                uow.Insert(new Message {
                    From = nick,
                    Text = e.Message,
                    Protocol = e.Protocol,
                    Channel = e.Channel
                });
            }
        }

        #endregion
    }
}
