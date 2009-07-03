using System;
using Hudson.Domain;

namespace Hudson.Interfaces.Services
{
    /// <summary>
    /// Interface to manipulate of the <see cref="Server"/> class.
    /// </summary>
    public interface IServerService
    {
        /// <summary>
        /// Gets the Hudson <see cref="Server"/>.
        /// </summary>
        /// <param name="url">The URL of the server.</param>
        /// <returns></returns>
        Server GetServer(Uri url);
    }
}
