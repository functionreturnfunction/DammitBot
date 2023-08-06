using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Wrappers;

namespace DammitBot.CommandHandlers;

/// <inheritdoc cref="ICommandHandlerFactory"/>
public class CommandHandlerFactory
    : MessageHandlerFactoryBase<
            ICommandHandlerTypeService,
            CommandEventArgs,
            ICommandHandler,
            CompositeCommandHandler>,
        ICommandHandlerFactory
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="CommandHandlerFactory"/> class. 
    /// </summary>
    public CommandHandlerFactory(
        ICommandHandlerTypeService handlerTypeService,
        IInstantiationService instantiationService)
        : base(handlerTypeService, instantiationService) {}

    #endregion

    #region Private Methods

    /// <inheritdoc />
    protected override CompositeCommandHandler CreateCompositeHandler(
        IEnumerable<ICommandHandler> handlers)
    {
        return new CompositeCommandHandler(handlers);
    }

    #endregion
}