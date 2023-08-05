using System;
using DammitBot.CommandHandlers;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.MessageHandlers;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using Lamar;
using Moq;
using Xunit;

namespace DammitBot.Tests.MessageHandlers;

public class MessagesTest : InMemoryDatabaseUnitTestBase<MessagesTest.MessageTester>
{
    #region Private Members

    private Mock<ICommandHandlerFactory> _commandHandlerFactory;
    private Mock<IProtocolService> _protocolService;

    #endregion

    #region Private Methods

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        serviceRegistry.Scan(s =>
        {
            s.AssemblyContainingType<IBot>();
            s.WithDefaultConventions();
        });
        
        serviceRegistry.For<IBot>().Use<Bot>().Singleton();
        serviceRegistry.For<IMessageHandlerAttributeService>()
                .Use<CommandAwareMessageHandlerAttributeService>();
        serviceRegistry.For<IUnitOfWork>().Use<TestDapperUnitOfWork>();

        _commandHandlerFactory = serviceRegistry.For<ICommandHandlerFactory>().Mock();
        serviceRegistry.For<ISchedulerService>().Mock();
        _protocolService = serviceRegistry.For<IProtocolService>().Mock();
        serviceRegistry.For<Mock<IProtocolService>>().Use(_protocolService);
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
        WithUnitOfWork(uow => {
            var userId = Convert.ToInt32(uow.Insert<User>(new User {Username = "foo"}));
            uow.Insert<Nick>(new Nick {Nickname = "foo", UserId = userId});
            uow.Insert<Nick>(new Nick {Nickname = "bar"});
            uow.Commit();
        });
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
        _target.TestMessage("blah blah blah", "not a known nick");
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

        _target.TestMessage("bot blah blah blah", "who is this guy");

        _commandHandlerFactory.Verify(
            x =>
                x.BuildHandler(It.Is<CommandEventArgs>(
                        a => a.Command == "blah blah blah"))
                    .Handle(It.Is<CommandEventArgs>(
                        a => a.Command == "blah blah blah")),
            Times.Never);
            
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

        public void TestMessage(string message, string nick, string? protocol = null, string? channel = null)
        {
            var args = new MessageEventArgs(message, channel  ?? "#bar", protocol ?? "foo", nick);
            _protocolService.Raise(
                x => x.ChannelMessageReceived += null,
                null,
                args);
        }

        public void Dispose()
        {
            _instantiationService.Dispose();
        }

        #endregion
    }

    #endregion
}