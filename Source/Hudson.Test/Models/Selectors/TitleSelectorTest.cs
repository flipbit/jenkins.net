using System.Collections.Generic;
using Hudson.Domain;
using NUnit.Framework;

namespace Hudson.Models.Selectors
{
    [TestFixture]
    public class TitleSelectorTest
    {
        private TitleSelector selector;

        [SetUp]
        public void SetUp()
        {
            selector = new TitleSelector();
        }

        [Test]
        public void TestTitleWhenEmptyJobCollection()
        {
            var result = selector.Select(new List<JobModel>());

            Assert.AreEqual("No Jobs Defined", result);
        }

        [Test]
        public void TestTitleForASingleJob()
        {
            var jobs = new List<JobModel> { new JobModel { BuildStatus = BuildStatus.Passed, Name = "Job 1" } };

            var result = selector.Select(jobs);

            Assert.AreEqual("Job 1 Passed", result);
        }

        [Test]
        public void TestFailedJobOverridesPassedJob()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Failed, Name = "Job 1" },
                new JobModel { BuildStatus = BuildStatus.Passed, Name = "Job 2" }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual("Job 1 Failed", result);
        }

        [Test]
        public void TestBuildingJobOverridesPassedAndFailedJobs()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Building, Name = "Job 1" },
                new JobModel { BuildStatus = BuildStatus.Failed, Name = "Job 2" },
                new JobModel { BuildStatus = BuildStatus.Passed, Name = "Job 3" }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual("Job 1 Building", result);
        }

        [Test]
        public void TestPassedJobOverridesPassedAndFailedAndUnknownJobs()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Unknown, Name = "Job 1" },
                new JobModel { BuildStatus = BuildStatus.Passed, Name = "Job 2" },
                new JobModel { BuildStatus = BuildStatus.Unknown, Name = "Job 3" }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual("Job 2 Passed", result);
        }

        [Test]
        public void TestBuildingJobOverridesPassedAndFailedJobsOutOfOrder()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Passed, Name = "Job 1" },
                new JobModel { BuildStatus = BuildStatus.Building, Name = "Job 2" },
                new JobModel { BuildStatus = BuildStatus.Failed, Name = "Job 3" }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual("Job 2 Building", result);
        }
    }
}