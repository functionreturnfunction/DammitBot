using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using DammitBot.Data.Models;
using DammitBot.MessageHandlers;
using DammitBot.Wrappers;

namespace DammitBot.Events
{
    public class CommandEventArgs : MessageEventArgs
    {
        #region Private Members

        private readonly MessageEventArgs _innerMessageArgs;
        private readonly Nick _from;

        #endregion

        #region Properties

        public virtual string Command { get; }

        [ExcludeFromCodeCoverage]
        public override IPrivateMessage PrivateMessage => _innerArgs?.PrivateMessage ?? _innerMessageArgs.PrivateMessage;

        [ExcludeFromCodeCoverage]
        public override IIrcMessage IrcMessage => _innerArgs?.IrcMessage ?? _innerMessageArgs.IrcMessage;

        public virtual Nick From { get; }

        #endregion

        #region Constructors

        [ExcludeFromCodeCoverage]
        public CommandEventArgs(IPrivateMessageEventArgs args) : base(args)
        {
            if (args != null)
            {
                Command = ReadCommand(args.PrivateMessage.Message);
            }
        }

        public CommandEventArgs(MessageEventArgs args, Nick from) : this((IPrivateMessageEventArgs)null)
        {
            _innerMessageArgs = args;
            _from = from;
            Command = ReadCommand(args.PrivateMessage.Message);
        }

        /// <summary>
        /// Only used for testing purposes!!
        /// </summary>
        public CommandEventArgs() { }

        #endregion

        #region Private Methods

        private string ReadCommand(string message)
        {
            return Regex.Match(message, CommandMessageHandler.REGEX).Groups[1].ToString();
        }

        #endregion
    }
}