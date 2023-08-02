using System;
using System.Collections.Generic;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Wrappers;

using Moq;
using Xunit;

namespace DammitBot.Abstract;

public abstract class CrazyMessageHandlerThingyTestBase<TCrazyThingy, TMessageHandler, TEventArgs>
    : UnitTestBase<TCrazyThingy>
    where TMessageHandler : class, IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs, new()
{
    #region Constants

    public const int SAMPLE_SIZE = 5;

    #endregion

    #region Private Members

    protected IEnumerable<Type> _handlers;

    #endregion

    #region Properties

    protected virtual int SampleSite => SAMPLE_SIZE;

    #endregion

    #region Private Methods

    protected override void ConfigureContainer()
    {
        base.ConfigureContainer();
        _handlers = new List<Type> {typeof(TMessageHandler)};
        _container.Inject<IInstantiationService>(new InstantiationService(_container));
    }

    #endregion

    #region Abstract Methods

    protected abstract void TestMethod(TEventArgs args);

    #endregion

    #region Test Methods

    public virtual void TestHandleCallsHandleOnEachInnerHandler()
    {
        var args = new TEventArgs();
        var instances = new List<Mock<TMessageHandler>>();

        foreach (var handler in _handlers)
        {
            var instance = new Mock<TMessageHandler>();
            _container.Inject(handler, instance.Object);
            instances.Add(instance);
        }

        TestMethod(args);

        foreach (var instance in instances)
        {
            instance.Verify(h => h.Handle(args));
        }
    }

    #endregion
}