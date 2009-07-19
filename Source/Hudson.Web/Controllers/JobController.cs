using System.Web.Mvc;
using Hudson.Domain;
using Hudson.Interfaces.Services;
using Hudson.Models;
using Hudson.Web.Models;

namespace Hudson.Web.Controllers
{
    /// <summary>
    /// Controller for displaying the Jobs on a Hudson Server.
    /// </summary>
    public class JobController : AbstractController
    {
        public const string ControllerName = "job";
        public const string ListName = "list";

        /// <summary>
        /// Gets or sets the job service.
        /// </summary>
        /// <value>The job service.</value>
        public IJobService JobService { get; set; }

        /// <summary>
        /// Lists the jobs on the Hudson server.
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var model = new ServerModel();

            model.Update(HudsonServer);

            return View(model);
        }

        public ActionResult Monitor(string id)
        {
            if (HudsonServer == null)
            {
                return RedirectToAction("index", "server");
            }

            Build build = null;
            Job job = null;

            foreach (var j in HudsonServer.Jobs)
            {
                if (string.Compare(j.Name, id, true) != 0) continue;

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
                    User = build.User,
                    Name = job.Name
                };

                return View(model);
            }

            return RedirectToAction(ListName);
        }

    }
}
