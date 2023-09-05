using System;
using DammitBot.CommandHandlers;
using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using DammitBot.Events;
using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using Lamar;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DammitBot.Tests.MessageHandlers;

public class CommandsTest : InMemoryDatabaseUnitTestBase<CommandsTest.MessageTester>
{
    #region Private Members

    private Mock<ICommandHandlerFactory> _commandHandlerFactory;
    private Nick _nickWithUser, _nickWithoutUser, _nickFromOtherProtocol;

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
        
        new CommandsPluginContainerConfiguration().Configure(serviceRegistry);
        serviceRegistry.For<IBot>().Use<Bot>().Singleton();

        serviceRegistry.For<IUnitOfWork>().Use<TestDapperUnitOfWork>();

        _commandHandlerFactory = serviceRegistry.For<ICommandHandlerFactory>().Mock();
        serviceRegistry.For<IProtocolService>().Mock();
        serviceRegistry.For<ISchedulerService>().Mock();
        serviceRegistry.For(typeof(ILogger<>)).Use(typeof(MockLogger<>));
    }

    #endregion

    #region Constructors
    
    public CommandsTest()
    {
        WithUnitOfWork(uow => {
            var userId = Convert.ToInt32(uow.Insert(new UserFaker().Generate()));

            var nickFaker = new NickFaker();
            _nickWithUser = nickFaker.Generate();
            _nickWithUser.UserId = userId;
            _nickWithUser.Protocol = "Irc";
            uow.Insert(_nickWithUser);

            _nickWithoutUser = nickFaker.Generate();
            _nickWithoutUser.Id = Convert.ToInt32(uow.Insert(_nickWithoutUser));

            _nickFromOtherProtocol = nickFaker.Generate();
            _nickFromOtherProtocol.Nickname = _nickWithUser.Nickname;
            _nickFromOtherProtocol.Protocol = "Slack";
            _nickFromOtherProtocol.Id = Convert.ToInt32(uow.Insert(_nickFromOtherProtocol));

            uow.Commit();
        });
    }
    
    #endregion

    #region Tests

    [Fact]
    public void Test_Command_RunsCommand()
    {
        _commandHandlerFactory.Setup(
            x => x
                .BuildHandler(It.IsAny<CommandEventArgs>())
                .Handle(It.IsAny<CommandEventArgs>()));

        _target.TestMessage("bot blah blah blah", _nickWithUser);

        _commandHandlerFactory.Verify(
            x =>
                x.BuildHandler(It.Is<CommandEventArgs>(
                        a => a.Command == "blah blah blah"))
                    .Handle(It.Is<CommandEventArgs>(
                        a => a.Command == "blah blah blah")));
    }

    [Fact]
    public void Test_Command_DoesNotRunCommand_IfNickDoesNotHaveUser()
    {
        _commandHandlerFactory.Setup(
            x => x
                .BuildHandler(It.IsAny<CommandEventArgs>())
                .Handle(It.IsAny<CommandEventArgs>()));

        _target.TestMessage("bot blah blah blah", _nickWithoutUser);

        _commandHandlerFactory.Verify(
            x =>
                x.BuildHandler(It.Is<CommandEventArgs>(
                        a => a.Command == "blah blah blah"))
                    .Handle(It.Is<CommandEventArgs>(
                        a => a.Command == "blah blah blah")), Times.Never);
            
    }

    [Fact]
    public void Test_Command_DoesNotRunCommand_IfNickNotRecognized()
    {
        _commandHandlerFactory.Setup(
            x => x
                .BuildHandler(It.IsAny<CommandEventArgs>())
                .Handle(It.IsAny<CommandEventArgs>()));

        _target.TestMessage("bot blah blah blah", new NickFaker().Generate());

        _commandHandlerFactory.Verify(
            x =>
                x.BuildHandler(It.Is<CommandEventArgs>(
                        a => a.Command == "blah blah blah"))
                    .Handle(It.Is<CommandEventArgs>(
                        a => a.Command == "blah blah blah")),
            Times.Never);
            
    }

    [Fact]
    public void Test_Command_DoesNotRunCommand_IfNickIsKnownButForDifferentProtocol()
    {
        _commandHandlerFactory.Setup(
            x => x
                .BuildHandler(It.IsAny<CommandEventArgs>())
                .Handle(It.IsAny<CommandEventArgs>()));

        _target.TestMessage("bot blah blah blah", _nickFromOtherProtocol);

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

    public class MessageTester
    {
        #region Private Members

        private readonly Mock<IProtocolService> _protocolService;

        #endregion

        #region Constructors

        public MessageTester(
            IInstantiationService instantiationService,
            IProtocolService protocolService)
        {
            _protocolService = Mock.Get(protocolService);
            var bot = instantiationService.GetInstance<IBot>();
            bot.Die();
            bot.Run();
        }

        #endregion

        #region Exposed Methods

        public void TestMessage(
            string? message,
            Nick nick,
            string channel = "foo")
        {
            _protocolService.Raise(
                x => x.ChannelMessageReceived += null,
                null!,
                new MessageEventArgs(message!, channel, nick.Protocol, nick.Nickname));
        }

        #endregion
    }

    #endregion
}