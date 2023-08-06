using System.Text.RegularExpressions;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.CommandHandlers;

/// <inheritdoc cref="ICommandHandlerTypeService" />
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

    /// <summary>
    /// Constructor for the <see cref="CommandHandlerTypeService"/> class.
    /// </summary>
    public CommandHandlerTypeService(
        IAssemblyService assemblyService,
        MessageHandlerAttributeComparerBase<HandlesCommandAttribute> attributeComparer,
        IConfigurationProvider configurationProvider)
        : base(assemblyService, attributeComparer)
    {
        _config = configurationProvider.BotConfig;
    }

    #endregion

    #region Private Methods

    /// <inheritdoc />
    /// <remarks>
    /// This implementation strips the name that the bot goes by from the beginning of the message, so
    /// that only the text of the command is left.
    /// </remarks>
    protected override string GetMessageText(CommandEventArgs message)
    {
        return message.GetCommandText(_config);
    }

    #endregion
}