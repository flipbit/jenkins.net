using System;
using NUnit.Framework;

namespace Hudson.Services
{
    [TestFixture]
    public class XmlServiceTest
    {
        private XmlService service;

        [SetUp]
        public void SetUp()
        {
            service = new XmlService();
        }

        [Test]
        public void TestDownloadInvalidUrl()
        {
            try
            {
                service.GetPage(new Uri("oops"));

                Assert.Fail("Exception noT thrown!");
            }
            catch(UriFormatException)
            {
                
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }                       
        }
    }
}
