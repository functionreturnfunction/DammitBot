using DammitBot.Abstract;
using DammitBot.CommandHandlers;
using DammitBot.Events;
using Xunit;

namespace DammitBot.Tests.CommandHandlers;

public class CommandHandlerFactoryTest
    : MessageHandlerFactoryTestBase<
        CommandHandlerFactory,
        ICommandHandlerRepository,
        ICommandHandler,
        CommandEventArgs>
{
    [Fact]
    public override void TestHandleCallsHandleOnEachInnerHandler()
    {
        base.TestHandleCallsHandleOnEachInnerHandler();
    }
}