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
                job = new Job();

                job.Name = xml.Find("//name");
                job.Description = xml.Find("//description");
                job.DisplayName = xml.Find("//displayName");
                job.Url = xml.FindUri("//url");
                job.Buildable = bool.Parse(xml.Find("//buildable"));
                job.BuildStatus = BuildStatusParser.Parse(xml.Find("//color"));
                job.HealthReport = xml.Find("//healthReport/description");
                job.IconUrl = xml.Find("//healthReport/iconUrl");
                job.Score = xml.FindInteger("//healthReport/score");
                job.InQueue = bool.Parse(xml.Find("//inQueue"));
                job.KeepDependencies = bool.Parse(xml.Find("//keepDependencies"));
                job.NextBuildNumber = xml.FindInteger("//nextBuildNumber");
            }

            return job;
        }
    }
}
