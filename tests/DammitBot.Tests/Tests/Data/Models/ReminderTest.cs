using System;
using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Data.Models;

public class ReminderTest : ModelWithRequiredFieldsTestBase<Reminder>
{
    #region Private Methods

    protected override Reminder ConstructTarget()
    {
        var user = UserTest.ConstructValidObject();
        WithUnitOfWork(uow => {
            user.Id = Convert.ToInt32(uow.Insert<User>(user));
            uow.Commit();
        });
        return ConstructValidObject(user, user, _now);
    }

    protected override Action<Reminder>[] GetWaysToInvalidate()
    {
        return new Action<Reminder>[] {
            r => {
                r.From = null;
                r.FromId = null;
            },
            r => {
                r.To = null;
                r.ToId = null;
            }
        };
    }

    protected override void RunPostCreationAssertions(Reminder createdObject)
    {
        Assert.InRange(createdObject.Id, 1, int.MaxValue);
        Assert.Equal(_now, createdObject.RemindAt);
        Assert.Null(createdObject.RemindedAt);
    }

    #endregion

    #region Exposed Methods

    public static Reminder ConstructValidObject(User from, User to, DateTime remindAt)
    {
        var reminder = new ReminderFaker().Generate();
        reminder.From = from;
        reminder.To = to;
        reminder.RemindAt = remindAt;
        return reminder;
    }

    [Fact]
    public void Test_Create_Timestamp()
    {
        this.TestSaveWithValidFieldsSetsCreatedAt();
    }

    [Fact]
    public void Test_Update_Timestamp()
    {
        this.TestUpdateWithValidFieldsSetsUpdatedAt(u => u.Text = "new reminder");
    }

    [Fact]
    public override void Test_Updating_WithMissingRequiredFields_ThrowsException()
    {
        base.Test_Updating_WithMissingRequiredFields_ThrowsException();
    }

    #endregion
}