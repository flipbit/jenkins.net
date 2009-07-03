using System.Collections.Generic;
using Hudson.Core;
using Hudson.Domain;
using Hudson.Interfaces.Services;
using Hudson.Mappers;

namespace Hudson.Services
{
    /// <summary>
    /// Service to manipulate <see cref="Job"/> objects.
    /// </summary>
    public class JobService : IJobService 
    {
        private readonly BuildDescriptorMapper mapper;

        #region Service Properties
        
        /// <summary>
        /// Gets or sets the XML service.
        /// </summary>
        /// <value>The XML service.</value>
        public IXmlService XmlService { get; set; }

        /// <summary>
        /// Gets or sets the build service.
        /// </summary>
        /// <value>The build service.</value>
        public IBuildService BuildService { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="JobService"/> class.
        /// </summary>
        public JobService()
        {
            mapper = new BuildDescriptorMapper();
        }

        /// <summary>
        /// Gets the full <see cref="Job"/> from the given <see cref="JobDescriptor"/>.
        /// </summary>
        /// <param name="jobDescriptor">The job descriptor.</param>
        /// <returns></returns>
        public Job GetJob(JobDescriptor jobDescriptor)
        {
            Job job = new NullJob(jobDescriptor);

            var url = new XmlApiPrepender().Prepend(jobDescriptor.Url);

            var xml = XmlService.GetPage(url);

            if (xml.IsValid)
            {
                job = new JobMapper().Map(xml.Contents);

                /* Get Builds (takes a while...)
                var descriptors = mapper.MapMany(xml.Contents, "//build");
                job.Builds = BuildService.GetBuilds(descriptors); */

                // Get Last Build
                var firstBuild = mapper.Map(xml.Contents, "//firstBuild");
                job.FirstBuild = BuildService.GetBuild(firstBuild);

                // Get Last Build
                var lastBuild = mapper.Map(xml.Contents, "//lastBuild");
                job.LastBuild = BuildService.GetBuild(lastBuild);

                // Get Last Failed Build
                var lastFailedBuild = mapper.Map(xml.Contents, "//lastFailedBuild");
                job.LastFailedBuild = BuildService.GetBuild(lastFailedBuild);

                // Get Last Stable Build
                var lastStableBuild = mapper.Map(xml.Contents, "//lastStableBuild");
                job.LastStableBuild = BuildService.GetBuild(lastStableBuild);

                // Get Last Successful Build
                var lastSuccessfulBuild = mapper.Map(xml.Contents, "//lastSuccessfulBuild");
                job.LastSuccessfulBuild = BuildService.GetBuild(lastSuccessfulBuild);
            }

            return job;
        }

        /// <summary>
        /// Gets a collection of full <see cref="Job"/>s from the given <see cref="JobDescriptor"/> collection.
        /// </summary>
        /// <param name="jobDescriptors">The job descriptors.</param>
        /// <returns></returns>
        public IList<Job> GetJobs(IList<JobDescriptor> jobDescriptors)
        {
            var jobs = new List<Job>();

            foreach(var descriptor in jobDescriptors)
            {
                jobs.Add(GetJob(descriptor));
            }

            return jobs;
        }
    }
}
