using Hudson.Domain;
using NUnit.Framework;

namespace Hudson.Parsers
{
    [TestFixture]
    public class BuildStatusParserTest
    {
        [Test]
        public void TestBuildingStatus()
        {
            // Building
            Assert.AreEqual(BuildStatus.Building, BuildStatusParser.Parse("blue_anime"));
            Assert.AreEqual(BuildStatus.Building, BuildStatusParser.Parse("red_anime"));

            // Passed
            Assert.AreEqual(BuildStatus.Passed, BuildStatusParser.Parse("blue"));

            // Failure
            Assert.AreEqual(BuildStatus.Failed, BuildStatusParser.Parse("red"));

            // Everything else
            Assert.AreEqual(BuildStatus.Unknown, BuildStatusParser.Parse("unknown"));
        }
    }
}
