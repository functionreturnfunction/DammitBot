using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;
using Microsoft.Extensions.Options;

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

    private readonly BotConfiguration _config;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="CommandHandlerTypeService"/> class.
    /// </summary>
    public CommandHandlerTypeService(
        IAssemblyTypeService assemblyTypeService,
        MessageHandlerAttributeComparerBase<HandlesCommandAttribute> attributeComparer,
        IOptions<BotConfiguration> botConfig)
        : base(assemblyTypeService, attributeComparer)
    {
        _config = botConfig.Value;
    }

    #endregion
}