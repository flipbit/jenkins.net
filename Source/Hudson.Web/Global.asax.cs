using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using MvcContrib.Castle;

namespace Hudson
{
    public class MvcApplication : HttpApplication, IContainerAccessor
    {
        private void InitializeWindsor()
        {
            if (Container != null) return;

            Container = new WindsorContainer(new XmlInterpreter(Server.MapPath("~/configuration/hudson.config")));

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container));
        }

        public IWindsorContainer Container { get; private set; }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });
        }

        protected void Application_Start()
        {
            InitializeWindsor();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}