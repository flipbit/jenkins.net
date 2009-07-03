using Hudson.Domain;
using Hudson.Factories;
using NUnit.Framework;

namespace Hudson.Mappers
{
    [TestFixture]
    public class JobDescriptorMapperTest
    {
        private JobDescriptorMapper mapper;

        [SetUp]
        public void SetUp()
        {
            mapper = new JobDescriptorMapper();
        }

        [Test]
        public void TestMapJobDescriptors()
        {
            var xml = XmlFactory.Load("ServerResponse.Xml");

            var descriptors = mapper.Map(xml.Contents);

            Assert.AreEqual(3, descriptors.Count);
            Assert.AreEqual("first-job", descriptors[0].Name);
            Assert.AreEqual("http://www.example.com/job/first-job/", descriptors[0].Url.ToString());
            Assert.AreEqual(BuildStatus.Unknown, descriptors[0].Status);
        }

    }
}
