using DammitBot.Abstract;
using DammitBot.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DammitBot.CommandHandlers
{
    [TestClass]
    public class CommandHandlerFactoryTest : MessageHandlerFactoryTestBase<CommandHandlerFactory, ICommandHandlerRepository, ICommandHandler, CommandEventArgs>
    {}
}
