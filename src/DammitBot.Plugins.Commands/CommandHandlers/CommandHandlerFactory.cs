using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Wrappers;

namespace DammitBot.CommandHandlers;

public class CommandHandlerFactory
    : MessageHandlerFactoryBase<
            ICommandHandlerTypeService,
            CommandEventArgs,
            ICommandHandler,
            CompositeCommandHandler>,
        ICommandHandlerFactory
{
    #region Constructors

    public CommandHandlerFactory(
        ICommandHandlerTypeService handlerTypeService,
        IInstantiationService instantiationService)
        : base(handlerTypeService, instantiationService) {}

    #endregion

    #region Private Methods

    protected override CompositeCommandHandler CreateCompositeHandler(
        IEnumerable<ICommandHandler> handlers)
    {
        return new CompositeCommandHandler(handlers);
    }

    #endregion
}