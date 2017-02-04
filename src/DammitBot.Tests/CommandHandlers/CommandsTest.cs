using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DammitBot.CommandHandlers
{
    [TestClass]
    public class CommandsTest : UnitTestBase<CommandsTest.CommandTester>
    {
        #region Private Members

        private Mock<IBot> _bot;

        #endregion

        #region Nested Type: CommandTester

        public class CommandTester
        {
            #region Private Members

            private readonly CommandMessageHandler _handler;

            #endregion

            #region Constructors

            public CommandTester(CommandMessageHandler handler)
            {
                _handler = handler;
            }

            #endregion

            #region Exposed Methods

            public void TestCommand(string command)
            {
                var args = new Mock<MessageEventArgs>();
                args.SetupGet(x => x.PrivateMessage.Message).Returns("bot " + command);
                _handler.Handle(args.Object);
            }

            #endregion
        }

        #endregion

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            _container.Configure(i => {
                i.Scan(s => {
                    s.AssembliesFromApplicationBaseDirectory();
                    s.WithDefaultConventions();
                });
                i.For<ICommandHandlerRepository>().Use<UnknownCommandHandlerAwareCommandHandlerRepository>();
            });

            Inject(out _bot);
        }

        [TestMethod]
        public void TestBotDieCausesBotToDie()
        {
            _target.TestCommand("die");

            _bot.Verify(x => x.Die());
        }

        [TestMethod]
        public void TestGetMatchingHandlersReturnsOnlyUnkownCommandHandlerForUnkownCommand()
        {
            _target.TestCommand("asdfasdfasdfasdf");

            _bot.Verify(x => x.SayInChannel(string.Format(UnknownCommandHandler.MESSAGE, Bot.DEFAULT_GOES_BY)));
        }
    }
}