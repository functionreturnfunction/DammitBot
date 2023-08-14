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
using Moq;
using Xunit;

namespace DammitBot.Tests.MessageHandlers;

public class CommandsTest : InMemoryDatabaseUnitTestBase<CommandsTest.MessageTester>
{
    #region Private Members

    private Mock<ICommandHandlerFactory> _commandHandlerFactory;
    private Mock<IProtocolService> _protocolService;
    private Nick _nickWithUser, _nickWithoutUser;

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
        _protocolService = serviceRegistry.For<IProtocolService>().Mock();
        serviceRegistry.For<ISchedulerService>().Mock();
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
            uow.Insert(_nickWithUser);

            _nickWithoutUser = nickFaker.Generate();
            _nickWithoutUser.Id = Convert.ToInt32(uow.Insert(_nickWithoutUser));

            uow.Commit();
        });
    }
    
    #endregion

    #region Tests

    [Fact]
    public void Test_Command_RunsCommand()
    {
        _commandHandlerFactory.Setup(
            x => x.BuildHandler(It.IsAny<CommandEventArgs>()).Handle(It.IsAny<CommandEventArgs>()));

        _target.TestMessage("bot blah blah blah", _nickWithUser.Nickname);

        _commandHandlerFactory.Verify(
            x =>
                x.BuildHandler(It.Is<CommandEventArgs>(a => a.Command == "blah blah blah"))
                    .Handle(It.Is<CommandEventArgs>(a => a.Command == "blah blah blah")));
    }

    [Fact]
    public void Test_Command_DoesNotRunCommand_IfNickDoesNotHaveUser()
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
    public void Test_Command_DoesNotRunCommand_IfNickNotRecognized()
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
            string nick,
            string protocol = "#bar",
            string channel = "foo")
        {
            _protocolService.Raise(
                x => x.ChannelMessageReceived += null,
                null!,
                new MessageEventArgs(message!, channel, protocol, nick));
        }

        #endregion
    }

    #endregion
}