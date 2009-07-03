using System;
using Hudson.Domain;

namespace Hudson.Interfaces.Services
{
    /// <summary>
    /// Service to manipulate the <see cref="XmlPage"/> class.
    /// </summary>
    public interface IXmlService
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        IContext Context { get; set; }

        /// <summary>
        /// Downloads the page from the given URL
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        XmlPage GetPage(Uri url);
    }
}
