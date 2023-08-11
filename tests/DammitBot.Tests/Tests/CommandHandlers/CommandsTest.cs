using System;
using DammitBot.CommandHandlers;
using DammitBot.Configuration;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.MessageHandlers;
using Dapper;
using Lamar;
using Moq;
using Xunit;

namespace DammitBot.Tests.CommandHandlers;

public class CommandsTest : InMemoryDatabaseUnitTestBase<CommandsTest.CommandTester>
{
    #region Private Members

    private Mock<IBot> _bot;

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

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        serviceRegistry.For<ICommandHandlerTypeService>()
            .Use<UnknownCommandHandlerTypeAwareCommandHandlerTypeService>();

        _bot = serviceRegistry.For<IBot>().Mock();
    }

    #endregion

    #region Exposed Methods

    [Fact]
    public void Test_BotDie_CausesBotToDie()
    {
        _target.TestCommand("die");

        _bot.Verify(x => x.Die());
    }

    [Fact]
    public void Test_BotRemindMe_CausesReminderyThingsToHappen()
    {
        var beforeCount = _connection.QuerySingle<int>("select count(*) from Reminders");
        var args = _target.TestCommand("remind me to do things in 1 minute");

        _bot.Verify(x =>
            x.ReplyToMessage(
                It.IsAny<MessageEventArgs>(),
                $"Reminder set for {_now.AddMinutes(1)}"));

        var afterCount = _connection.QuerySingle<int>("select count(*) from Reminders");

        Assert.True(afterCount == beforeCount + 1);
    }

    [Fact]
    public void Test_BotRemindOtherUser_AlsoCausesReminderyThingsToHappen()
    {
        var beforeCount = _connection.QuerySingle<int>("select count(*) from Reminders");
        var args = _target.TestCommand("remind bar to do things in 1 minute");

        _bot.Verify(x =>
            x.ReplyToMessage(
                It.IsAny<MessageEventArgs>(),
                $"Reminder set for {_now.AddMinutes(1)}"));

        var afterCount = _connection.QuerySingle<int>("select count(*) from Reminders");

        Assert.True(afterCount == beforeCount + 1);
    }

    [Fact]
    public void Test_GetMatchingHandlers_ReturnsOnlyUnknownCommandHandler_ForUnknownCommand()
    {
        _target.TestCommand("asdfasdfasdfasdf");

        _bot.Verify(x =>
            x.ReplyToMessage(
                It.IsAny<MessageEventArgs>(),
                string.Format(UnknownCommandHandler.MESSAGE, Bot.DEFAULT_GOES_BY)));
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

        public MessageEventArgs TestCommand(string command)
        {
            var args = new MessageEventArgs(
                "bot " + command,
                "channel",
                "protocol",
                "foo");
            
            _handler.Handle(args);

            return args;
        }

        #endregion
    }

    #endregion
}