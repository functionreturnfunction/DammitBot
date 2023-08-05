using System.Diagnostics.CodeAnalysis;
using DammitBot.Library;
using IrcDotNet;

namespace DammitBot.Wrappers;

[ExcludeFromCodeCoverage]
public class IrcMessageWrapper : IIrcMessage
{
    #region Private Members

    private readonly IrcClient.IrcMessage _innerMessage;

    #endregion

    #region Properties

    public string RawMessage { get; }

    #endregion

    #region Constructors

    public IrcMessageWrapper(string rawContent, IrcClient.IrcMessage message)
    {
        RawMessage = rawContent;
        _innerMessage = message;
    }

    #endregion
}