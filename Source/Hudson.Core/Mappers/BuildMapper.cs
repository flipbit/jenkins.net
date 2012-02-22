using System;
using System.Xml;
using Hudson.Domain;
using Hudson.Extensions;

namespace Hudson.Mappers
{
    /// <summary>
    /// Creates a new <see cref="Build"/> object from some XML.
    /// </summary>
    public class BuildMapper
    {
        /// <summary>
        /// Maps the specified XML into a <see cref="Build"/> object.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public Build Map(XmlDocument xml)
        {
            Build build = null;

            if (xml != null)
            {
                build = new Build
                {
                    Description = xml.Find("//shortDescription"),
                    Building = bool.Parse(xml.Find("//building")),
                    Duration = xml.FindInteger("//duration"),
                    FullDisplayName = xml.Find("//fullDisplayName"),
                    KeepLog = bool.Parse(xml.Find("//keepLog")),
                    Number = xml.FindInteger("//number"),
                    Success = xml.Find("//result") == "SUCCESS",
                    Url = new Uri(xml.Find("//url")),
                    User = xml.FindLast("//author//fullName")
                };

                // Hudson Properties

                DateTime created;

                if (DateTime.TryParse(xml.Find("//date"), out created))
                {
                    build.Created = created;    
                }

                var seconds = Convert.ToDouble(xml.Find("//timestamp"));

                build.Created = JavaTimeStampToDateTime(seconds);

                // GIT Properties
                var rev = xml.Find("//lastBuiltRevision//SHA1");
                build.Revision =  rev.Length > 5 ? rev.Substring(0, 5) : rev;
                build.Comments = xml.FindLast("//msg");
            }

            return build;
        }

        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is millisecods past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            dtDateTime = dtDateTime.AddSeconds(Math.Round(javaTimeStamp / 1000)).ToLocalTime();

            return dtDateTime;
        }

    }
}
