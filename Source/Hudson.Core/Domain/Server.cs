using System;
using System.Collections.Generic;

namespace Hudson.Domain
{
    /// <summary>
    /// An instance of a Hudson Server
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        public Server()
        {
            Jobs = new List<Job>();
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets the job descriptors.
        /// </summary>
        /// <value>The job descriptors.</value>
        public IList<Job> Jobs { get; private set; }
    }
}