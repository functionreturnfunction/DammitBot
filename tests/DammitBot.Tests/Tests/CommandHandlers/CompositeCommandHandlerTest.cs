using DammitBot.Abstract;
using DammitBot.CommandHandlers;
using DammitBot.Events;

namespace DammitBot.Tests.CommandHandlers;

public class CompositeCommandHandlerTest
    : CompositeMessageHandlerTestBase<CompositeCommandHandler, ICommandHandler, CommandEventArgs> { }