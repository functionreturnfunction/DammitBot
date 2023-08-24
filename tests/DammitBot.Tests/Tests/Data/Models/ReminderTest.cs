using System;
using DammitBot.Data.Models;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Data.Models;

public class ReminderTest : ModelWithRequiredFieldsTestBase<Reminder>
{
    #region Constants

    public struct Defaults
    {
        #region Constants

        public const string TEXT = "reminder";

        #endregion
    }

    #endregion

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
        return new Reminder {
            Text = Defaults.TEXT,
            From = from,
            To = to,
            RemindAt = remindAt
        };
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