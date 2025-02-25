﻿using DammitBot.CommandHandlers;
using DammitBot.Configuration;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Metadata;
using DammitBot.Utilities;
using Microsoft.Extensions.Options;

namespace DammitBot.MessageHandlers;

/// <inheritdoc />
/// <remarks>
/// This implementation checks if messages can be considered commands, and uses a
/// <see cref="ICommandHandlerFactory"/> to build an <see cref="ICommandHandler"/> to handle them if they
/// can. 
/// </remarks>
[HandlesBotMessage]
public class CommandMessageHandler : IMessageHandler
{
    #region Private Members

    private readonly ICommandHandlerFactory _handlerFactory;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly BotConfiguration _config;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="CommandMessageHandler"/> class.
    /// </summary>
    public CommandMessageHandler(
        ICommandHandlerFactory handlerFactory,
        IUnitOfWorkFactory unitOfWorkFactory,
        IOptions<BotConfiguration> botConfig)
    {
        _handlerFactory = handlerFactory;
        _unitOfWorkFactory = unitOfWorkFactory;
        _config = botConfig.Value;
    }

    #endregion
    
    #region Private Methods

    private static Nick? LoadNick(IUnitOfWork uow, MessageEventArgs e)
    {
        return uow
            .GetRepository<INickRepository, Nick>()
            .FindByNicknameAndProtocol(e.User, e.Protocol);
    }
    
    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    /// <inheritdoc cref="CommandMessageHandler" path="remarks" />
    public void Handle(MessageEventArgs e)
    {
        CommandEventArgs? args;
        using (var uow = _unitOfWorkFactory.Build())
        {
            var nick = LoadNick(uow, e);
            if (nick?.User == null)
            {
                return;
            }

            args = new CommandEventArgs(e, e.GetCommandText(_config), nick);
        }

        _handlerFactory.BuildHandler(args).Handle(args);
    }

    #endregion
}