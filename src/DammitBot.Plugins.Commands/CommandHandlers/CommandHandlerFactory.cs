using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Wrappers;

namespace DammitBot.CommandHandlers
{
    public class CommandHandlerFactory :  MessageHandlerFactoryBase<ICommandHandlerRepository, CommandEventArgs, ICommandHandler, CompositeCommandHandler>, ICommandHandlerFactory
    {
        #region Constructors

        public CommandHandlerFactory(ICommandHandlerRepository repository, IInstantiationService instantiationService) : base(repository, instantiationService) {}

        #endregion

        #region Private Methods

        protected override CompositeCommandHandler CreateCompositeHandler(IEnumerable<ICommandHandler> handlers)
        {
            return new CompositeCommandHandler(handlers);
        }

        #endregion
    }
}