using System;
using System.Linq.Expressions;
using DammitBot.CommandHandlers;
using DammitBot.Configuration;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.Helpers;
using DammitBot.Scheduling.Library;
using DammitBot.TestLibrary;
using DammitBot.Wrappers;

using Moq;
using System.Linq;
using DammitBot.Utilities;
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

        #region Setup/Teardown

        public override void Dispose()
        {
            base.Dispose();
            _target.Dispose();
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
            });

            Inject(out _commandHandlerFactory);
            Inject(new Mock<ISchedulerService>().Object);
            Inject(new Mock<ITeamCityHelper>().Object);
            Inject(out _persistenceService);
            Inject(out _protocolService);
            Inject(_protocolService);
        }

        #endregion

        [Fact]
        public void TestAnyMessageIsLogged()
        {
            _persistenceService.Setup(
                    x => x.Where(It.Is<Expression<Func<Nick, bool>>>(fn => fn.Compile()(new Nick {Nickname = "foo"}))))
                .Returns(new[] {new Nick {User = new User()} }.AsQueryable());

            _target.TestMessage("blah blah blah", "foo");
        }

        [Fact]
        public void TestMessageFromNickWithNoUserIsLogged()
        {
            _persistenceService.Setup(
                    x => x.Where(It.Is<Expression<Func<Nick, bool>>>(fn => fn.Compile()(new Nick {Nickname = "foo"}))))
                .Returns(new[] {new Nick ()}.AsQueryable());

            _target.TestMessage("blah blah blah", "foo");
        }

        [Fact]
        public void TestMessageFromUnkownNickIsLogged()
        {
            _persistenceService.Setup(
                    x => x.Where(It.Is<Expression<Func<Nick, bool>>>(fn => fn.Compile()(new Nick {Nickname = "foo"}))))
                .Returns(new Nick[] {}.AsQueryable());

            _target.TestMessage("blah blah blah", "foo");
        }

        [Fact]
        public void TestCommandRunsCommand()
        {
            _commandHandlerFactory.Setup(
                x => x.BuildHandler(It.IsAny<CommandEventArgs>()).Handle(It.IsAny<CommandEventArgs>()));
            _persistenceService.Setup(
                    x => x.Where(It.Is<Expression<Func<Nick, bool>>>(fn => fn.Compile()(new Nick {Nickname = "foo"}))))
                .Returns(new[] {new Nick {User = new User()} }.AsQueryable());

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
            _persistenceService.Setup(
                    x => x.Where(It.Is<Expression<Func<Nick, bool>>>(fn => fn.Compile()(new Nick {Nickname = "foo"}))))
                .Returns(new[] {new Nick()}.AsQueryable());

            _target.TestMessage("bot blah blah blah", "foo");

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
                    x => x.Where(It.Is<Expression<Func<Nick, bool>>>(fn => fn.Compile()(new Nick {Nickname = "foo"}))))
                .Returns(new Nick[] {}.AsQueryable());

            _target.TestMessage("bot blah blah blah", "foo");

            _commandHandlerFactory.Verify(
                x =>
                    x.BuildHandler(It.Is<CommandEventArgs>(a => a.Command == "blah blah blah"))
                        .Handle(It.Is<CommandEventArgs>(a => a.Command == "blah blah blah")), Times.Never);
            
        }
    }
}
