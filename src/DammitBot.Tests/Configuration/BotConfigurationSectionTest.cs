using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DammitBot.Configuration
{
    [TestClass]
    public class BotConfigurationSectionTest
    {
        #region Private Members

        private BotConfigurationSection _target;

        #endregion

        #region Setup/Teardown

        [TestInitialize]
        public void TestInitialize()
        {
            _target = (BotConfigurationSection)System.Configuration.ConfigurationManager.GetSection(BotConfigurationSection.SECTION_NAME);
        }

        #endregion

        [TestMethod]
        public void TestValuesAreSetProperlyByConfigFile()
        {
            Assert.AreEqual(_target.Channel, "channel");
            Assert.AreEqual(_target.Nick, "nick");
            Assert.AreEqual(_target.Server, "server");
            Assert.AreEqual(_target.User, "user");
        }
    }
}
