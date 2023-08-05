using System.Text.RegularExpressions;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.CommandHandlers;

public class CommandHandlerService
    : MessageHandlerServiceBase<
            HandlesCommandAttribute,
            MessageHandlerAttributeComparerBase<HandlesCommandAttribute>,
            CommandEventArgs,
            ICommandHandler>,
        ICommandHandlerService
{
    #region Private Members

    private readonly IBotConfigurationSection _config;

    #endregion

    #region Constructors

    public CommandHandlerService(IAssemblyService assemblyService,
        MessageHandlerAttributeComparerBase<HandlesCommandAttribute> attributeComparer,
        IConfigurationManager configurationManager) : base(assemblyService, attributeComparer)
    {
        _config = configurationManager.BotConfig;
    }

    #endregion

    #region Private Methods

    protected override string GetMessageText(CommandEventArgs message)
    {
        return Regex.Match(message.Message, _config.GoesBy + " (.+)").Groups[1].Value;
    }

    #endregion
}