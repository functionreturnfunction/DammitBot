using System;
using DammitBot.CommandHandlers;
using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
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
    private User _user, _otherUser;
    private Nick _nickWithUser;

    #endregion

    public CommandsTest()
    {
        var userFaker = new UserFaker();
        var nickFaker = new NickFaker();
        
        WithUnitOfWork(uow =>
        {
            _user = userFaker.Generate();
            _user.Id = Convert.ToInt32(uow.Insert(_user));

            _otherUser = userFaker.Generate();
            _otherUser.Id = Convert.ToInt32(uow.Insert(_otherUser));

            _nickWithUser = nickFaker.Generate();
            _nickWithUser.User = _user;
            _nickWithUser.Id = Convert.ToInt32(uow.Insert(_nickWithUser));

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
        _target.TestCommand("die", _nickWithUser.Nickname);

        _bot.Verify(x => x.Die());
    }

    [Fact]
    public void Test_BotRemindMe_CausesReminderyThingsToHappen()
    {
        var beforeCount = _connection.QuerySingle<int>("select count(*) from Reminders");
        var args = _target.TestCommand("remind me to do things in 1 minute", _nickWithUser.Nickname);

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
        var args = _target.TestCommand(
            $"remind {_otherUser.Username} to do things in 1 minute",
            _nickWithUser.Nickname);

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
        _target.TestCommand("asdfasdfasdfasdf", _nickWithUser.Nickname);

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

        public MessageEventArgs TestCommand(string command, string user = "foo")
        {
            var args = new MessageEventArgs(
                "bot " + command,
                "channel",
                "protocol",
                user);
            
            _handler.Handle(args);

            return args;
        }

        #endregion
    }

    #endregion
}