using System.Collections.Generic;
using Hudson.Domain;
using NUnit.Framework;

namespace Hudson.Models.Selectors
{
    [TestFixture]
    public class BuildStatusSelectorTest
    {
        private BuildStatusSelector selector;

        [SetUp]
        public void SetUp()
        {
            selector = new BuildStatusSelector();
        }

        [Test]
        public void TestReturnsUnknownWhenEmptyJobCollection()
        {
            var result = selector.Select(new List<JobModel>());

            Assert.AreEqual(BuildStatus.Unknown, result);
        }

        [Test]
        public void TestReturnsStatusForASingleJob()
        {
            var jobs = new List<JobModel> { new JobModel { BuildStatus = BuildStatus.Passed } };

            var result = selector.Select(jobs);

            Assert.AreEqual(BuildStatus.Passed, result);
        }

        [Test]
        public void TestFailedJobOverridesPassedJob()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Failed },
                new JobModel { BuildStatus = BuildStatus.Passed }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual(BuildStatus.Failed, result);
        }

        [Test]
        public void TestBuildingJobOverridesPassedAndFailedJobs()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Building },
                new JobModel { BuildStatus = BuildStatus.Failed },
                new JobModel { BuildStatus = BuildStatus.Passed }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual(BuildStatus.Building, result);
        }

        [Test]
        public void TestPassedJobOverridesUnknownJobs()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Unknown },
                new JobModel { BuildStatus = BuildStatus.Passed },
                new JobModel { BuildStatus = BuildStatus.Unknown }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual(BuildStatus.Passed, result);
        }
    }
}