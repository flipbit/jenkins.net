using System;
using Hudson.Domain;
using Hudson.Interfaces.Services;
using Moq;
using NUnit.Framework;

namespace Hudson.Caching
{
    [TestFixture]
    public class BuildCacheTest
    {
        private BuildCache cache;

        private Mock<IBuildService> buildService;

        [SetUp]
        public void SetUp()
        {
            buildService = new Mock<IBuildService>();

            cache = new BuildCache { BuildService = buildService.Object };
        }

        [Test]
        public void TestGetBuildWhenCacheNotHit()
        {
            var descriptor = new BuildDescriptor { Number = 10, Url = new Uri("http://www.example.com/") };

            buildService
                .Expect(call => call.GetBuild(descriptor))
                .Returns(new Build());

            var build = cache.GetBuild(descriptor);

            Assert.IsNotNull(build);
            Assert.IsTrue(cache.Cache.ContainsKey("10-http://www.example.com/"));
        }

        [Test]
        public void TestGetBuildWhenCacheNotHitDoesntInsertRunningBuilds()
        {
            var descriptor = new BuildDescriptor { Number = 10, Url = new Uri("http://www.example.com/") };

            buildService
                .Expect(call => call.GetBuild(descriptor))
                .Returns(new Build{ Building = true});

            var build = cache.GetBuild(descriptor);

            Assert.IsNotNull(build);
            Assert.IsFalse(cache.Cache.ContainsKey("10-http://www.example.com/"));
        }

        [Test]
        public void TestGetBuildWhenCacheHit()
        {
            var descriptor = new BuildDescriptor { Number = 10, Url = new Uri("http://www.example.com/") };

            buildService
                .Expect(call => call.GetBuild(descriptor))
                .Never();

            cache.Cache.Add("10-http://www.example.com/", new Build());

            var build = cache.GetBuild(descriptor);

            Assert.IsNotNull(build);
        }
    }
}
