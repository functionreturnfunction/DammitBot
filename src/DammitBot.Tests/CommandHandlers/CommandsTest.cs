using System;
using System.Linq;
using System.Linq.Expressions;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
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
        private Mock<IPersistenceService> _persistenceService;

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

            public void TestCommand(string command)
            {
                _persistenceService.Setup(
                        x =>
                            x.Where(
                                It.Is<Expression<Func<Nick, bool>>>(fn => fn.Compile()(new Nick {Nickname = "foo"}))))
                    .Returns(new[] {new Nick {User = new User()}}.AsQueryable());
                var args = new Mock<MessageEventArgs>();
                args.SetupGet(x => x.PrivateMessage.Message).Returns("bot " + command);
                args.SetupGet(x => x.PrivateMessage.Nick).Returns("foo");
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
            Inject(out _persistenceService);
            Inject(_persistenceService);
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