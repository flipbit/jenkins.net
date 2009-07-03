using System;
using NUnit.Framework;

namespace Hudson.Core
{
    [TestFixture]
    public class XmlApiPrependerTest
    {
        private XmlApiPrepender prepender;

        [SetUp]
        public void SetUp()
        {
            prepender = new XmlApiPrepender();
        }

        [Test]
        public void TestPrependOnUrl()
        {
            var url = prepender.Prepend(new Uri("http://www.example.com/"));

            Assert.AreEqual("http://www.example.com/api/xml/", url.AbsoluteUri);
        }

        [Test]
        public void TestPrependOnUrlWithNoTrailingSlash()
        {
            var url = prepender.Prepend(new Uri("http://www.example.com"));

            Assert.AreEqual("http://www.example.com/api/xml/", url.AbsoluteUri);
        }

        [Test]
        public void TestPrependOnUrlWhenAlreadyPrepended()
        {
            var url = prepender.Prepend(new Uri("http://www.example.com/api/xml/"));

            Assert.AreEqual("http://www.example.com/api/xml/", url.AbsoluteUri);
        }

        [Test]
        public void TestPrependOnUrlWhenAlreadyPrependedWithNoSlash()
        {
            var url = prepender.Prepend(new Uri("http://www.example.com/api/xml"));

            Assert.AreEqual("http://www.example.com/api/xml/", url.AbsoluteUri);
        }
    }
}