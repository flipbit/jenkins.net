using System.Web.Mvc;
using NUnit.Framework;

namespace Hudson.Web
{
    internal class MvcAssert
    {
        public static void RenderedView(ActionResult result, string name)
        {
            Assert.AreEqual(typeof(ViewResult), result.GetType());
            Assert.AreEqual(name, ((ViewResult)result).ViewName);
        }

        public static void RedirectedTo(ActionResult result, string controller, string action)
        {
            Assert.AreEqual(typeof(RedirectToRouteResult), result.GetType());
            Assert.AreEqual(controller, ((RedirectToRouteResult)result).RouteValues["controller"]);
            Assert.AreEqual(action, ((RedirectToRouteResult)result).RouteValues["action"]);
        }
    }
}
