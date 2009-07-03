using System;
using Hudson.Domain;
using Hudson.Factories;
using Hudson.Interfaces.Services;
using Moq;
using NUnit.Framework;

namespace Hudson.Services
{
    [TestFixture]
    public class ServerServiceTest
    {
        private ServerService service;

        private Mock<IJobService> jobService;
        private Mock<IXmlService> httpService;

        [SetUp]
        public void SetUp()
        {
            jobService = new Mock<IJobService>();
            httpService = new Mock<IXmlService>();

            service = new ServerService { JobService = jobService.Object, XmlService = httpService.Object };
        }

        [Test]
        public void TestGetServer()
        {
            httpService
                .Expect(call => call.GetPage(new Uri("http://www.example.com/api/xml/")))
                .Returns(XmlFactory.Load("ServerResponse.Xml"));

            jobService
                .Expect(call => call.GetJob(It.IsAny<JobDescriptor>()))
                .Returns(new Job())
                .AtMost(3);
                
            var server = service.GetServer(new Uri("http://www.example.com/"));

            Assert.IsNotNull(server);

            Assert.AreEqual("<center><b>Build Server</b></center>", server.Description);
            Assert.AreEqual(3, server.Jobs.Count);
        }

        [Test]
        public void TestGetServerWhenXmlInvalid()
        {
            httpService
                .Expect(call => call.GetPage(new Uri("http://www.example.com/api/xml/")))
                .Returns(XmlFactory.LoadInvalidXml());

            jobService
                .Expect(call => call.GetJob(It.IsAny<JobDescriptor>()))
                .Returns(new Job())
                .AtMost(3);

            var server = service.GetServer(new Uri("http://www.example.com/"));

            Assert.AreEqual(typeof(NullServer), server.GetType());
            Assert.AreEqual("http://www.example.com/", server.Url.ToString());
        }
    }
}
