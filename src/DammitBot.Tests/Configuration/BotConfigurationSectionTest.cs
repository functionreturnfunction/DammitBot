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
            Assert.AreEqual(_target.GoesBy, "bot");
        }
    }
}
