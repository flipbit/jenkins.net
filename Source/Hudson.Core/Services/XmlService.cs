using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Hudson.Core;
using Hudson.Domain;
using Hudson.Interfaces;
using Hudson.Interfaces.Services;

namespace Hudson.Services
{
    /// <summary>
    /// Service to manipulate <see cref="XmlPage"/> objects.
    /// </summary>
    public class XmlService : IXmlService
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; set; }

        /// <summary>
        /// Downloads the page from the given URL
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public XmlPage GetPage(Uri url)
        {
            XmlPage page;

            try
            {
                var request = WebRequest.Create(url);

                request.Method = "GET";
                request.Timeout = 10000;

                if (Context.RequireAuthentication)
                {
                    var authInfo = Context.Username + ":" + Context.Password;

                    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

                    request.Headers["Authorization"] = "Basic " + authInfo;
                }

                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            page = new XmlPage(url, reader.ReadToEnd());
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                throw new HudsonException(ex.Message);
            }

            return page;
        }
    }
}
