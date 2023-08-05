using System;
using System.Linq;
using DammitBot.Events;

namespace DammitBot.Abstract;

public abstract class CompositeMessageHandlerTestBase<TCompositeHandler, TMessageHandler, TEventArgs>
    : CrazyMessageHandlerThingyTestBase<TCompositeHandler, TMessageHandler, TEventArgs>
    where TCompositeHandler : class, IMessageHandler<TEventArgs>
    where TMessageHandler : class, IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs
{
    #region Private Methods

    protected override TCompositeHandler ConstructTarget()
    {
        if (_handlers == null)
        {
            throw new InvalidOperationException(
                $"{nameof(_handlers)} collection has not yet been initialized, which should " +
                $"have happened in {nameof(ConfigureContainer)}()...");
        }
        
        var instance = Activator.CreateInstance(
            typeof(TCompositeHandler),
            _handlers.Select(h => (TMessageHandler)_container.GetInstance(h))); 
        return instance as TCompositeHandler ??
               throw new Exception(
                   $"Unable to create instance of type {typeof(TCompositeHandler)}");
    }

    protected override void TestMethod(TEventArgs args)
    {
        _target.Handle(args);
    }

    #endregion
}