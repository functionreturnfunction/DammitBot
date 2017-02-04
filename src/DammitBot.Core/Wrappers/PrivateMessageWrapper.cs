using System.Diagnostics.CodeAnalysis;
using ChatSharp;

namespace DammitBot.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class PrivateMessageWrapper : IPrivateMessage
    {
        #region Private Members

        private readonly PrivateMessage _innerMessage;

        #endregion

        #region Properties

        public string Message => _innerMessage.Message;

        #endregion

        #region Constructors

        public PrivateMessageWrapper(PrivateMessage message)
        {
            _innerMessage = message;
        }

        #endregion
    }
}