using System;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.Protocols.Irc.Wrappers;
using DammitBot.TestLibrary;
using DammitBot.Utilities;
using DammitBot.Wrappers;

using Moq;
using Xunit;

namespace DammitBot
{

    public class BotTest : UnitTestBase<Bot>
    {
        #region Private Members

        private Mock<BotConfigurationSection> _config;
        private Mock<IMessageHandlerFactory> _handlerFactory;
        private Mock<IPluginService> _pluginService;
        private Mock<IProtocolService> _protocolService;

        #endregion

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            _config = new Mock<BotConfigurationSection>();
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


        [Fact]
        public void TestRunThrowsExceptionIfAlreadyRunning()
        {
            SafelyRunTarget();

            Assert.Throws<InvalidOperationException>(() => _target.Run());
        }

        // TODO:
        //[Fact]
        //public void TestChannelMessageReceivedHandlesMessageWithHandlerFromHandlerFactory()
        //{
        //    var args = new Mock<MessageEventArgs>();
        //    args.Setup(a => a.Message).Returns("foo");
        //    _handlerFactory.Setup(f => f.BuildHandler(args.Object).Handle(args.Object));
        //    SafelyRunTarget();
        //    _protocolService.Raise(c => c.ChannelMessageRecieved += null, null, args.Object);

        //    _handlerFactory.Verify(f => f.BuildHandler(args.Object).Handle(args.Object));
        //}

        [Fact]
        public void TestSayToAllSaysToAll()
        {
            SafelyRunTarget();
            _target.SayToAll("foo");

            _protocolService.Verify(c => c.SayToAll("foo"));
        }

        [Fact]
        public void TestDisposingBotCleansUpPluginService()
        {
            SafelyRunTarget();

            _target.Dispose();

            _pluginService.Verify(x => x.Cleanup());
        }
    }
}