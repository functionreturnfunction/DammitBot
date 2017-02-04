using DammitBot.CommandHandlers;
using DammitBot.Events;
using DammitBot.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DammitBot.MessageHandlers
{
    [TestClass]
    public class CommandMessageHandlerTest : UnitTestBase<CommandMessageHandler>
    {
        #region Private Members

        private Mock<ICommandHandlerFactory> _handlerFactory;

        #endregion

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Inject(out _handlerFactory);
        }

        [TestMethod]
        public void TestHandleUsesFactoryToBuildHandlerAndThenUsesIt()
        {
            var args = new Mock<MessageEventArgs>();
            args.SetupGet(a => a.IrcMessage.RawMessage).Returns("bot foo");
            args.Setup(a => a.PrivateMessage.Message).Returns("bot foo");
            _handlerFactory.Setup(x => x.BuildHandler(It.IsAny<CommandEventArgs>()).Handle(It.IsAny<CommandEventArgs>()));

            _target.Handle(args.Object);

            _handlerFactory.Verify(
                x =>
                    x.BuildHandler(It.Is<CommandEventArgs>(a => a.IrcMessage.RawMessage == "bot foo"))
                        .Handle(It.Is<CommandEventArgs>(a => a.IrcMessage.RawMessage == "bot foo")));
        }
    }
}