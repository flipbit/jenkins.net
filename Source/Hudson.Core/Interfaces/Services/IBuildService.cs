using System.Collections.Generic;
using Hudson.Domain;

namespace Hudson.Interfaces.Services
{
    /// <summary>
    /// Interface to manipulate the <see cref="Build"/> class.
    /// </summary>
    public interface IBuildService
    {
        /// <summary>
        /// Gets the build.
        /// </summary>
        /// <param name="buildDescriptor">The build descriptor.</param>
        /// <returns></returns>
        Build GetBuild(BuildDescriptor buildDescriptor);

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <param name="buildDescriptors">The build descriptors.</param>
        /// <returns></returns>
        IList<Build> GetBuilds(IList<BuildDescriptor> buildDescriptors);
    }
}
