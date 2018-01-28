using DammitBot.Abstract;
using DammitBot.Events;
using Xunit;

namespace DammitBot.CommandHandlers
{
    public class CommandHandlerFactoryTest : MessageHandlerFactoryTestBase<CommandHandlerFactory, ICommandHandlerRepository, ICommandHandler, CommandEventArgs>
    {
        [Fact]
        public override void TestHandleCallsHandleOnEachInnerHandler()
        {
            base.TestHandleCallsHandleOnEachInnerHandler();
        }
    }
}
