using System;
using DammitBot.Data.Models;
using DammitBot.TestLibrary;
using Xunit;

namespace DammitBot.Data.Dapper
{
    public class NickTest : ModelWithRequiredFieldsTest<Nick>
    {
        #region Constants

        public struct Defaults
        {
            public const string NICKNAME = "nick";
        }

        #endregion

        #region Private Methods

        protected override Nick ConstructTarget()
        {
            return ConstructValidObject();
        }

        protected override Action<Nick>[] GetWaysToInvalidate()
        {
            return new Action<Nick>[] {
                n => n.Nickname = null
            };
        }

        protected override void RunPostCreationAssertions(Nick createdObject)
        {
            Assert.InRange(createdObject.Id, 1, int.MaxValue);
            Assert.Equal(Defaults.NICKNAME, createdObject.Nickname);
        }

        #endregion

        #region Exposed Methods

        public static Nick ConstructValidObject()
        {
            return new Nick {
                Nickname = Defaults.NICKNAME
            };
        }

        [Fact]
        public void TestCreateTimestamp()
        {
            // var whatevs = uow.ExecuteReader("SELECT * FROM Nicks;");
            // var sb = new StringBuilder();

            // while (whatevs.Read())
            // {
            //     sb.AppendLine("{");
            //     for (var i = 0; i < whatevs.FieldCount; ++i)
            //     {
            //         sb.AppendLine($"'{whatevs.GetName(i)}': {whatevs.GetValue(i)}");
            //     }
            //     sb.AppendLine("},");
            // }

            // var json = sb.ToString();

            this.TestSaveWithValidFieldsSetsCreatedAt();
        }

        [Fact]
        public void TestUpdateTimestamp()
        {
            this.TestUpdateWithValidFieldsSetsUpdatedAt(n => n.Nickname = "new nickname");
        }

        [Fact]
        public void TestUpdatingWithUserWorks()
        {
            var userId = -1;
            _target = CreateValidObject();
            var targetId = _target.Id;

            WithUnitOfWork(uow => {
                _target.User = UserTest.ConstructValidObject();
                _target.User.Id = Convert.ToInt32(uow.GetRepository<User>().Insert(_target.User));
                userId = _target.User.Id;
                targetId = _target.Id;

                uow.GetRepository<Nick>().Insert(_target);

                uow.Commit();
            });

            WithUnitOfWork(uow => {
                _target = uow.GetRepository<Nick>().Find(targetId);
                Assert.Equal(userId, _target.User.Id);
            });
        }

        #endregion
    }
}
