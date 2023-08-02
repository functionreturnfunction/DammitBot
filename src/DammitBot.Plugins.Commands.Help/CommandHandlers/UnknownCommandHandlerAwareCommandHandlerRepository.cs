using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.CommandHandlers;

public class UnknownCommandHandlerAwareCommandHandlerRepository : CommandHandlerRepository
{
    #region Constructors

    public UnknownCommandHandlerAwareCommandHandlerRepository(
        IAssemblyService assemblyService,
        MessageHandlerAttributeServiceBase<HandlesCommandAttribute> attributeService,
        IConfigurationManager configurationManager)
        : base(assemblyService, attributeService, configurationManager) {}

    #endregion

    #region Exposed Methods

    public override IEnumerable<Type> GetMatchingHandlers(CommandEventArgs message)
    {
        var handlers = base.GetMatchingHandlers(message).ToArray();

        return handlers.Any() ? handlers : new[] {typeof(UnknownCommandHandler)};
    }

    #endregion
}