using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Hudson.Domain;
using Hudson.Extensions;
using Hudson.Parsers;

namespace Hudson.Mappers
{
    /// <summary>
    /// Maps a collection of <see cref="JobDescriptor"/> objects from some XML
    /// </summary>
    public class JobDescriptorMapper
    {
        /// <summary>
        /// Maps the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public IList<JobDescriptor> Map(XmlDocument xml)
        {
            var descriptors = new List<JobDescriptor>();

            var nodes = xml.SelectNodes("//job");

            if (nodes != null)
            {
                descriptors.AddRange(
                    nodes.Cast<XmlNode>()
                    .Select(node => node.CloneNode(true))
                    .Select(xmlNode => new JobDescriptor
                    {
                        Name = xmlNode.Find("//name"),
                        Url = new Uri(xmlNode.Find("//url")),
                        Status = BuildStatusParser.Parse(xmlNode.Find("//color"))
                    }));
            }

            return descriptors;
        }
    }
}
