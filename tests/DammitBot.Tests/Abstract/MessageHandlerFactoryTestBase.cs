using System;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Utilities;
using Lamar;
using Moq;

namespace DammitBot.Abstract;

public abstract class MessageHandlerFactoryTestBase<
        TMessageHandlerFactory,
        TMessageHandler,
        TIMessageHandler,
        TEventArgs>
    : CrazyMessageHandlerThingyTestBase<TMessageHandlerFactory, TMessageHandler, TEventArgs>
    where TMessageHandlerFactory : IMessageHandlerFactory<TIMessageHandler, TEventArgs>
    where TMessageHandler : class, IMessageHandler<TEventArgs>, TIMessageHandler
    where TIMessageHandler : IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs
{
    #region Private Members

    protected Mock<IAssemblyTypeService>? _assemblyTypeService;

    #endregion

    #region Private Methods

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        _assemblyTypeService = serviceRegistry.For<IAssemblyTypeService>().Mock();
    }

    protected override void TestMethod(TEventArgs args)
    {
        if (_handlers == null)
        {
            throw new InvalidOperationException(
                $"{nameof(_handlers)} collection has not yet been initialized, which should " +
                $"have happened in {nameof(ConfigureContainer)}()...");
        }

        if (_assemblyTypeService == null)
        {
            throw new InvalidOperationException(
                $"{nameof(_assemblyTypeService)} has not yet been initialized, which should have" +
                $"happened in {nameof(ConfigureContainer)}()...");
        }

        _assemblyTypeService
            .Setup(r => r.GetTypesFromPluginAssemblies())
            .Returns(_handlers);
        _assemblyTypeService
            .Setup(r => r.GetTypesFromAllAssemblies())
            .Returns(_handlers);

        _target.BuildHandler(args).Handle(args);
    }

    #endregion
}