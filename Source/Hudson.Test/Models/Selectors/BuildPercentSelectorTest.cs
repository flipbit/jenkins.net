using System;
using System.Collections.Generic;
using Hudson.Domain;
using NUnit.Framework;

namespace Hudson.Models.Selectors
{
    [TestFixture]
    public class BuildPercentSelectorTest
    {
        private BuildPercentSelector selector;

        [SetUp]
        public void SetUp()
        {
            selector = new BuildPercentSelector();
        }

        [Test]
        public void TestBuildPercentSelectorWhenNoBuilds()
        {
            var result = selector.Select(new List<JobModel>());

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void TestBuildPercentSelectorWhenNoBuildsAreBuilding()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Unknown },
                new JobModel { BuildStatus = BuildStatus.Passed },
                new JobModel { BuildStatus = BuildStatus.Unknown }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void TestBuildPercentSelectorOneBuildRunning()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Building, Created = DateTime.Now.AddSeconds(-30), LastStableBuildTime = 60 }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual(50, result);
        }

        [Test]
        public void TestBuildPercentSelectorUsesLongestBuildRunning()
        {
            var jobs = new List<JobModel> 
            { 
                new JobModel { BuildStatus = BuildStatus.Building, Created = DateTime.Now.AddSeconds(-40), LastStableBuildTime = 120 },
                new JobModel { BuildStatus = BuildStatus.Failed },
                new JobModel { BuildStatus = BuildStatus.Building, Created = DateTime.Now.AddSeconds(-30), LastStableBuildTime = 60 }
            };

            var result = selector.Select(jobs);

            Assert.AreEqual(33, result);
        }
    }
}
