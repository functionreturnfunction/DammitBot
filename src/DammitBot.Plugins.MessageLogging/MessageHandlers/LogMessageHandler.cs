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

        private readonly IPersistenceService _persistenceService;

        #endregion

        #region Constructors

        public LogMessageHandler(IPersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
        }

        #endregion

        #region Exposed Methods

        public void Handle(MessageEventArgs e)
        {
            using (_persistenceService)
            {
                var nick = _persistenceService.Query<Nick>().SingleOrDefault(n => n.Nickname == e.User);

                if (nick == null)
                {
                    nick = new Nick {Nickname = e.User};
                    _persistenceService.Save(nick);
                }

                _persistenceService.Save(new Message {
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
