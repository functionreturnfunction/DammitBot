using System.Diagnostics.CodeAnalysis;
using DammitBot.Wrappers;

namespace DammitBot.Events
{
    public class MessageEventArgs
    {
        #region Private Members

        protected readonly IPrivateMessageEventArgs _innerArgs;

        #endregion

        #region Properties

        [ExcludeFromCodeCoverage]
        public virtual IIrcMessage IrcMessage => _innerArgs.IrcMessage;

        [ExcludeFromCodeCoverage]
        public virtual IPrivateMessage PrivateMessage => _innerArgs.PrivateMessage;

        #endregion

        #region Constructors

        public MessageEventArgs(IPrivateMessageEventArgs args)
        {
            _innerArgs = args;
        }

        /// <summary>
        /// Only used for testing purposes!!!
        /// </summary>
        public MessageEventArgs() : this(null) {}

        #endregion
    }
}
