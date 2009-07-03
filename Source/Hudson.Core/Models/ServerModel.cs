using System.Collections.Generic;
using System.Linq;
using Hudson.Domain;
using Hudson.Models.Selectors;

namespace Hudson.Models
{
    /// <summary>
    /// Model class to represent the <see cref="Server"/> object.
    /// </summary>
    public class ServerModel
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerModel"/> class.
        /// </summary>
        public ServerModel()
        {
            Jobs = new List<JobModel>();
        }

        /// <summary>
        /// Gets or sets the jobs.
        /// </summary>
        /// <value>The jobs.</value>
        public IList<JobModel> Jobs { get; set; }

        /// <summary>
        /// Gets the build status for the <see cref="Jobs"/>.
        /// </summary>
        /// <value>The build status.</value>
        public BuildStatus BuildStatus
        {
            get
            {
                // Return a build status based upon the collection
                return new BuildStatusSelector().Select(Jobs);
            }
        }

        /// <summary>
        /// Gets the title based upon the <see cref="Job"/> collection.
        /// </summary>
        /// <value>The title.</value>
        public string Title 
        { 
            get
            {
                // Return a build title based upon the contents of the collection
                return new TitleSelector().Select(Jobs);
            } 
        }

        /// <summary>
        /// Gets the text based upon the <see cref="Job"/> collection.
        /// </summary>
        /// <value>The text.</value>
        public string Text 
        { 
            get
            {
                return new TextSelector().Select(Jobs);
            }
        }

        /// <summary>
        /// Gets the percentage of running builds.
        /// </summary>
        /// <value>The percent.</value>
        public int Percent
        {
            get
            {
                return new BuildPercentSelector().Select(Jobs);
            }
        }

        /// <summary>
        /// Updates the model from the specified server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns></returns>
        public bool Update(Server server)
        {
            var updated = false;

            foreach (var job in server.Jobs)
            {
                bool jobUpdated;

                var jobName = job.Name;

                if (Contains(jobName))
                {
                    var jobModel = Jobs.First(j => string.Compare(j.Name, jobName, true) == 0);

                    jobUpdated = jobModel.Update(job);
                }
                else
                {
                    Jobs.Add(new JobModel(job));

                    jobUpdated = true;
                }

                if (jobUpdated) updated = true;
            }

            return updated;
        }

        public bool Contains(string name)
        {
            return Jobs.Count(job => string.Compare(job.Name, name, true) == 0) > 0;
        }
    }
}
