using DammitBot.Abstract;
using DammitBot.CommandHandlers;
using DammitBot.Data.Models;
using DammitBot.Events;
using Xunit;

namespace DammitBot.Tests.CommandHandlers;

public class CommandHandlerFactoryTest
    : MessageHandlerFactoryTestBase<
        CommandHandlerFactory,
        ICommandHandlerService,
        ICommandHandler,
        CommandEventArgs>
{
    protected override CommandEventArgs CreateEventArgs()
    {
        return new CommandEventArgs(CreateMessageEventArgs(), new Nick());
    }

    [Fact]
    public override void TestHandleCallsHandleOnEachInnerHandler()
    {
        base.TestHandleCallsHandleOnEachInnerHandler();
    }
}