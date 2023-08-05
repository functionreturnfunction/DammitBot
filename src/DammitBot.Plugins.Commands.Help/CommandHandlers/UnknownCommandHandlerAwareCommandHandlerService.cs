using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.CommandHandlers;

public class UnknownCommandHandlerAwareCommandHandlerService : CommandHandlerService
{
    #region Constructors

    public UnknownCommandHandlerAwareCommandHandlerService(
        IAssemblyService assemblyService,
        MessageHandlerAttributeComparerBase<HandlesCommandAttribute> attributeComparer,
        IConfigurationManager configurationManager)
        : base(assemblyService, attributeComparer, configurationManager) {}

    #endregion

    #region Exposed Methods

    public override IEnumerable<Type> GetMatchingHandlers(CommandEventArgs message)
    {
        var handlers = base.GetMatchingHandlers(message).ToArray();

        return handlers.Any() ? handlers : new[] {typeof(UnknownCommandHandler)};
    }

    #endregion
}