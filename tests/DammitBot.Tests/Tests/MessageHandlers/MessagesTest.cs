using System;
using DammitBot.CommandHandlers;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.MessageHandlers;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using Lamar;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DammitBot.Tests.MessageHandlers;

public class MessagesTest : InMemoryDatabaseUnitTestBase<MessagesTest.MessageTester>
{
    #region Private Members

    private Mock<ICommandHandlerFactory> _commandHandlerFactory;
    private Mock<IProtocolService> _protocolService;
    private Mock<IMessageHandlerFactory> _messageHandlerFactory;

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

        _messageHandlerFactory = serviceRegistry.For<IMessageHandlerFactory>().Mock();
        _commandHandlerFactory = serviceRegistry.For<ICommandHandlerFactory>().Mock();
        _protocolService = serviceRegistry.For<IProtocolService>().Mock();
        serviceRegistry.For<ISchedulerService>().Mock();
        serviceRegistry.For(typeof(ILogger<>)).Use(typeof(MockLogger<>));
    }

    #endregion

    #region Constructors
    
    public MessagesTest()
    {
        WithUnitOfWork(uow => {
            var userId = Convert.ToInt32(uow.Insert(new User {Username = "foo"}));
            uow.Insert(new Nick {Protocol = "#bar", Nickname = "foo", UserId = userId});
            uow.Insert(new Nick {Protocol = "#bar", Nickname = "bar"});
            uow.Commit();
        });
    }
    
    #endregion

    #region Tests

    [Fact]
    public void Test_AnyMessage_IsLogged()
    {
        _target.TestMessage("blah blah blah", "foo");
    }

    [Fact]
    public void Test_MessageFromNickWithNoUser_IsLogged()
    {
        _target.TestMessage("blah blah blah", "bar");
    }

    [Fact]
    public void Test_MessageFromUnknownNick_IsLogged()
    {
        _target.TestMessage("blah blah blah", "not a known nick");
    }

    #endregion

    #region Nested Type: MessageTester

    public class MessageTester
    {
        #region Private Members

        private readonly Mock<IProtocolService> _protocolService;
        private readonly Mock<IMessageHandlerFactory> _messageHandlerFactory;

        #endregion

        #region Constructors

        public MessageTester(
            IInstantiationService instantiationService,
            IMessageHandlerFactory messageHandlerFactory,
            IProtocolService protocolService)
        {
            _messageHandlerFactory = Mock.Get(messageHandlerFactory);
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
            string channel = "foo",
            bool shouldLog = true)
        {
            Mock<IMessageHandler>? handler = null;
            if (shouldLog)
            {
                _messageHandlerFactory.Setup(x => x.BuildHandler(
                            It.Is<MessageEventArgs>(
                                args =>
                                    args.Message == message &&
                                    args.Channel == channel &&
                                    args.Protocol == protocol &&
                                    args.User == nick)))
                    .Returns((handler = new Mock<IMessageHandler>()).Object);
                handler.Setup(x => x.Handle(
                    It.Is<MessageEventArgs>(
                        args =>
                            args.Message == message &&
                            args.Channel == channel &&
                            args.Protocol == protocol &&
                            args.User == nick)));
            }
            
            _protocolService.Raise(
                x => x.ChannelMessageReceived += null,
                null!,
                new MessageEventArgs(message!, channel, protocol, nick));

            if (shouldLog)
            {
                _messageHandlerFactory.VerifyAll();
                handler!.VerifyAll();
            }
        }

        #endregion
    }

    #endregion
}