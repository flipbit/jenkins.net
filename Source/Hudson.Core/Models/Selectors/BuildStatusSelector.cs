using System.Collections.Generic;
using Hudson.Domain;

namespace Hudson.Models.Selectors
{
    /// <summary>
    /// Selects a <see cref="BuildStatus" /> to represent a <see cref="JobModel"/> collection.
    /// </summary>
    public class BuildStatusSelector
    {
        /// <summary>
        /// Selects the <see cref="BuildStatus"/> for the specified jobs.
        /// </summary>
        /// <param name="jobs">The jobs.</param>
        /// <returns></returns>
        public BuildStatus Select(IList<JobModel> jobs)
        {
            var result = BuildStatus.Unknown;

            foreach(var job in jobs)
            {
                if (job.BuildStatus <= result) continue;

                result = job.BuildStatus;
            }

            return result;
        }
    }
}
