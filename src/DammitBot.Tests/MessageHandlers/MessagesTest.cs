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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using DammitBot.Protocols.Irc.Configuration;
using DammitBot.Protocols.Irc.Wrappers;

namespace DammitBot.MessageHandlers
{
    [TestClass]
    public class MessagesTest : UnitTestBase<MessagesTest.MessageTester>
    {
        #region Private Members

        private Mock<IIrcClientFactory> _ircClientFactory;
        private Mock<IIrcClient> _irc;
        private Mock<ICommandHandlerFactory> _commandHandlerFactory;
        private Mock<IPersistenceService> _persistenceService;

        #endregion

        #region Setup/Teardown

        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
            _target.Dispose();
        }

        #endregion

        #region Nested Type: MessageTester

        public class MessageTester : IDisposable
        {
            #region Private Members

            private readonly IInstantiationService _instantiationService;
            private readonly Mock<IIrcClient> _irc;

            #endregion

            #region Constructors

            public MessageTester(Mock<IIrcClient> client, IInstantiationService instantiationService)
            {
                _instantiationService = instantiationService;
                var bot = instantiationService.GetInstance<IBot>();
                bot.Die();
                bot.Run();
                _irc = client;
            }

            #endregion

            #region Exposed Methods

            public void TestMessage(string message, string nick)
            {
                var args = new Mock<MessageEventArgs>();
                args.SetupGet(x => x.Message).Returns(message);
                args.SetupGet(x => x.User).Returns(nick);
                _irc.Raise(x => x.ChannelMessageRecieved += null, null, args.Object);
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

            Inject(out _ircClientFactory);
            Inject(out _irc);
            _container.Inject(_irc);
            _ircClientFactory.Setup(x => x.Build(It.IsAny<IrcConfigurationSection>())).Returns(_irc.Object);
            Inject(out _commandHandlerFactory);
            Inject(new Mock<ISchedulerService>().Object);
            Inject(new Mock<ITeamCityHelper>().Object);
            Inject(out _persistenceService);
        }

        #endregion

        [TestMethod]
        public void TestAnyMessageIsLogged()
        {
            _persistenceService.Setup(
                    x => x.Where(It.Is<Expression<Func<Nick, bool>>>(fn => fn.Compile()(new Nick {Nickname = "foo"}))))
                .Returns(new[] {new Nick {User = new User()} }.AsQueryable());

            _target.TestMessage("blah blah blah", "foo");
        }

        [TestMethod]
        public void TestMessageFromNickWithNoUserIsLogged()
        {
            _persistenceService.Setup(
                    x => x.Where(It.Is<Expression<Func<Nick, bool>>>(fn => fn.Compile()(new Nick {Nickname = "foo"}))))
                .Returns(new[] {new Nick ()}.AsQueryable());

            _target.TestMessage("blah blah blah", "foo");
        }

        [TestMethod]
        public void TestMessageFromUnkownNickIsLogged()
        {
            _persistenceService.Setup(
                    x => x.Where(It.Is<Expression<Func<Nick, bool>>>(fn => fn.Compile()(new Nick {Nickname = "foo"}))))
                .Returns(new Nick[] {}.AsQueryable());

            _target.TestMessage("blah blah blah", "foo");
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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
