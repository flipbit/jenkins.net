using System;

namespace Hudson.Domain
{
    /// <summary>
    /// Represents an invalid <see cref="Server"/>
    /// </summary>
    public class NullServer : Server
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullServer"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public NullServer(Uri url)
        {
            Url = url;

            Description = "Unable to load server";
        }
    }
}
