using System.Linq;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.TestLibrary;
using Moq;
using Xunit;

namespace DammitBot.CommandHandlers
{
    public class CommandsTest : UnitTestBase<CommandsTest.CommandTester>
    {
        #region Private Members

        private Mock<IBot> _bot;
        private Mock<IPersistenceService> _persistenceService;

        #endregion

        #region Private Methods

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            _container.Configure(i => {
                i.For<ICommandHandlerRepository>().Use<UnknownCommandHandlerAwareCommandHandlerRepository>();
            });

            Inject(out _bot);
            Inject(out _persistenceService);
            Inject(_persistenceService);
        }

        #endregion

        #region Exposed Methods

        [Fact]
        public void TestBotDieCausesBotToDie()
        {
            _target.TestCommand("die");

            _bot.Verify(x => x.Die());
        }

        [Fact]
        public void TestBotRemindMeCausesReminderyThingsToHappen()
        {
            var args = _target.TestCommand("remind me to do things in 1 minute");

            _bot.Verify(x => x.ReplyToMessage(It.IsAny<MessageEventArgs>(), $"Reminder set for {_now.AddMinutes(1)}"));
        }

        [Fact]
        public void TestGetMatchingHandlersReturnsOnlyUnkownCommandHandlerForUnkownCommand()
        {
            _target.TestCommand("asdfasdfasdfasdf");

            _bot.Verify(x => x.SayToAll(string.Format(UnknownCommandHandler.MESSAGE, Bot.DEFAULT_GOES_BY)));
        }

        #endregion

        #region Nested Type: CommandTester

        public class CommandTester
        {
            #region Private Members

            private readonly CommandMessageHandler _handler;
            private readonly Mock<IPersistenceService> _persistenceService;

            #endregion

            #region Constructors

            public CommandTester(CommandMessageHandler handler, Mock<IPersistenceService> svc)
            {
                _handler = handler;
                _persistenceService = svc;
            }

            #endregion

            #region Exposed Methods

            public Mock<MessageEventArgs> TestCommand(string command)
            {
                _persistenceService.Setup(x => x.Query<Nick>())
                    .Returns(new[] {new Nick {Nickname = "foo", User = new User()}}.AsQueryable());
                var args = new Mock<MessageEventArgs>();
                args.SetupGet(x => x.Message).Returns("bot " + command);
                args.SetupGet(x => x.User).Returns("foo");
                _handler.Handle(args.Object);
                return args;
            }

            #endregion
        }

        #endregion
    }
}