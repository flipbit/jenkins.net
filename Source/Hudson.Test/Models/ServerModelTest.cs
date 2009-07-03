using Hudson.Domain;
using Hudson.Factories;
using NUnit.Framework;

namespace Hudson.Models
{
    [TestFixture]
    public class ServerModelTest
    {
        private ServerModel model;

        [SetUp]
        public void SetUp()
        {
            model = new ServerModel();
        }

        [Test]
        public void TestContainsWhenNotFound()
        {
            Assert.IsFalse(model.Contains("job"));
        }

        [Test]
        public void TestContainsWhenFound()
        {
            model.Jobs.Add(new JobModel { Name = "job" });

            Assert.IsTrue(model.Contains("job"));
        }

        [Test]
        public void TestUpdateWhenNoJobs()
        {
            var server = new Server();
            server.Jobs.Add(JobFactory.Create());

            var updated = model.Update(server);

            Assert.IsTrue(updated);
        }

        [Test]
        public void TestServerStatusWhenNoJobs()
        {
            Assert.AreEqual(BuildStatus.Unknown, model.BuildStatus);
        }

        [Test]
        public void TestServerStatusWhenOneJob()
        {
            model.Jobs.Add(new JobModel { BuildStatus = BuildStatus.Failed });

            Assert.AreEqual(BuildStatus.Failed, model.BuildStatus);
        }

        [Test]
        public void TestServerStatusWhenFailedJob()
        {
            model.Jobs.Add(new JobModel { BuildStatus = BuildStatus.Passed });
            model.Jobs.Add(new JobModel { BuildStatus = BuildStatus.Failed });

            Assert.AreEqual(BuildStatus.Failed, model.BuildStatus);
        }

        [Test]
        public void TestServerStatusWhenBuildingJob()
        {
            model.Jobs.Add(new JobModel { BuildStatus = BuildStatus.Building });
            model.Jobs.Add(new JobModel { BuildStatus = BuildStatus.Passed });
            model.Jobs.Add(new JobModel { BuildStatus = BuildStatus.Failed });

            Assert.AreEqual(BuildStatus.Building, model.BuildStatus);
        }
    }
}
