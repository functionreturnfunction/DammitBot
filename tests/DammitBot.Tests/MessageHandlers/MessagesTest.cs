using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using DammitBot.CommandHandlers;
using DammitBot.Data.Library;
using DammitBot.Data.Migrations.Library;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.Helpers;
using DammitBot.Scheduling.Library;
using DammitBot.TestLibrary;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using Moq;
using Xunit;

namespace DammitBot.MessageHandlers
{

    public class MessagesTest : UnitTestBase<MessagesTest.MessageTester>
    {
        #region Private Members

        private Mock<ICommandHandlerFactory> _commandHandlerFactory;
        private Mock<IPersistenceService> _persistenceService;
        private Mock<IProtocolService> _protocolService;

        #endregion

        #region Private Methods

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            _container.Configure(i => {
                i.Scan(s => {
                    s.AssemblyContainingType<IBot>();
                    s.WithDefaultConventions();
                });
                i.For<IBot>().Use<Bot>().Singleton();
                i.For<IMessageHandlerAttributeService>().Use<CommandAwareMessageHandlerAttributeService>();
                i.For<IUnitOfWork>().Use<TestUnitOfWork>();
            });

            var migrationService = new Mock<MigrationService>();
            migrationService.SetupGet(x => x.Thingies).Returns(Enumerable.Empty<MigrationBase>());

            Inject(out _commandHandlerFactory);
            Inject<ISchedulerService>();
            Inject<ITeamCityHelper>();
            Inject(out _persistenceService);
            Inject(out _protocolService);
            Inject(_protocolService);

            Inject<MigrationService>(migrationService.Object);
        }

        #endregion

        #region Exposed Methods

        #region Setup/Teardown

        public override void Dispose()
        {
            base.Dispose();
            _target.Dispose();
        }

        #endregion

        public MessagesTest()
        {
            var nicks = new[] {
                new Nick {Nickname = "foo", User = new User()},
                new Nick {Nickname = "bar"}
            };
            _persistenceService.Setup(x => x.Query<Nick>())
                .Returns(nicks.AsQueryable());
        }

        [Fact]
        public void TestAnyMessageIsLogged()
        {
            _target.TestMessage("blah blah blah", "foo");
        }

        [Fact]
        public void TestMessageFromNickWithNoUserIsLogged()
        {
            _target.TestMessage("blah blah blah", "bar");
        }

        [Fact]
        public void TestMessageFromUnkownNickIsLogged()
        {
            _persistenceService.Setup(x => x.Query<Nick>())
                .Returns(new Nick[] {}.AsQueryable());

            _target.TestMessage("blah blah blah", "foo");
        }

        [Fact]
        public void TestCommandRunsCommand()
        {
            _commandHandlerFactory.Setup(
                x => x.BuildHandler(It.IsAny<CommandEventArgs>()).Handle(It.IsAny<CommandEventArgs>()));

            _target.TestMessage("bot blah blah blah", "foo");

            _commandHandlerFactory.Verify(
                x =>
                    x.BuildHandler(It.Is<CommandEventArgs>(a => a.Command == "blah blah blah"))
                        .Handle(It.Is<CommandEventArgs>(a => a.Command == "blah blah blah")));
        }

        [Fact]
        public void TestCommandDoesNotRunCommandIfNickDoesNotHaveUser()
        {
            _commandHandlerFactory.Setup(
                x => x.BuildHandler(It.IsAny<CommandEventArgs>()).Handle(It.IsAny<CommandEventArgs>()));

            _target.TestMessage("bot blah blah blah", "bar");

            _commandHandlerFactory.Verify(
                x =>
                    x.BuildHandler(It.Is<CommandEventArgs>(a => a.Command == "blah blah blah"))
                        .Handle(It.Is<CommandEventArgs>(a => a.Command == "blah blah blah")), Times.Never);
            
        }

        [Fact]
        public void TestCommandDoesNotRunCommandIfNickNotRecognized()
        {
            _commandHandlerFactory.Setup(
                x => x.BuildHandler(It.IsAny<CommandEventArgs>()).Handle(It.IsAny<CommandEventArgs>()));
            _persistenceService.Setup(
x => x.Query<Nick>())
                .Returns(new Nick[] {}.AsQueryable());

            _target.TestMessage("bot blah blah blah", "foo");

            _commandHandlerFactory.Verify(
                x =>
                    x.BuildHandler(It.Is<CommandEventArgs>(a => a.Command == "blah blah blah"))
                        .Handle(It.Is<CommandEventArgs>(a => a.Command == "blah blah blah")), Times.Never);
            
        }

        #endregion

        #region Nested Type: MessageTester

        public class MessageTester : IDisposable
        {
            #region Private Members

            private readonly IInstantiationService _instantiationService;
            private readonly Mock<IProtocolService> _protocolService;

            #endregion

            #region Constructors

            public MessageTester(IInstantiationService instantiationService, Mock<IProtocolService> svc)
            {
                _protocolService = svc;
                _instantiationService = instantiationService;
                var bot = instantiationService.GetInstance<IBot>();
                bot.Die();
                bot.Run();
            }

            #endregion

            #region Exposed Methods

            public void TestMessage(string message, string nick)
            {
                var args = new Mock<MessageEventArgs>();
                args.SetupGet(x => x.Message).Returns(message);
                args.SetupGet(x => x.User).Returns(nick);
                _protocolService.Raise(x => x.ChannelMessageReceived += null, null, args.Object);
            }

            public void Dispose()
            {
                _instantiationService.Dispose();
            }

            #endregion
        }

        #endregion
    }
}
