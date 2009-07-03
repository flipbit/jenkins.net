using System;
using Hudson.Factories;
using NUnit.Framework;

namespace Hudson.Mappers
{
    [TestFixture]
    public class BuildMapperTest
    {
        private BuildMapper mapper;

        [SetUp]
        public void SetUp()
        {
            mapper = new BuildMapper();
        }

        [Test]
        public void TestMapBuild()
        {
            var xml = XmlFactory.Load("BuildResponse.xml");

            var build = mapper.Map(xml.Contents);

            Assert.IsNotNull(build);

            Assert.AreEqual("Started by user", build.Description);
            Assert.AreEqual(true, build.Building);
            Assert.AreEqual(28204, build.Duration);
            Assert.AreEqual("first-job #92", build.FullDisplayName);
            Assert.AreEqual(false, build.KeepLog);
            Assert.AreEqual("Changed account.", build.Comments);
            Assert.AreEqual(92, build.Number);
            Assert.AreEqual(119, build.Revision);
            Assert.AreEqual(true, build.Success);
            Assert.AreEqual("http://www.example.com/job/first-job/92/", build.Url.ToString());
            Assert.AreEqual(DateTime.Parse("2009-06-21 18:49:37"), build.Created);
        }
    }
}
