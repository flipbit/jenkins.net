using System.Collections.Generic;
using Hudson.Core;
using Hudson.Domain;
using Hudson.Interfaces.Services;
using Hudson.Mappers;

namespace Hudson.Services
{
    /// <summary>
    /// Service to manipulate <see cref="Build"/> objects.
    /// </summary>
    public class BuildService : IBuildService 
    {
        /// <summary>
        /// Gets or sets the XML service.
        /// </summary>
        /// <value>The XML service.</value>
        public IXmlService XmlService { get; set; }

        /// <summary>
        /// Gets the build.
        /// </summary>
        /// <param name="buildDescriptor">The build descriptor.</param>
        /// <returns></returns>
        public Build GetBuild(BuildDescriptor buildDescriptor)
        {
            Build build = new NullBuild(buildDescriptor);

            var url = new XmlApiPrepender().Prepend(build.Url);

            var xml = XmlService.GetPage(url);

            if (xml.IsValid)
            {
                build = new BuildMapper().Map(xml.Contents);
            }

            return build;
        }

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <param name="buildDescriptors">The build descriptors.</param>
        /// <returns></returns>
        public IList<Build> GetBuilds(IList<BuildDescriptor> buildDescriptors)
        {
            var builds = new List<Build>();

            foreach(var descriptor in buildDescriptors)
            {
                var build = GetBuild(descriptor);

                builds.Add(build);
            }

            return builds;
        }
    }
}
