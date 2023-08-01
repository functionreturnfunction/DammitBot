using System;
using DammitBot.CommandHandlers;
using DammitBot.Configuration;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.MessageHandlers;
using Moq;
using Xunit;

namespace DammitBot.Tests.CommandHandlers
{
    public class CommandsTest : InMemoryDatabaseUnitTestBase<CommandsTest.CommandTester>
    {
        #region Private Members

        private Mock<IBot> _bot;
        private Mock<IConfigurationManager> _configurationManager;
        private Nick[] _nicks;

        #endregion

        public CommandsTest()
        {
            WithUnitOfWork(uow => {
                var foo = new User {Username = "foo"};
                foo.Id = Convert.ToInt32(uow.Insert<User>(foo));
                uow.Insert<User>(new User {Username = "bar"});
                uow.Insert<Nick>(new Nick {Nickname = "foo", User = foo});

                uow.Commit();
            });
        }

        #region Private Methods

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            _container.Configure(i => {
                i.For<ICommandHandlerRepository>().Use<UnknownCommandHandlerAwareCommandHandlerRepository>();
            });

            Inject(out _bot);
            Inject(out _configurationManager);
            _configurationManager.Setup(x => x.BotConfig.GoesBy).Returns("(?:dammit )?bot");
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
        public void TestBotRemindOtherUserAlsoCausesReminderyThingsToHappen()
        {
            var args = _target.TestCommand("remind bar to do things in 1 minute");

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

            #endregion

            #region Constructors

            public CommandTester(CommandMessageHandler handler)
            {
                _handler = handler;
            }

            #endregion

            #region Exposed Methods

            public Mock<MessageEventArgs> TestCommand(string command)
            {
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