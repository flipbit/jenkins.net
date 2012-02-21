using Hudson.Interfaces.Services;
using Moq;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace Hudson.Web.Controllers
{
    [TestFixture]
    [Ignore]
    public class ServerControllerTest
    {
        private ServerController controller;

        private TestControllerBuilder builder;

        private Mock<IServerService> serverService;

        [SetUp]
        public void SetUp()
        {
            builder = new TestControllerBuilder();
            serverService = new Mock<IServerService>();

            controller = new ServerController { ServerService = serverService.Object };

            builder.InitializeController(controller);
        }

        [Test]
        public void TestUserEntersServerUrl()
        {
            var result = controller.Connect("http://www.example.com/", string.Empty, string.Empty);

            MvcAssert.RedirectedTo(result, JobController.ControllerName, JobController.ListName);
        }
    }
}
