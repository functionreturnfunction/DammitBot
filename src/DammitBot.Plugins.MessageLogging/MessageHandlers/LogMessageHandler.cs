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

        #region Exposed Methods

        public void Handle(MessageEventArgs e)
        {
        }

        #endregion
    }
}
