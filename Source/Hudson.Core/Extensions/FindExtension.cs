using System;
using System.Xml;

namespace Hudson.Extensions
{
    public static class FindExtension
    {
        public static string Find(this XmlNode xml, string xpath)
        {
            var selectedNode = xml.SelectSingleNode(xpath);

            return selectedNode != null ? selectedNode.InnerText : String.Empty;
        }

        public static int FindInteger(this XmlNode xml, string xpath)
        {
            var valueAsString = xml.Find(xpath);

            int value;

            return int.TryParse(valueAsString, out value) ? value : 0;
        }

        public static string FindLast(this XmlNode xml, string xpath)
        {
            var selectedNodes = xml.SelectNodes(xpath);

            return selectedNodes == null || selectedNodes.Count == 0 ? 
                String.Empty : 
                selectedNodes[selectedNodes.Count - 1].InnerText;
        }

        /// <summary>
        /// Finds the given node and returns it as a <see cref="Uri"/> object.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="xpath">The xpath.</param>
        /// <returns></returns>
        public static Uri FindUri(this XmlNode xml, string xpath)
        {
            Uri result = null;

            var value = xml.Find(xpath);

            if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
            {
                result = new Uri(value);
            }

            return result;
        }

    }
}
