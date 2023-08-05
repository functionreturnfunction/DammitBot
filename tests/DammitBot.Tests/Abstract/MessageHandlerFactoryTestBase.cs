using System;
using DammitBot.Events;
using DammitBot.Library;
using Lamar;
using Moq;

namespace DammitBot.Abstract;

public abstract class MessageHandlerFactoryTestBase<
        TMessageHandlerFactory,
        TMessageHandlerTypeService,
        TMessageHandler,
        TEventArgs>
    : CrazyMessageHandlerThingyTestBase<TMessageHandlerFactory, TMessageHandler, TEventArgs>
    where TMessageHandlerFactory : IMessageHandlerFactory<TMessageHandler, TEventArgs>
    where TMessageHandlerTypeService : class, IMessageHandlerTypeService<TMessageHandler, TEventArgs>
    where TMessageHandler : class, IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs
{
    #region Private Members

    protected Mock<TMessageHandlerTypeService>? _handlerTypeService;

    #endregion

    #region Private Methods

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        _handlerTypeService = serviceRegistry.For<TMessageHandlerTypeService>().Mock();
    }

    protected override void TestMethod(TEventArgs args)
    {
        if (_handlers == null)
        {
            throw new InvalidOperationException(
                $"{nameof(_handlers)} collection has not yet been initialized, which should " +
                $"have happened in {nameof(ConfigureContainer)}()...");
        }

        if (_handlerTypeService == null)
        {
            throw new InvalidOperationException(
                $"{nameof(_handlerTypeService)} has not yet been initialized, which should have" +
                $"happened in {nameof(ConfigureContainer)}()...");
        }

        _handlerTypeService.Setup(r => r.GetMatchingHandlerTypes(args))
            .Returns(_handlers);

        _target.BuildHandler(args).Handle(args);
    }

    #endregion
}