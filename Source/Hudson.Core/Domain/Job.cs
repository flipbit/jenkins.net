using System.Collections.Generic;

namespace Hudson.Domain
{
    /// <summary>
    /// Represents a Job in Hudson
    /// </summary>
    public class Job : JobDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Job"/> class.
        /// </summary>
        public Job()
        {
            Builds = new List<Build>();
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public BuildStatus BuildStatus { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Job"/> is buildable.
        /// </summary>
        /// <value><c>true</c> if buildable; otherwise, <c>false</c>.</value>
        public bool Buildable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [in queue].
        /// </summary>
        /// <value><c>true</c> if [in queue]; otherwise, <c>false</c>.</value>
        public bool InQueue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Job"/> is description.
        /// </summary>
        /// <value><c>true</c> if description; otherwise, <c>false</c>.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the first build.
        /// </summary>
        /// <value>The first build.</value>
        public Build FirstBuild { get; set; }

        /// <summary>
        /// Gets or sets the last build.
        /// </summary>
        /// <value>The last build.</value>
        public Build LastBuild { get; set; }

        /// <summary>
        /// Gets or sets the last successful build.
        /// </summary>
        /// <value>The last successful build.</value>
        public Build LastSuccessfulBuild { get; set; }

        /// <summary>
        /// Gets or sets the last failed build.
        /// </summary>
        /// <value>The last failed build.</value>
        public Build LastFailedBuild { get; set; }

        /// <summary>
        /// Gets or sets the last stable build.
        /// </summary>
        /// <value>The last stable build.</value>
        public Build LastStableBuild { get; set; }

        /// <summary>
        /// Gets or sets the next build number.
        /// </summary>
        /// <value>The next build number.</value>
        public int NextBuildNumber { get; set; }

        /// <summary>
        /// Gets or sets the health report.
        /// </summary>
        /// <value>The health report.</value>
        public string HealthReport { get; set; }

        /// <summary>
        /// Gets or sets the icon URL.
        /// </summary>
        /// <value>The icon URL.</value>
        public string IconUrl { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>The score.</value>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [keep dependencies].
        /// </summary>
        /// <value><c>true</c> if [keep dependencies]; otherwise, <c>false</c>.</value>
        public bool KeepDependencies { get; set; }

        /// <summary>
        /// Gets or sets the builds.
        /// </summary>
        /// <value>The builds.</value>
        public IList<Build> Builds { get; set; }
    }
}