using System;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Metadata;

namespace DammitBot.MessageHandlers;

/// <inheritdoc />
/// <remarks>
/// This implementation logs any observed messages to the configured database.
/// </remarks>
[HandlesMessage(REGEX)]
public class LogMessageHandler : IMessageHandler
{
    #region Constants

    private const string REGEX = ".*";

    #endregion

    #region Private Members

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="LogMessageHandler"/> class.
    /// </summary>
    /// <param name="unitOfWorkFactory"></param>
    public LogMessageHandler(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    #endregion

    #region Exposed Methods
    
    /// <inheritdoc />
    /// <inheritdoc cref="LogMessageHandler" path="remarks" />
    public void Handle(MessageEventArgs e)
    {
        using var uow = _unitOfWorkFactory.Build();
        var nick = uow
            .GetRepository<INickRepository, Nick>()
            .FindByNicknameAndProtocol(e.User, e.Protocol);

        if (nick == null)
        {
            nick = new Nick {Protocol = e.Protocol, Nickname = e.User};
            nick.Id = Convert.ToInt32(uow.Insert(nick));
        }

        uow.Insert(new Message {
            From = nick,
            Text = e.Message,
            Protocol = e.Protocol,
            Channel = e.Channel
        });

        uow.Commit();
    }

    #endregion
}