using System;
using DammitBot.Data.Models;
using DammitBot.TestLibrary;
using Xunit;

namespace DammitBot.Data.NHibernate
{
    public class MessageTest : ModelWithRequiredFieldsTest<Message>
    {
        #region Constants

        public struct Defaults
        {
            #region Constants

            public const string TEXT = "text";

            #endregion
        }

        #endregion

        #region Private Methods

        protected override Message GetValidObject()
        {
            return ConstructValidObject(_target.Save(NickTest.ConstructValidObject()));
        }

        protected override Action<Message>[] GetWaysToInvalidate()
        {
            return new Action<Message>[] {
                m => m.Text = null,
                m => m.From = null
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
                From = nick
            };
        }

        [Fact]
        public void TestCreateTimestamp()
        {
            this.TestSaveWithValidFieldsSetsCreatedAt();
        }

        [Fact]
        public void TestUpdateTimestamp()
        {
            this.TestUpdateWithValidFieldsSetsUpdatedAt(m => m.Text = "new text");
        }

        #endregion
    }
}