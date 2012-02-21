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

            Assert.AreEqual("Started by an SCM change", build.Description);
            Assert.AreEqual(true, build.Building);
            Assert.AreEqual(811570, build.Duration);
            Assert.AreEqual("first-job #92", build.FullDisplayName);
            Assert.AreEqual(false, build.KeepLog);
            Assert.AreEqual("Fixed stuff.", build.Comments);
            Assert.AreEqual(1124, build.Number);
            Assert.AreEqual("fa8825becade7adf64212e2395460d290e201d08", build.Revision);
            Assert.AreEqual(true, build.Success);
            Assert.AreEqual("http://www.example.com/job/first-job/92/", build.Url.ToString());
            Assert.AreEqual(DateTime.Parse("2012-02-20 17:09:39"), build.Created);
            Assert.AreEqual("test.user", build.User);
        }
    }
}
