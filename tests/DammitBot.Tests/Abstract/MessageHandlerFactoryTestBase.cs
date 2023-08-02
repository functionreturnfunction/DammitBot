using System;
using DammitBot.Events;
using Moq;

namespace DammitBot.Abstract;

public abstract class MessageHandlerFactoryTestBase<
        TMessageHandlerFactory,
        TMessageHandlerRepository,
        TMessageHandler,
        TEventArgs>
    : CrazyMessageHandlerThingyTestBase<TMessageHandlerFactory, TMessageHandler, TEventArgs>
    where TMessageHandlerFactory : IHandlerFactory<TMessageHandler, TEventArgs>
    where TMessageHandlerRepository : class, IMessageHandlerRepository<TMessageHandler, TEventArgs>
    where TMessageHandler : class, IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs, new()
{
    #region Private Members

    protected Mock<TMessageHandlerRepository>? _repository;

    #endregion

    #region Private Methods

    protected override void ConfigureContainer()
    {
        base.ConfigureContainer();

        Inject(out _repository);
    }

    protected override void TestMethod(TEventArgs args)
    {
        if (_handlers == null)
        {
            throw new InvalidOperationException(
                $"{nameof(_handlers)} collection has not yet been initialized, which should " +
                $"have happened in {nameof(ConfigureContainer)}()...");
        }

        if (_repository == null)
        {
            throw new InvalidOperationException(
                $"{nameof(_repository)} has not yet been initialized, which should have" +
                $"happened in {nameof(ConfigureContainer)}()...");
        }

        _repository.Setup(r => r.GetMatchingHandlers(args))
            .Returns(_handlers);

        _target.BuildHandler(args).Handle(args);
    }

    #endregion
}