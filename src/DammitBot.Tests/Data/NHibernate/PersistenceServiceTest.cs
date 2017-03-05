using System;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using DammitBot.TestLibrary;
using NHibernate;
using Xunit;

namespace DammitBot.Data.NHibernate
{
    public class PersistenceServiceTest : InMemoryDatabaseUnitTestBase<PersistenceService>
    {
        #region Private Methods

        private Tuple<Nick, Message, User> CreateValidObjects()
        {
            User user;
            Nick nick;
            Message message;
            using (_target)
            {
                user = _target.Save(new User {
                    Username = "user"
                });
                nick = _target.Save(new Nick {
                    Nickname = "nick",
                    User = user
                });
                message = _target.Save(new Message {
                    Text = "message",
                    From = nick
                });
            }
            _target = ConstructTarget();

            return new Tuple<Nick, Message, User>(nick, message, user);
        }

        #endregion

        #region Exposed Methods

        #region Querying

        [Fact]
        public void TestQuerying()
        {
            using (_target)
            {
                Assert.Empty(_target.Query<Nick>());
                Assert.Empty(_target.Query<Message>());
                Assert.Empty(_target.Query<User>());
            }
        }

        #endregion

        #region Creating

        [Fact]
        public void TestCreatingWithInvalidProperties()
        {
            Assert.Throws<PropertyValueException>(() =>
                    _target.Save(new Nick()));
            Assert.Throws<PropertyValueException>(() =>
                    _target.Save(new Message()));
            Assert.Throws<PropertyValueException>(() =>
                    _target.Save(new User()));
        }

        [Fact]
        public void TestCreatingWithValidPropertiesSetsProvidedFields()
        {
            var validObjects = CreateValidObjects();

            using (_target)
            {
                Assert.InRange(validObjects.Item3.Id, 1, int.MaxValue);
                Assert.Equal("user", validObjects.Item3.Username);

                Assert.InRange(validObjects.Item1.Id, 1, int.MaxValue);
                Assert.Equal("nick", validObjects.Item1.Nickname);
                Assert.Same(validObjects.Item3, validObjects.Item1.User);

                Assert.InRange(validObjects.Item2.Id, 1, int.MaxValue);
                Assert.Equal("message", validObjects.Item2.Text);
                Assert.Same(validObjects.Item1, validObjects.Item2.From);
            }
        }

        [Fact]
        public void TestCreatingWithValidPropertiesSetsCreeatedAtWhereApplicable()
        {
            var validObjects = CreateValidObjects();

            using (_target)
            {
                Assert.Equal(_now, validObjects.Item1.CreatedAt);
                Assert.Equal(_now, validObjects.Item2.CreatedAt);
                Assert.Equal(_now, validObjects.Item3.CreatedAt);

                Assert.Null(validObjects.Item1.UpdatedAt);
                Assert.Null(validObjects.Item2.UpdatedAt);
                Assert.Null(validObjects.Item3.UpdatedAt);
            }
        }

        [Fact]
        public void TestCreatingWithValidPropertiesActuallyCreates()
        {
            var validObjects = CreateValidObjects();

            using (_target = ConstructTarget())
            {
                Assert.Equal(_target.Find<Nick>(validObjects.Item1.Id).Id, validObjects.Item1.Id);
                Assert.Equal(_target.Find<Message>(validObjects.Item2.Id).Id, validObjects.Item2.Id);
                Assert.Equal(_target.Find<User>(validObjects.Item3.Id).Id, validObjects.Item3.Id);
            }
        }

        #endregion

        #region Updating

        [Fact]
        public void TestUpdatingWithInvalidProperties()
        {
            var validObjects = CreateValidObjects();

            validObjects.Item1.Nickname = null;
            validObjects.Item2.Text = null;
            validObjects.Item3.Username = null;

            Assert.Throws<PropertyValueException>(() => _target.Save(validObjects.Item1));
            Assert.Throws<PropertyValueException>(() => _target.Save(validObjects.Item2));
            Assert.Throws<PropertyValueException>(() => _target.Save(validObjects.Item3));
        }

        [Fact]
        public void TestUpdatingWithValidPropertiesSetsUpdatedAtWhereApplicable()
        {
            var validObjects = CreateValidObjects();

            using (_target)
            {
                validObjects.Item1.Nickname = "newNick";
                validObjects.Item2.Text = "newMessage";
                validObjects.Item3.Username = "newUser";

                _target.Save(validObjects.Item1);
                _target.Save(validObjects.Item2);
                _target.Save(validObjects.Item3);

                Assert.Equal(_now, validObjects.Item1.UpdatedAt);
                Assert.Equal(_now, validObjects.Item2.UpdatedAt);
                Assert.Equal(_now, validObjects.Item3.UpdatedAt);
            }
        }

        #endregion

        #endregion
    }
}
