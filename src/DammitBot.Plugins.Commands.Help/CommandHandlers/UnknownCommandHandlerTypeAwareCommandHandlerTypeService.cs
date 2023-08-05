using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.CommandHandlers;

public class UnknownCommandHandlerTypeAwareCommandHandlerTypeService : CommandHandlerTypeService
{
    #region Constructors

    public UnknownCommandHandlerTypeAwareCommandHandlerTypeService(
        IAssemblyService assemblyService,
        MessageHandlerAttributeComparerBase<HandlesCommandAttribute> attributeComparer,
        IConfigurationProvider configurationProvider)
        : base(assemblyService, attributeComparer, configurationProvider) {}

    #endregion

    #region Exposed Methods

    public override IEnumerable<Type> GetMatchingHandlerTypes(CommandEventArgs message)
    {
        var handlers = base.GetMatchingHandlerTypes(message).ToArray();

        return handlers.Any() ? handlers : new[] {typeof(UnknownCommandHandler)};
    }

    #endregion
}