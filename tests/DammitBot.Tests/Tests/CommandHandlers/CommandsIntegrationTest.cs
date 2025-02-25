﻿using System;
using DammitBot.CommandHandlers;
using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using DammitBot.Events;
using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.MessageHandlers;
using Dapper;
using Lamar;
using Moq;
using Xunit;

namespace DammitBot.Tests.CommandHandlers;

public class CommandsIntegrationTest : InMemoryDatabaseUnitTestBase<CommandsIntegrationTest.CommandTester>
{
    #region Private Members

    private Mock<IBot> _bot;
    private User _user, _otherUser, _adminUser;
    private Nick _nickWithUser, _nickWithAdminUser;

    #endregion

    #region Setup/Teardown
    
    public CommandsIntegrationTest()
    {
        var userFaker = new UserFaker();
        var nickFaker = new NickFaker();
        
        WithUnitOfWork(uow =>
        {
            _user = userFaker.Generate();
            _user.Id = Convert.ToInt32(uow.Insert(_user));

            _otherUser = userFaker.Generate();
            _otherUser.Id = Convert.ToInt32(uow.Insert(_otherUser));

            _adminUser = userFaker.Generate();
            _adminUser.IsAdmin = true;
            _adminUser.Id = Convert.ToInt32(uow.Insert(_adminUser));

            _nickWithUser = nickFaker.Generate();
            _nickWithUser.User = _user;
            _nickWithUser.Id = Convert.ToInt32(uow.Insert(_nickWithUser));

            _nickWithAdminUser = nickFaker.Generate();
            _nickWithAdminUser.User = _adminUser;
            _nickWithAdminUser.Id = Convert.ToInt32(uow.Insert(_nickWithAdminUser));
            
            uow.Commit();
        });
        
        _dateTimeProvider.SetCurrentTime(_now = _now.Date);
    }

    #endregion
    
    #region Private Methods

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);
        
        new CommandsPluginContainerConfiguration().Configure(serviceRegistry);
        new HelpCommandContainerConfiguration().Configure(serviceRegistry);

        _bot = serviceRegistry.For<IBot>().Mock();
    }

    #endregion

    #region bot die

    [Fact]
    public void Test_BotDie_CausesBotToDie_WhenUserIsAdmin()
    {
        _target.TestCommand("die", _nickWithAdminUser);

        _bot.Verify(x => x.Die());
    }

    [Fact]
    public void Test_BotDie_DoesNotCauseBotToDie_WhenUserIsNotAdmin()
    {
        _target.TestCommand("die", _nickWithUser);

        _bot.Verify(x => x.Die(), Times.Never);
    }
    
    #endregion
    
    #region bot help

    [Fact]
    public void Test_BotHelp_ListsCommandsAndDescriptions()
    {
        _target.TestCommand("help", _nickWithUser);

        var expected = new[] {
            "This bot responds to the following commands:",
            "	 (?:dammit )?bot help( .+)? - Get usage information for the available bot commands.",
            "	 (?:dammit )?bot remind ([\\s]+).+ - Set reminders; messages which the bot will send to a user or channel at a predefined point in the future."
        };

        _bot.Verify(x =>
            x.ReplyToMessage(
                It.IsAny<CommandEventArgs>(),
                string.Join(Environment.NewLine, expected) + Environment.NewLine));
    }

    [Fact]
    public void Test_BotHelp_ListsAdminCommandsAndDescriptions_WhenUserIsAdmin()
    {
        _target.TestCommand("help", _nickWithAdminUser);

        var expected = new[] {
            "This bot responds to the following commands:",
            "	 (?:dammit )?bot die - Disconnect from any connected protocols, stop any running services, and shut down the bot. (admin only)",
            "	 (?:dammit )?bot help( .+)? - Get usage information for the available bot commands.",
            "	 (?:dammit )?bot remind ([\\s]+).+ - Set reminders; messages which the bot will send to a user or channel at a predefined point in the future."
        };

        _bot.Verify(x =>
            x.ReplyToMessage(
                It.IsAny<CommandEventArgs>(),
                string.Join(Environment.NewLine, expected) + Environment.NewLine));
    }
    
    #endregion
    
    #region bot remind

    [Fact]
    public void Test_BotRemindMe_CausesReminderyThingsToHappen()
    {
        var expectedResponse = $"Reminder set for {_now.AddMinutes(1)}";
        var beforeCount = _connection.QuerySingle<int>("select count(*) from Reminders");

        var args = _target.TestCommand("remind me to do things in 1 minute", _nickWithUser);
        
        _bot.Verify(x =>
            x.ReplyToMessage(
                It.IsAny<MessageEventArgs>(),
                expectedResponse));

        var afterCount = _connection.QuerySingle<int>("select count(*) from Reminders");

        Assert.True(afterCount == beforeCount + 1);
    }

    [Fact]
    public void Test_BotRemindOtherUser_AlsoCausesReminderyThingsToHappen()
    {
        var beforeCount = _connection.QuerySingle<int>("select count(*) from Reminders");

        var args = _target.TestCommand(
            $"remind {_otherUser.Username} to do things in 1 minute",
            _nickWithUser);
        
        _bot.Verify(x =>
            x.ReplyToMessage(
                It.IsAny<CommandEventArgs>(),
                $"Reminder set for {_now.AddMinutes(1)}"));

        var afterCount = _connection.QuerySingle<int>("select count(*) from Reminders");

        Assert.True(afterCount == beforeCount + 1);
    }

    [Fact]
    public void Test_BotRemind_DoesNotRemind_WhenTimeStringCannotBeParsed()
    {
        var beforeCount = _connection.QuerySingle<int>("select count(*) from Reminders");
        var timeString = "in 7 parsecs";
        var args = _target.TestCommand(
            $"remind me to do things {timeString}",
            _nickWithUser);

        _bot.Verify(x =>
            x.ReplyToMessage(
                It.IsAny<MessageEventArgs>(),
                $"Cannot parse time string '{timeString}'"));

        var afterCount = _connection.QuerySingle<int>("select count(*) from Reminders");

        Assert.True(afterCount == beforeCount);
    }
    
    #endregion
    
    #region bot asdfasdfasdfasdf (unknown command)

    [Fact]
    public void Test_GetMatchingHandlers_ReturnsOnlyUnknownCommandHandler_ForUnknownCommand()
    {
        _target.TestCommand("asdfasdfasdfasdf", _nickWithUser);

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

        public MessageEventArgs TestCommand(string command, Nick nick)
        {
            var args = new MessageEventArgs(
                "bot " + command,
                "channel",
                nick.Protocol,
                nick.Nickname);
            
            _handler.Handle(args);

            return args;
        }

        #endregion
    }

    #endregion
}