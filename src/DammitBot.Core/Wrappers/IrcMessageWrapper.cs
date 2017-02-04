using System.Diagnostics.CodeAnalysis;
using ChatSharp;

namespace DammitBot.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class IrcMessageWrapper : IIrcMessage
    {
        #region Private Members

        private readonly IrcMessage _innerMessage;

        #endregion

        #region Properties

        public string RawMessage => _innerMessage.RawMessage;

        #endregion

        #region Constructors

        public IrcMessageWrapper(IrcMessage message)
        {
            _innerMessage = message;
        }

        #endregion
    }
}
