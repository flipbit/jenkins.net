using System;
using System.Collections.Generic;
using Hudson.Domain;
using Hudson.Factories;
using Hudson.Interfaces.Services;
using Moq;
using NUnit.Framework;

namespace Hudson.Services
{
    [TestFixture]
    public class JobServiceTest
    {
        #region Private

        private JobService service;

        private Mock<IXmlService> xmlService;

        private Mock<IBuildService> buildService;

        private void ExpectCallsToGetJob()
        {
            xmlService
                .Expect(call => call.GetPage(It.IsAny<Uri>()))
                .Returns(XmlFactory.Load("JobResponse.Xml"));

            buildService
                .Expect(call => call.GetBuilds(It.IsAny<IList<BuildDescriptor>>()))
                .Returns(new List<Build>());

            buildService
                .Expect(call => call.GetBuild(It.IsAny<BuildDescriptor>()))
                .Returns(new Build());
        }

        #endregion

        [SetUp]
        public void SetUp()
        {
            xmlService = new Mock<IXmlService>();
            buildService = new Mock<IBuildService>();

            service = new JobService { XmlService = xmlService.Object, BuildService = buildService.Object };
        }

        [Test]
        public void TestGetJob()
        {
            ExpectCallsToGetJob();

            var descriptor = new JobDescriptor { Url = new Uri("http://www.example.com/jobs/first-job/") };

            var job = service.GetJob(descriptor);

            Assert.IsNotNull(job);
            Assert.AreEqual("first-job", job.Name);

            // Check builds have been initilized
            Assert.IsNotNull(job.LastBuild);
            Assert.IsNotNull(job.LastFailedBuild.Number);
            Assert.IsNotNull(job.LastStableBuild.Number);
            Assert.IsNotNull(job.LastSuccessfulBuild.Number);
        }

        [Test]
        public void TestGetJobWhenInvalid()
        {
            xmlService
                .Expect(call => call.GetPage(new Uri("http://www.example.com/jobs/first-job/api/xml/")))
                .Returns(XmlFactory.LoadInvalidXml());

            var descriptor = new JobDescriptor { Url = new Uri("http://www.example.com/jobs/first-job/") };

            var job = service.GetJob(descriptor);

            Assert.AreEqual(typeof(NullJob), job.GetType());
        }

        [Test]
        public void TestGetJobs()
        {
            ExpectCallsToGetJob();

            var descriptors = new List<JobDescriptor>();

            descriptors.Add(new JobDescriptor { Url = new Uri("http://www.example.com/jobs/first-job/") });
            descriptors.Add(new JobDescriptor { Url = new Uri("http://www.example.com/jobs/second-job/") });

            var jobs = service.GetJobs(descriptors);

            Assert.AreEqual(2, jobs.Count);
        }
    }
}
