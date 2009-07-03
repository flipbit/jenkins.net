using Hudson.Domain;
using Hudson.Factories;
using NUnit.Framework;

namespace Hudson.Mappers
{
    [TestFixture]
    public class JobMapperTest
    {
        private JobMapper mapper;

        [SetUp]
        public void SetUp()
        {
            mapper = new JobMapper();
        }

        [Test]
        public void TestMapJob()
        {
            var xml = XmlFactory.Load("JobResponse.Xml");

            var job = mapper.Map(xml.Contents);

            Assert.IsNotNull(job);

            Assert.AreEqual("first-job", job.Name);
            Assert.AreEqual("first job", job.DisplayName);
            Assert.AreEqual("first job description", job.Description);
            Assert.AreEqual("http://www.example.com/job/first-job/", job.Url.ToString());
            Assert.IsTrue(job.Buildable);
            Assert.AreEqual(BuildStatus.Failed, job.BuildStatus);
            Assert.AreEqual("Build stability: No recent builds failed.", job.HealthReport);
            Assert.AreEqual("health-80plus.gif", job.IconUrl);
            Assert.AreEqual(100, job.Score);
            Assert.IsTrue(job.InQueue);
            Assert.IsTrue(job.KeepDependencies);
            Assert.AreEqual(93, job.NextBuildNumber);
        }
    }
}
