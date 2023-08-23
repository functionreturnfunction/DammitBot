using System;
using DammitBot.Data.Models;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Data.Models;

public class MessageTest : ModelWithRequiredFieldsTestBase<Message>
{
    #region Constants

    public struct Defaults
    {
        #region Constants

        public const string TEXT = "text",
            PROTOCOL = "bleh",
            CHANNEL = "#hmpf";

        #endregion
    }

    #endregion

    #region Private Methods

    protected override Message ConstructTarget()
    {
        var nick = NickTest.ConstructValidObject();
        WithUnitOfWork(uow => {
            nick.Id = Convert.ToInt32(uow.Insert<Nick>(nick));
            uow.Commit();
        });
        return ConstructValidObject(nick);
    }

    protected override Action<Message>[] GetWaysToInvalidate()
    {
        return new Action<Message>[] {
            m => m.Text = null,
            m => {
                m.From = null;
                m.FromId = null;
            }
        };
    }

    protected override void RunPostCreationAssertions(Message createdObject)
    {
        Assert.InRange(createdObject.Id, 1, int.MaxValue);
        Assert.Equal(Defaults.TEXT, createdObject.Text);
    }

    #endregion

    #region Exposed Methods

    public static Message ConstructValidObject(Nick nick)
    {
        return new Message {
            Text = Defaults.TEXT,
            Protocol = Defaults.PROTOCOL,
            Channel = Defaults.CHANNEL,
            From = nick
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
        this.TestUpdateWithValidFieldsSetsUpdatedAt(m => m.Text = "new text");
    }

    [Fact]
    public override void Test_Updating_WithMissingRequiredFields_ThrowsException()
    {
        base.Test_Updating_WithMissingRequiredFields_ThrowsException();
    }

    #endregion
}