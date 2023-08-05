using System;
using DammitBot.Events;
using DammitBot.Library;
using Lamar;
using Moq;

namespace DammitBot.Abstract;

public abstract class MessageHandlerFactoryTestBase<
        TMessageHandlerFactory,
        TMessageHandlerService,
        TMessageHandler,
        TEventArgs>
    : CrazyMessageHandlerThingyTestBase<TMessageHandlerFactory, TMessageHandler, TEventArgs>
    where TMessageHandlerFactory : IMessageHandlerFactory<TMessageHandler, TEventArgs>
    where TMessageHandlerService : class, IMessageHandlerService<TMessageHandler, TEventArgs>
    where TMessageHandler : class, IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs
{
    #region Private Members

    protected Mock<TMessageHandlerService>? _handlerService;

    #endregion

    #region Private Methods

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        _handlerService = serviceRegistry.For<TMessageHandlerService>().Mock();
    }

    protected override void TestMethod(TEventArgs args)
    {
        if (_handlers == null)
        {
            throw new InvalidOperationException(
                $"{nameof(_handlers)} collection has not yet been initialized, which should " +
                $"have happened in {nameof(ConfigureContainer)}()...");
        }

        if (_handlerService == null)
        {
            throw new InvalidOperationException(
                $"{nameof(_handlerService)} has not yet been initialized, which should have" +
                $"happened in {nameof(ConfigureContainer)}()...");
        }

        _handlerService.Setup(r => r.GetMatchingHandlers(args))
            .Returns(_handlers);

        _target.BuildHandler(args).Handle(args);
    }

    #endregion
}