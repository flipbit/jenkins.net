using System;
using System.Web.Mvc;
using Hudson.Interfaces;
using Hudson.Interfaces.Services;

namespace Hudson.Web.Controllers
{
    /// <summary>
    /// Controller to handle connecting to a Hudson Server instance.
    /// </summary>
    public class ServerController : AbstractController
    {
        public const string IndexName = "index";

        #region Service Properties
        
        /// <summary>
        /// Gets or sets the server service.
        /// </summary>
        /// <value>The server service.</value>
        public IServerService ServerService { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; set; }

        #endregion

        /// <summary>
        /// Displays the UI to allow the user to enter the server URL, username
        /// and password.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Settings.Server))
            {
                return Connect(Settings.Server, Settings.Username, Settings.Password);
            }
            else
            {
                return View();                
            }
        }

        /// <summary>
        /// Connects the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public ActionResult Connect(string url, string username, string password)
        {
            // Assign username/password to the context
            Context.Username = username;
            Context.Password = password;

            // Make sure a protocol is specified
            if (!url.Contains("://")) url = "http://" + url;

            try
            {
                // Download the server information
                HudsonServer = ServerService.GetServer(new Uri(url));

                Settings.Server = url;
                Settings.Username = username;
                Settings.Password = password;

                return RedirectToAction(JobController.ListName, JobController.ControllerName);
            }
            catch (Exception ex)
            {
                Error = ex.Message;

                return RedirectToAction(IndexName);
            }
        }

    }
}
