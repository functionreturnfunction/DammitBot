using System;
using System.Linq;
using DammitBot.Events;

namespace DammitBot.Abstract;

public abstract class CompositeMessageHandlerTestBase<TCompositeHandler, TMessageHandler, TEventArgs>
    : CrazyMessageHandlerThingyTestBase<TCompositeHandler, TMessageHandler, TEventArgs>
    where TCompositeHandler : IMessageHandler<TEventArgs>
    where TMessageHandler : class, IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs, new()
{
    #region Private Methods

    protected override TCompositeHandler ConstructTarget()
    {
        return (TCompositeHandler)Activator.CreateInstance(
            typeof(TCompositeHandler),
            _handlers.Select(h => (TMessageHandler)_container.GetInstance(h)));
    }

    protected override void TestMethod(TEventArgs args)
    {
        _target.Handle(args);
    }

    #endregion
}