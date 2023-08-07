using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.CommandHandlers;

/// <inheritdoc />
/// <remarks>
/// This implementation will provide a special <see cref="UnknownCommandHandler"/> type if no matches are
/// found for the command message.
/// </remarks>
public class UnknownCommandHandlerTypeAwareCommandHandlerTypeService : CommandHandlerTypeService
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="UnknownCommandHandlerTypeAwareCommandHandlerTypeService"/> class.
    /// </summary>
    public UnknownCommandHandlerTypeAwareCommandHandlerTypeService(
        IAssemblyService assemblyService,
        MessageHandlerAttributeComparerBase<HandlesCommandAttribute> attributeComparer,
        IConfigurationProvider configurationProvider)
        : base(assemblyService, attributeComparer, configurationProvider) {}

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    /// <inheritdoc cref="UnknownCommandHandlerTypeAwareCommandHandlerTypeService" path="remarks"/>
    public override IEnumerable<Type> GetMatchingHandlerTypes(CommandEventArgs message)
    {
        var handlers = base.GetMatchingHandlerTypes(message).ToArray();

        return handlers.Any() ? handlers : new[] {typeof(UnknownCommandHandler)};
    }

    #endregion
}