using System;
using DammitBot.Events;
using DammitBot.Protocols.Irc;
using DammitBot.TestLibrary;
using DammitBot.Wrappers;
using Moq;
using Xunit;

namespace DammitBot.Utilities
{
    public class ProtocolServiceTest : UnitTestBase<ProtocolService>
    {
        #region Private Members

        private Mock<Irc> _irc;

        #endregion

        #region Private Methods

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Inject(out _irc);
            Inject<IInstantiationService>(_container.GetInstance<InstantiationService>());
            Inject<IAssemblyService>(_container.GetInstance<AssemblyService>());
        }

        #endregion

        #region Exposed Methods

        [Fact]
        public void TestAttachingToChannelMessageReceivedAttachesToEachProtocol()
        {
            bool called = false;
            var args = new MessageEventArgs();
            EventHandler<MessageEventArgs> handler = (s, a) => {
                called = true;
                Assert.Same(args, a);
            };

            _target.Initialize();
            _target.ChannelMessageReceived += handler;
            _irc.Raise(x => x.ChannelMessageReceived += null, null, args);

            Assert.True(called);
        }

        [Fact]
        public void TestDetachingFromChannelMessageReceivedDetachesFromEachProtocol()
        {
            bool called = false;
            var args = new MessageEventArgs();
            EventHandler<MessageEventArgs> handler = (s, a) => {
                called = true;
            };

            _target.Initialize();
            _target.ChannelMessageReceived += handler;
            _target.ChannelMessageReceived -= handler;
            _irc.Raise(x => x.ChannelMessageReceived += null, null, args);

            Assert.False(called);
        }

        [Fact]
        public void TestCleanupCleansUpAllProtocols()
        {
            _target.Initialize();
            _target.Cleanup();

            _irc.Verify(x => x.Cleanup());
        }

        [Fact]
        public void TestSayToAllSaysToAllProtocols()
        {
            _target.Initialize();
            _target.SayToAll("foo");

            _irc.Verify(x => x.SayToAll("foo"));
        }

        #endregion
    }
}