using System.Xml;
using Hudson.Domain;
using Hudson.Extensions;
using Hudson.Parsers;

namespace Hudson.Mappers
{
    /// <summary>
    /// Creates a <see cref="Job"/> object from some XML
    /// </summary>
    public class JobMapper
    {
        /// <summary>
        /// Maps the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public Job Map(XmlDocument xml)
        {
            Job job = null;

            if (xml != null)
            {
                job = new Job
                {
                    Name = xml.Find("//name"),
                    Description = xml.Find("//description"),
                    DisplayName = xml.Find("//displayName"),
                    Url = xml.FindUri("//url"),
                    Buildable = bool.Parse(xml.Find("//buildable")),
                    BuildStatus = BuildStatusParser.Parse(xml.Find("//color")),
                    HealthReport = xml.Find("//healthReport/description"),
                    IconUrl = xml.Find("//healthReport/iconUrl"),
                    Score = xml.FindInteger("//healthReport/score"),
                    InQueue = bool.Parse(xml.Find("//inQueue")),
                    KeepDependencies = bool.Parse(xml.Find("//keepDependencies")),
                    NextBuildNumber = xml.FindInteger("//nextBuildNumber")
                };
            }

            return job;
        }
    }
}
