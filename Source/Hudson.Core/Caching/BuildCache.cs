using System.Collections.Generic;
using Hudson.Domain;
using Hudson.Interfaces.Services;

namespace Hudson.Caching
{
    /// <summary>
    /// Caches calls to the build service in order to reduce
    /// network bandwidth.
    /// </summary>
    public class BuildCache : IBuildService 
    {
        /// <summary>
        /// Gets or sets the cache.
        /// </summary>
        /// <value>The cache.</value>
        public IDictionary<string, Build> Cache { get; set; }

        /// <summary>
        /// Gets or sets the build service.
        /// </summary>
        /// <value>The build service.</value>
        public IBuildService BuildService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildCache"/> class.
        /// </summary>
        public BuildCache()
        {
            Cache = new Dictionary<string, Build>();
        }

        /// <summary>
        /// Gets the build.
        /// </summary>
        /// <param name="buildDescriptor">The build descriptor.</param>
        /// <returns></returns>
        public Build GetBuild(BuildDescriptor buildDescriptor)
        {
            Build build = new NullBuild(buildDescriptor);

            if (buildDescriptor != null)
            {
                var key = build.Number + "-" + build.Url;

                if (Cache.ContainsKey(key))
                {
                    build = Cache[key];

                    System.Diagnostics.Debug.WriteLine("Hit: " + key);
                }
                else
                {
                    build = BuildService.GetBuild(buildDescriptor);

                    if (!build.Building) Cache.Add(key, build);

                    System.Diagnostics.Debug.WriteLine("Miss: " + key);
                }
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
                builds.Add(GetBuild(descriptor));
            }

            return builds;
        }
    }
}
