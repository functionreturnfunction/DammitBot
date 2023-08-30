using DammitBot.Abstract;

namespace DammitBot.Library;

/// <summary>
/// Client which does the work of authenticating/connecting to Slack, and sending/receiving messages.
/// </summary>
public interface ISlackClient : IProtocolClient, IDisposable { }
