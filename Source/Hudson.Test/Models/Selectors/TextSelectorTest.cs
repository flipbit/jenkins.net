using System.Collections.Generic;
using Hudson.Domain;
using NUnit.Framework;

namespace Hudson.Models.Selectors
{
    [TestFixture]
    public class TextSelectorTest
    {
        private TextSelector selector;

        [SetUp]
        public void SetUp()
        {
            selector = new TextSelector();
        }

        [Test]
        public void TestTitleWhenEmptyJobCollection()
        {
            var result = selector.Select(new List<JobModel>());

            Assert.AreEqual("There are no jobs defined on the hudson server.", result);
        }

        [Test]
        public void TestTitleForASingleJob()
        {
            var jobs = new List<JobModel> { new JobModel { BuildStatus = BuildStatus.Passed, Comment = "Job 1 Built" } };

            var result = selector.Select(jobs);

            Assert.AreEqual("Job 1 Built", result);
        }

        [Test]
        public void TestFailedJobOverridesPassedJob()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Failed, Comment = "Job 1 Comment" },
                new JobModel { BuildStatus = BuildStatus.Passed, Comment = "Job 2 Comment" }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual("Job 1 Comment", result);
        }

        [Test]
        public void TestBuildingJobOverridesPassedAndFailedJobs()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Building, Comment = "Job 1 Comment" },
                new JobModel { BuildStatus = BuildStatus.Failed, Comment = "Job 2 Comment" },
                new JobModel { BuildStatus = BuildStatus.Passed, Comment = "Job 3 Comment" }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual("Job 1 Comment", result);
        }

        [Test]
        public void TestPassedJobOverridesPassedAndFailedAndUnknownJobs()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Unknown, Comment = "Job 1 Comment" },
                new JobModel { BuildStatus = BuildStatus.Passed, Comment = "Job 2 Comment" },
                new JobModel { BuildStatus = BuildStatus.Unknown, Comment = "Job 3 Comment" }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual("Job 2 Comment", result);
        }
    }
}