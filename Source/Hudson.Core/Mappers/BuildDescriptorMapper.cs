using System;
using System.Collections.Generic;
using System.Xml;
using Hudson.Domain;
using Hudson.Extensions;

namespace Hudson.Mappers
{
    /// <summary>
    /// Maps a <see cref="BuildDescriptor"/> from some XML
    /// </summary>
    public class BuildDescriptorMapper
    {
        /// <summary>
        /// Maps the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public BuildDescriptor Map(XmlNode xml)
        {
            BuildDescriptor descriptor = null;

            if (xml != null)
            {
                descriptor = new BuildDescriptor
                                 {
                                     Number = xml.FindInteger("//number"),
                                     Url = new Uri(xml.Find("//url"))
                                 };
            }

            return descriptor;
        }

        /// <summary>
        /// Maps the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="xpath">The xpath.</param>
        /// <returns></returns>
        public BuildDescriptor Map(XmlNode xml, string xpath)
        {
            BuildDescriptor descriptor = null;

            var node = xml.SelectSingleNode(xpath);

            if (node != null)
            {
                descriptor = new BuildDescriptor
                                 {
                                     Number = xml.FindInteger(xpath + "/number"),
                                     Url = new Uri(xml.Find(xpath + "/url"))
                                 };
            }

            return descriptor;
        }

        /// <summary>
        /// Maps the many.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="xpath">The xpath.</param>
        /// <returns></returns>
        public IList<BuildDescriptor> MapMany(XmlNode xml, string xpath)
        {
            var descriptors = new List<BuildDescriptor>();

            var nodes = xml.SelectNodes(xpath);

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    var descriptor = Map(node.CloneNode(true));

                    if (descriptor != null) descriptors.Add(descriptor);
                }
            }

            return descriptors;
        }
    }
}
