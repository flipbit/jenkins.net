using System.Collections.Generic;
using Hudson.Domain;

namespace Hudson.Interfaces.Services
{
    /// <summary>
    /// Interface to manipulate the <see cref="Job"/> class.
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// Gets the full <see cref="Job"/> from the given <see cref="JobDescriptor"/>.
        /// </summary>
        /// <param name="jobDescriptor">The job descriptor.</param>
        /// <returns></returns>
        Job GetJob(JobDescriptor jobDescriptor);

        /// <summary>
        /// Gets a collection of full <see cref="Job"/>s from the given <see cref="JobDescriptor"/> collection.
        /// </summary>
        /// <param name="jobDescriptors">The job descriptors.</param>
        /// <returns></returns>
        IList<Job> GetJobs(IList<JobDescriptor> jobDescriptors);
    }
}
