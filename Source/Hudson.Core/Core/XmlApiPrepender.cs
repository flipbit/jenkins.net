using System;

namespace Hudson.Core
{
    /// <summary>
    /// Prepends the Hudson XML API to a given url. 
    /// </summary>
    public class XmlApiPrepender
    {
        /// <summary>
        /// Prepends the specified URL with the Hudson API path.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public Uri Prepend(Uri url)
        {
            var rawUrl = url.AbsoluteUri.ToLower();

            if (!rawUrl.EndsWith("/")) rawUrl += "/";

            if (!rawUrl.EndsWith("/api/xml/"))
            {                                
                rawUrl += "api/xml/";                
            }
           
            return new Uri(rawUrl);
        }
    }
}