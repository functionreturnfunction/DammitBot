using DammitBot.Abstract;
using DammitBot.CommandHandlers;
using DammitBot.Data.Models;
using DammitBot.Events;

namespace DammitBot.Tests.CommandHandlers;

public class CompositeCommandHandlerTest
    : CompositeMessageHandlerTestBase<CompositeCommandHandler, ICommandHandler, CommandEventArgs>
{
    protected override CommandEventArgs CreateEventArgs()
    {
        return new CommandEventArgs(CreateMessageEventArgs(), new Nick());
    }
}