using System;

namespace Hudson.Domain
{
    /// <summary>
    /// Contains the basic information about a <see cref="Job"/>.
    /// </summary>
    public class JobDescriptor
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets the status of the <see cref="JobDescriptor"/>.
        /// </summary>
        /// <value>The status.</value>
        public BuildStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
    }
}
