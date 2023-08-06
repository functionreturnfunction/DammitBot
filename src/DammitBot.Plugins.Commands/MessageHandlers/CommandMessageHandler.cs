using System.Linq;
using DammitBot.CommandHandlers;
using DammitBot.Configuration;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.MessageHandlers;

[HandlesBotMessage]
public class CommandMessageHandler : IMessageHandler
{
    #region Private Members

    private readonly ICommandHandlerFactory _handlerFactory;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IBotConfigurationSection _config;

    #endregion

    #region Constructors

    public CommandMessageHandler(
        ICommandHandlerFactory handlerFactory,
        IUnitOfWorkFactory unitOfWorkFactory,
        IConfigurationProvider configurationProvider)
    {
        _handlerFactory = handlerFactory;
        _unitOfWorkFactory = unitOfWorkFactory;
        _config = configurationProvider.BotConfig;
    }

    #endregion

    #region Exposed Methods

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

    private Nick? LoadNick(IUnitOfWork uow, MessageEventArgs e)
    {
        return uow.Query<Nick>().SingleOrDefault(n => n.Nickname == e.User);
    }

    #endregion
}