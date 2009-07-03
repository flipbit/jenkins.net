using Hudson.Domain;
using Hudson.Factories;
using NUnit.Framework;

namespace Hudson.Models
{
    [TestFixture]
    public class JobModelTest
    {
        private JobModel model;

        [SetUp]
        public void SetUp()
        {
            model = new JobModel();
        }

        [Test]
        public void TestUpdateWhenModelIsEmpty()
        {
            var job = JobFactory.Create(10);
            job.BuildStatus = BuildStatus.Building;

            var changed = model.Update(job);

            Assert.IsTrue(changed);
            Assert.AreEqual(BuildStatus.Building, model.BuildStatus);
            Assert.AreEqual(10, model.Number);            
        }

        [Test]
        public void TestUpdateWhenModelNoChange()
        {
            var job = JobFactory.Create(10);
            job.BuildStatus = BuildStatus.Building;

            model.Number = 10;
            model.BuildStatus = BuildStatus.Building;

            var changed = model.Update(job);

            Assert.IsFalse(changed);
            Assert.AreEqual(BuildStatus.Building, model.BuildStatus);
            Assert.AreEqual(10, model.Number);
        }

        [Test]
        public void TestUpdateWhenOnlyBuildStatusChanged()
        {
            var job = JobFactory.Create(10);
            job.BuildStatus = BuildStatus.Failed;

            model.Number = 10;
            model.BuildStatus = BuildStatus.Passed;

            var changed = model.Update(job);

            Assert.IsTrue(changed);
            Assert.AreEqual(BuildStatus.Failed, model.BuildStatus);
            Assert.AreEqual(10, model.Number);
        }

        [Test]
        public void TestUpdateWhenModelNewJob()
        {
            var job = JobFactory.Create(11);
            job.BuildStatus = BuildStatus.Building;

            model.Number = 10;
            model.BuildStatus = BuildStatus.Passed;

            var changed = model.Update(job);

            Assert.IsTrue(changed);
            Assert.AreEqual(BuildStatus.Building, model.BuildStatus);
            Assert.AreEqual(11, model.Number);
        }
    }
}
