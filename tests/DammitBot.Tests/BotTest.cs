using System;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.TestLibrary;
using DammitBot.Utilities;
using Moq;
using Xunit;

namespace DammitBot
{

    public class BotTest : UnitTestBase<Bot>
    {
        #region Private Members

        private Mock<IBotConfigurationSection> _config;
        private Mock<IMessageHandlerFactory> _handlerFactory;
        private Mock<IPluginService> _pluginService;
        private Mock<IProtocolService> _protocolService;

        #endregion

        #region Private Methods

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            _config = new Mock<IBotConfigurationSection>();
            Mock<IConfigurationManager> manager;
            Inject(out manager);
            manager.SetupGet(x => x.BotConfig).Returns(_config.Object);
            Inject(out _handlerFactory);
            Inject(out _pluginService);
            Inject(out _protocolService);
        }

        private void SafelyRunTarget()
        {
            // die ensures the Loop doesn't run
            _target.Die();
            _target.Run();
        }

        #endregion

        #region Exposed Methods

        [Fact]
        public void TestRunThrowsExceptionIfAlreadyRunning()
        {
            SafelyRunTarget();

            Assert.Throws<InvalidOperationException>(() => _target.Run());
        }

        [Fact]
        public void TestChannelMessageReceivedHandlesMessageWithHandlerFromHandlerFactory()
        {
            var args = new Mock<MessageEventArgs>();
            args.Setup(a => a.Message).Returns("foo");
            _handlerFactory.Setup(f => f.BuildHandler(args.Object).Handle(args.Object));
            SafelyRunTarget();
            _protocolService.Raise(c => c.ChannelMessageReceived += null, null, args.Object);

            _handlerFactory.Verify(f => f.BuildHandler(args.Object).Handle(args.Object));
        }

        [Fact]
        public void TestSayToAllSaysToAll()
        {
            SafelyRunTarget();
            _target.SayToAll("foo");

            _protocolService.Verify(c => c.SayToAll("foo"));
        }

        [Fact]
        public void TestReplyToMessageRepliesToMessage()
        {
            var args = new Mock<MessageEventArgs>();
            args.SetupGet(x => x.Protocol).Returns("foo");
            args.SetupGet(x => x.Channel).Returns("bar");
            SafelyRunTarget();

            _target.ReplyToMessage(args.Object, "baz");

            _protocolService.Verify(x => x.SayToChannel("foo", "bar", "baz"));
        }

        [Fact]
        public void TestDisposingBotCleansUpPluginService()
        {
            SafelyRunTarget();

            _target.Dispose();

            _pluginService.Verify(x => x.Cleanup());
        }

        #endregion
    }
}