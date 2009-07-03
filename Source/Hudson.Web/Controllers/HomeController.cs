using System;
using System.Web.Mvc;
using Hudson.Domain;
using Hudson.Interfaces.Services;
using Hudson.Web.Models;

namespace Hudson.Web.Controllers
{
    [HandleError(View = "Error")]
    public class HomeController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets or sets the hudson server.
        /// </summary>
        /// <value>The hudson server.</value>
        public string HudsonServer { get; set; }

        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        /// <value>The job.</value>
        public string Job { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the server service.
        /// </summary>
        /// <value>The server service.</value>
        public IServerService ServerService { get; set; }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var server = ServerService.GetServer(new Uri(HudsonServer));

            Build build = null;
            Job job = null;

            foreach(var j in server.Jobs)
            {
                if (string.Compare(j.Name, Job, true) != 0) continue;

                job = j;
                build = j.LastBuild;
            }

            if (build != null)
            {
                var model = new BuildModel
                                {
                                    Comments = build.Comments,
                                    Created = build.Created,
                                    Number = build.Number,
                                    Revision = build.Revision,
                                    Status = job.BuildStatus.ToString(),
                                    User = build.User
                                };

                model.User = "svnuser";
                model.Comments = "Changed site repository mappings";
                model.Revision = 104;

                return View(model);
            }

            return View();
        }

    }
}