using System.Diagnostics.CodeAnalysis;
using ChatSharp.Events;
using DammitBot.Wrappers;

namespace DammitBot.Protocols.Irc.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class PrivateMessageEventArgsWrapper : IPrivateMessageEventArgs
    {
        #region Private Members

        private readonly PrivateMessageEventArgs _innerArgs;

        #endregion

        #region Properties

        public IIrcMessage IrcMessage => new IrcMessageWrapper(_innerArgs.IrcMessage);

        public IPrivateMessage PrivateMessage => new PrivateMessageWrapper(_innerArgs.PrivateMessage);

        #endregion

        #region Constructors

        public PrivateMessageEventArgsWrapper(PrivateMessageEventArgs args)
        {
            _innerArgs = args;
        }

        #endregion
    }
}
