using DammitBot.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DammitBot.MessageHandlers
{
    [TestClass]
    public class LogMessageHandlerTest : UnitTestBase<LogMessageHandler>
    {
        [TestMethod]
        public void TestHandleLogsMessageIfUserRecognized()
        {
            Assert.Inconclusive("test/functionality not yet written");
        }

        [TestMethod]
        public void TestHandleDoesNotLogMessageIfUserNotRecognized()
        {
            Assert.Inconclusive("test/functionality not yet written");
        }
    }
}
