using System;
using Hudson.Domain;

namespace Hudson.Models
{
    /// <summary>
    /// Model class to repesent the 
    /// </summary>
    public class JobModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobModel"/> class.
        /// </summary>
        public JobModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobModel"/> class.
        /// </summary>
        /// <param name="job">The job.</param>
        public JobModel(Job job)
        {
            Update(job);
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets the number of the current build.
        /// </summary>
        /// <value>The number.</value>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="JobModel"/> is building.
        /// </summary>
        /// <value><c>true</c> if building; otherwise, <c>false</c>.</value>
        public BuildStatus BuildStatus { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>The comment.</value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the last stable build time.
        /// </summary>
        /// <value>The last stable build time.</value>
        public long LastStableBuildTime { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Updates the model specified job.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <returns></returns>
        public bool Update(Job job)
        {
            var updated = false;

            if (job.LastBuild.Number > Number)
            {
                updated = true;
            }
            else if (job.LastBuild.Number == Number)
            {
                if (job.BuildStatus != BuildStatus)
                {
                    BuildStatus = job.BuildStatus;

                    updated = true;
                }
            }

            if (updated)
            {
                Number = job.LastBuild.Number;
                BuildStatus = job.BuildStatus;
                Comment = job.LastBuild.Comments;
                User = job.LastBuild.User;
                Created = job.LastBuild.Created;
                Name = job.Name;
                Url = job.Url;
                LastStableBuildTime = job.LastStableBuild.Duration / 1000;
            }

            if (string.IsNullOrEmpty(Comment)) Comment = job.Description;

            if (string.IsNullOrEmpty(Comment)) Comment = "No comments for this build.";

            return updated;
        }
    }
}
