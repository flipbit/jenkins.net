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
                build = new Build();

                // Hudson Properties
                build.Description = xml.Find("//shortDescription");
                build.Building = bool.Parse(xml.Find("//building"));
                build.Duration = xml.FindInteger("//duration");
                build.FullDisplayName = xml.Find("//fullDisplayName");
                build.KeepLog = bool.Parse(xml.Find("//keepLog"));
                build.Number = xml.FindInteger("//number");
                build.Success = xml.Find("//result") == "SUCCESS";
                build.Url = new Uri(xml.Find("//url"));
                build.User = xml.Find("//user");
                


                DateTime created;

                if (DateTime.TryParse(xml.Find("//date"), out created))
                {
                    build.Created = created;    
                }

                var seconds = Convert.ToDouble(xml.Find("//timestamp"));

                build.Created = JavaTimeStampToDateTime(seconds);

                // SVN Properties
                build.Revision = xml.FindInteger("//revision");
                build.Comments = xml.Find("//msg");
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
