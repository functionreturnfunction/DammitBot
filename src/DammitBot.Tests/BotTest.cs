using System;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.TestLibrary;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DammitBot
{
    [TestClass]
    public class BotTest : UnitTestBase<Bot>
    {
        #region Private Members

        private Mock<BotConfigurationSection> _config;
        private Mock<IMessageHandlerFactory> _handlerFactory;
        private Mock<IIrcClientFactory> _clientFactory;
        private Mock<IIrcClient> _irc;
        private Mock<IPluginService> _pluginService;

        #endregion

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            _config = new Mock<BotConfigurationSection>();
            Mock<IConfigurationManager> manager;
            Inject(out manager);
            manager.SetupGet(x => x.BotConfig).Returns(_config.Object);
            _config.SetupGet(x => x.Channel).Returns("#foo");
            Inject(out _clientFactory);
            Inject(out _handlerFactory);
            Inject(out _pluginService);
            _clientFactory.Setup(f => f.Build(_config.Object)).Returns((_irc = new Mock<IIrcClient>()).Object);
        }

        private void SafelyRunTarget()
        {
            // die ensures the Loop doesn't run
            _target.Die();
            _target.Run();
        }

        [TestMethod]
        public void TestRunGetsClientFromFactory()
        {
            SafelyRunTarget();

            _clientFactory.Verify(f => f.Build(_config.Object));
        }

        [TestMethod]
        public void TestRunThrowsExceptionIfAlreadyRunning()
        {
            SafelyRunTarget();

            MyAssert.Throws<InvalidOperationException>(() => _target.Run());
        }

        [TestMethod]
        public void TestConnectionCompleteJoinsTheConfiguredChannel()
        {
            var args = new Mock<EventArgs>();
            SafelyRunTarget();
            _irc.Raise(c => c.ConnectionComplete += null, null, args.Object);

            _irc.Verify(i => i.JoinChannel("#foo"));
        }

        [TestMethod]
        public void TestChannelMessageReceivedHandlesMessageWithHandlerFromHandlerFactory()
        {
            var args = new Mock<MessageEventArgs>();
            args.Setup(a => a.IrcMessage.RawMessage).Returns("foo");
            _handlerFactory.Setup(f => f.BuildHandler(args.Object).Handle(args.Object));
            SafelyRunTarget();
            _irc.Raise(c => c.ChannelMessageRecieved += null, null, args.Object);

            _handlerFactory.Verify(f => f.BuildHandler(args.Object).Handle(args.Object));
        }

        [TestMethod]
        public void TestRunConnectsClient()
        {
            SafelyRunTarget();

            _irc.Verify(c => c.ConnectAsync());
        }

        [TestMethod]
        public void TestSayInChannelSendsMessageToConfiguredChannel()
        {
            SafelyRunTarget();
            _target.SayInChannel("foo");

            _irc.Verify(c => c.SendMessage("foo", "#foo"));
        }

        [TestMethod]
        public void TestDisposingBotCleansUpPluginService()
        {
            SafelyRunTarget();

            _target.Dispose();

            _pluginService.Verify(x => x.Cleanup());
        }
    }
}