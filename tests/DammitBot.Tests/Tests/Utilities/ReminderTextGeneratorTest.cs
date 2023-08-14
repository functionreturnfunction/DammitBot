using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using DammitBot.Library;
using DammitBot.Utilities;
using Xunit;

namespace DammitBot.Tests.Utilities;

public class ReminderTextGeneratorTest : UnitTestBase<ReminderTextGenerator>
{
    #region Exposed Methods

    [Fact]
    public void Test_Generate_GeneratesTextForSameUser()
    {
        var user = new UserFaker().Generate();

        Assert.Equal($"@{user.Username} you wanted me to remind you to do stuff and junk on {_now:d} at {_now:t}",
            _target.Generate(new Reminder {
                Text = "to do stuff and junk", From = user, To = user, RemindAt = _now
            }).Text);
    }

    [Fact]
    public void Test_Generate_GeneratesTextForDifferentUser()
    {
        var userFaker = new UserFaker();
        var user = userFaker.Generate();
        var differentUser = userFaker.Generate(); 

        Assert.Equal(
            $"@{differentUser.Username} {user.Username} wanted me to remind you to do " +
            $"stuff and junk on {_now:d} at {_now:t}",
            _target.Generate(new Reminder {
                Text = "to do stuff and junk", From = user, To = differentUser, RemindAt = _now
            }).Text);
    }

    #endregion
}