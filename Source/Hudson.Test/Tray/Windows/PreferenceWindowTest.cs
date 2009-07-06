using NUnit.Framework;

namespace Hudson.Tray.Windows
{
    [TestFixture]
    public class PreferenceWindowTest
    {
        private PreferenceWindow window;

        [SetUp]
        public void SetUp()
        {
            window = new PreferenceWindow();
        }

        [Test]
        public void Test()
        {
            var result = window.Validate();

            Assert.IsFalse(result);
        }
    }
}
