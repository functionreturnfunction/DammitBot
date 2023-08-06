using System.Text.RegularExpressions;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.CommandHandlers;

public class CommandHandlerTypeService
    : MessageHandlerTypeServiceBase<
            HandlesCommandAttribute,
            MessageHandlerAttributeComparerBase<HandlesCommandAttribute>,
            CommandEventArgs,
            ICommandHandler>,
        ICommandHandlerTypeService
{
    #region Private Members

    private readonly IBotConfigurationSection _config;

    #endregion

    #region Constructors

    public CommandHandlerTypeService(IAssemblyService assemblyService,
        MessageHandlerAttributeComparerBase<HandlesCommandAttribute> attributeComparer,
        IConfigurationProvider configurationProvider) : base(assemblyService, attributeComparer)
    {
        _config = configurationProvider.BotConfig;
    }

    #endregion

    #region Private Methods

    protected override string GetMessageText(CommandEventArgs message)
    {
        return message.GetCommandText(_config);
    }

    #endregion
}