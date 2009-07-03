using Hudson.Interfaces.Services;
using Moq;
using NUnit.Framework;

namespace Hudson.Tray.Presenters
{
    [TestFixture]
    public class BasePresenterTest
    {
        private FakePresenter presenter;

        private Mock<IServerService> serverService;


        [SetUp]
        public void SetUp()
        {
            serverService = new Mock<IServerService>();

            presenter = new FakePresenter { ServerService = serverService.Object };
        }

        [Test]
        public void TestPoll()
        {
            presenter.Poll();
        }

        [Test]
        public void TestPollWhenErrorOccurs()
        {

        }
    }
}
