using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace DammitBot.Tests.MessageHandlers;

public class MessageHandlerFactoryTest
    : MessageHandlerFactoryTestBase<
        MessageHandlerFactory,
        MessageHandlerFactoryTest.TestMessageHandler,
        IMessageHandler,
        MessageEventArgs>
{
    protected override MessageEventArgs CreateEventArgs() => CreateMessageEventArgs();

    [Fact]
    public override void Test_Handle_CallsHandleOnEachInnerHandler()
    {
        base.Test_Handle_CallsHandleOnEachInnerHandler();
    }

    [Fact]
    public void Test_Handle_DoesNothingWithNullMessage()
    {
        var args = new MessageEventArgs(null!, "channel", "protocol", "user");
        var instances = new List<Mock<TestMessageHandler>>();

        _container.Configure(services =>
        {
            foreach (var handler in _handlers!)
            {
                var instance = new Mock<TestMessageHandler>();
                services.Add(new ServiceDescriptor(handler, instance.Object));
                instances.Add(instance);
            }
        });

        TestMethod(args);

        foreach (var instance in instances)
        {
            instance.Verify(
                h => h.Handle(It.IsAny<MessageEventArgs>()),
                Times.Never);
        }
    }

    [HandlesMessage(".*")]
    public class TestMessageHandler : IMessageHandler
    {
        public virtual void Handle(MessageEventArgs e) {}
    }
}