using System;
using Hudson.Core;
using Hudson.Domain;
using Hudson.Interfaces.Services;
using Hudson.Mappers;

namespace Hudson.Services
{
    /// <summary>
    /// Service to manipulate the <see cref="Server"/> object.
    /// </summary>
    public class ServerService : IServerService 
    {
        #region Service Properties

        /// <summary>
        /// Gets or sets the HTTP service.
        /// </summary>
        /// <value>The HTTP service.</value>
        public IXmlService XmlService { get; set; }

        /// <summary>
        /// Gets or sets the job service.
        /// </summary>
        /// <value>The job service.</value>
        public IJobService JobService { get; set; }

        #endregion

        /// <summary>
        /// Gets the Hudson <see cref="Server"/>.
        /// </summary>
        /// <param name="url">The URL of the server.</param>
        /// <returns></returns>
        public Server GetServer(Uri url)
        {
            Server server = new NullServer(url);

            url = new XmlApiPrepender().Prepend(url);

            var page = XmlService.GetPage(url);

            if (page.IsValid)
            {
                server = new ServerMapper().Map(page.Contents);

                var descriptors = new JobDescriptorMapper().Map(page.Contents);

                foreach (var descriptor in descriptors)
                {
                    var job = JobService.GetJob(descriptor);

                    server.Jobs.Add(job);
                }
            }

            return server;
        }
    }
}
