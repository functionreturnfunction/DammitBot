namespace DammitBot.Wrappers
{
    public interface IPrivateMessageEventArgs
    {
        #region Abstract Properties

        IIrcMessage IrcMessage { get; }
        IPrivateMessage PrivateMessage { get; }

        #endregion
    }
}