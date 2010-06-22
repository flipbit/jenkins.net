using System.Web.Mvc;
using Hudson.Domain;

namespace Hudson.Web.Controllers
{
    /// <summary>
    /// An abstract class that contains shared functionality for all controllers.
    /// </summary>
    public abstract class AbstractController : Controller
    {
        /// <summary>
        /// Gets or sets the hudson server.
        /// </summary>
        /// <value>The hudson server.</value>
        public Server HudsonServer
        {
            get
            {
                return (Server)Session["server"];
            }
            set
            {
                Session["server"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public string Error
        {
            get
            {
                return (string)Session["error"];
            }
            set
            {
                Session["error"] = value;
            }
        }

        protected string ServerName
        {
            get { return GetValue("Server"); }
        }

        protected string Username
        {
            get { return GetValue("user"); }
        }

        protected string Password
        {
            get { return GetValue("pass"); }
        }

        protected string JobName
        {
            get { return GetValue("job"); }
        }


        protected string GetValue(string name)
        {
            return Request.QueryString[name] ?? string.Empty;
        }
    }
}
