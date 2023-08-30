using System;
using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Data.Models;

public class NickTest : ModelWithRequiredFieldsTestBase<Nick>
{
    #region Private Methods

    protected override Nick CreateTarget()
    {
        return ConstructValidObject();
    }

    protected override Action<Nick>[] GetWaysToInvalidate()
    {
        return new Action<Nick>[] {
            n => n.Nickname = null,
            n => n.Protocol = null
        };
    }

    protected override void RunPostCreationAssertions(Nick createdObject)
    {
        Assert.InRange(createdObject.Id, 1, int.MaxValue);
    }

    #endregion

    #region Exposed Methods

    public static Nick ConstructValidObject()
    {
        return new NickFaker().Generate();
    }

    [Fact]
    public void Test_Create_Timestamp()
    {
        this.TestSaveWithValidFieldsSetsCreatedAt();
    }

    [Fact]
    public void Test_Update_Timestamp()
    {
        this.TestUpdateWithValidFieldsSetsUpdatedAt(n => n.Nickname = "new nickname");
    }

    [Fact]
    public void Test_Updating_WithUser_Works()
    {
        var userId = -1;
        _target = CreateValidObject();
        var targetId = _target.Id;

        WithUnitOfWork(uow => {
            _target.User = UserTest.ConstructValidObject();
            _target.User.Id = Convert.ToInt32(uow.Insert<User>(_target.User));
            userId = _target.User.Id;
            targetId = _target.Id;

            uow.Update<Nick>(_target);

            _target = uow.Find<Nick>(targetId);
            Assert.Equal(userId, _target.User.Id);
        });
    }

    #endregion
}