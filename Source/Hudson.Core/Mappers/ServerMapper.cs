using System.Xml;
using Hudson.Domain;
using Hudson.Extensions;

namespace Hudson.Mappers
{
    /// <summary>
    /// Maps an XML response from a Hudson server to a <see cref="Server"/> object.
    /// </summary>
    public class ServerMapper
    {
        /// <summary>
        /// Maps the specified XML to a <see cref="Server"/> object.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public Server Map(XmlDocument xml)
        {
            Server server = null;

            if (xml != null)
            {
                server = new Server
                             {
                                 Description = xml.Find("//description"), 
                                 Url = xml.FindUri("//url")
                             };
            }

            return server;
        }
    }
}
