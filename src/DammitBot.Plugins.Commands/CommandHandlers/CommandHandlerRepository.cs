using System.Text.RegularExpressions;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.CommandHandlers;

public class CommandHandlerRepository
    : MessageHandlerRepositoryBase<
            HandlesCommandAttribute,
            MessageHandlerAttributeServiceBase<HandlesCommandAttribute>,
            CommandEventArgs,
            ICommandHandler>,
        ICommandHandlerRepository
{
    #region Private Members

    private readonly IBotConfigurationSection _config;

    #endregion

    #region Constructors

    public CommandHandlerRepository(IAssemblyService assemblyService,
        MessageHandlerAttributeServiceBase<HandlesCommandAttribute> attributeService,
        IConfigurationManager configurationManager) : base(assemblyService, attributeService)
    {
        _config = configurationManager.BotConfig;
    }

    #endregion

    #region Private Methods

    protected override string GetMessage(CommandEventArgs message)
    {
        return Regex.Match(message.Message, _config.GoesBy + " (.+)").Groups[1].Value;
    }

    #endregion
}