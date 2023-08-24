using System;
using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using Xunit;

namespace DammitBot.Tests.Data.Models;

public class UserTest : ModelWithRequiredFieldsTestBase<User>
{
    #region Private Methods

    protected override void RunPostCreationAssertions(User createdObject)
    {
        Assert.InRange(createdObject.Id, 1, int.MaxValue);
    }

    protected override User ConstructTarget()
    {
        return ConstructValidObject();
    }

    protected override Action<User>[] GetWaysToInvalidate()
    {
        return new Action<User>[] {
            u => u.Username = null
        };
    }

    #endregion

    #region Exposed Methods

    public static User ConstructValidObject()
    {
        return new UserFaker().Generate();
    }

    [Fact]
    public override void Test_Updating_WithMissingRequiredFields_ThrowsException()
    {
        base.Test_Updating_WithMissingRequiredFields_ThrowsException();
    }

    [Fact]
    public void Test_Create_Timestamp()
    {
        this.TestSaveWithValidFieldsSetsCreatedAt();
    }

    [Fact]
    public void Test_Update_Timestamp()
    {
        this.TestUpdateWithValidFieldsSetsUpdatedAt(u => u.Username = "new username");
    }

    #endregion
}