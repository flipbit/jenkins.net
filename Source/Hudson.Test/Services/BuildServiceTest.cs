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
    public class BuildServiceTest
    {
        private BuildService service;

        private Mock<IXmlService> xmlService;

        [SetUp]
        public void SetUp()
        {
            xmlService = new Mock<IXmlService>();

            service = new BuildService { XmlService = xmlService.Object };
        }

        [Test]
        public void TestGetBuild()
        {
            xmlService
                .Expect(call => call.GetPage(new Uri("http://www.example.com/api/xml/")))
                .Returns(XmlFactory.Load("BuildResponse.xml"));

            var descriptor = new BuildDescriptor { Url = new Uri("http://www.example.com/") };

            var build = service.GetBuild(descriptor);

            Assert.IsNotNull(build);
        }

        [Test]
        public void TestGetBuildWhenInvalid()
        {
            xmlService
                .Expect(call => call.GetPage(new Uri("http://www.example.com/api/xml/")))
                .Returns(XmlFactory.LoadInvalidXml());

            var descriptor = new BuildDescriptor { Url = new Uri("http://www.example.com/") };

            var build = service.GetBuild(descriptor);

            Assert.AreEqual(typeof(NullBuild), build.GetType());
        }

        [Test]
        public void TestGetBuilds()
        {
            xmlService
                .Expect(call => call.GetPage(It.IsAny<Uri>()))
                .Returns(XmlFactory.Load("BuildResponse.xml"))
                .AtMost(3);

            var descriptors = new List<BuildDescriptor>();

            descriptors.Add(new BuildDescriptor { Url = new Uri("http://www.example1.com/") });
            descriptors.Add(new BuildDescriptor { Url = new Uri("http://www.example2.com/") });
            descriptors.Add(new BuildDescriptor { Url = new Uri("http://www.example3.com/") });

            var builds = service.GetBuilds(descriptors);

            Assert.AreEqual(3, builds.Count);
        }
    }
}
