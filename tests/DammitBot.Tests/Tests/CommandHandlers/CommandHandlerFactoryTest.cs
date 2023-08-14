using DammitBot.Abstract;
using DammitBot.CommandHandlers;
using DammitBot.Data.Models.Fakers;
using DammitBot.Events;
using DammitBot.Metadata;
using Xunit;

namespace DammitBot.Tests.CommandHandlers;

public class CommandHandlerFactoryTest
    : MessageHandlerFactoryTestBase<
        CommandHandlerFactory,
        CommandHandlerFactoryTest.TestCommandHandler,
        ICommandHandler,
        CommandEventArgs>
{
    protected override MessageEventArgs CreateMessageEventArgs()
    {
        return new MessageEventArgs("bot foo", "channel", "protocol", "user");
    }

    protected override CommandEventArgs CreateEventArgs()
    {
        return new CommandEventArgs(
            CreateMessageEventArgs(),
            "foo",
            new NickFaker().Generate());
    }

    [Fact]
    public override void Test_Handle_CallsHandleOnEachInnerHandler()
    {
        base.Test_Handle_CallsHandleOnEachInnerHandler();
    }
    
    [HandlesCommand(".*", "bar")]
    public class TestCommandHandler : ICommandHandler
    {
        public virtual void Handle(CommandEventArgs e) { }
    }
}