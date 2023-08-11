using System;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Wrappers;

namespace DammitBot.Utilities;

/// <inheritdoc cref="IProtocolService"/>
public class ProtocolService : PluginThingyServiceBase<IProtocol>, IProtocolService
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="ProtocolService"/> class.
    /// </summary>
    public ProtocolService(
        IAssemblyTypeService assemblyTypeService,
        IInstantiationService instantiationService)
        : base(assemblyTypeService, instantiationService) { }

    #endregion

    #region Events/Delegates

    /// <inheritdoc cref="IProtocolService.ChannelMessageReceived"/>
    public event EventHandler<MessageEventArgs> ChannelMessageReceived
    {
        add
        {
            foreach (var protocol in Thingies)
            {
                protocol.ChannelMessageReceived += value;
            }
        }
        remove
        {
            foreach (var protocol in Thingies)
            {
                protocol.ChannelMessageReceived -= value;
            }
        }
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc cref="IProtocolService.SayToAll"/>
    public void SayToAll(string message)
    {
        foreach (var protocol in Thingies)
        {
            protocol.SayToAll(message);
        }
    }

    /// <inheritdoc cref="IProtocolService.SayToChannel"/>
    public void SayToChannel(string protocol, string channel, string message)
    {
        Thingies.Single(x => x.Name == protocol).SayToChannel(channel, message);
    }

    #endregion
}