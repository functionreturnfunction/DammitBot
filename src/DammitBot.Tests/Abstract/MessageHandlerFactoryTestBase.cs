using System.Linq;
using DammitBot.Events;
using Moq;

namespace DammitBot.Abstract
{
    public abstract class MessageHandlerFactoryTestBase<TMessageHandlerFactory, TMessageHandlerRepository, TMessageHandler, TEventArgs> :
            CrazyMessageHandlerThingyTestBase<TMessageHandlerFactory, TMessageHandler, TEventArgs>
        where TMessageHandlerFactory : IHandlerFactory<TMessageHandler, TEventArgs>
        where TMessageHandlerRepository : class, IMessageHandlerRepository<TMessageHandler, TEventArgs>
        where TMessageHandler : class, IMessageHandler<TEventArgs>
        where TEventArgs : MessageEventArgs, new()
    {
        protected Mock<TMessageHandlerRepository> _repository;

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Inject(out _repository);
        }

        #region Private Methods

        protected override void TestMethod(TEventArgs args)
        {
            _repository.Setup(r => r.GetMatchingHandlers(args)).Returns(_handlers);

            _target.BuildHandler(args).Handle(args);
        }

        #endregion
    }
}