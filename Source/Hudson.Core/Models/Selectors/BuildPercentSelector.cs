using System;
using System.Collections.Generic;
using Hudson.Domain;

namespace Hudson.Models.Selectors
{
    /// <summary>
    /// Calculates the percentage complete for any running builds
    /// </summary>
    public class BuildPercentSelector
    {
        /// <summary>
        /// Selects the percent complete of any running builds in the specified jobs.
        /// </summary>
        /// <param name="jobs">The jobs.</param>
        /// <returns></returns>
        public int Select(IList<JobModel> jobs)
        {
            JobModel longestJob = null;

            var result = -1;

            foreach (var job in jobs)
            {
                if (job.BuildStatus != BuildStatus.Building) continue;

                if (longestJob == null)
                {
                    longestJob = job;    
                }
                else if (job.LastStableBuildTime > longestJob.LastStableBuildTime)
                {
                    longestJob = job;
                }
            }

            if (longestJob != null)
            {
                var createdAgo = DateTime.Now.Subtract(longestJob.Created).TotalSeconds;

                result = Calculate(createdAgo, longestJob.LastStableBuildTime);
            }

            return result;
        }

        public int Calculate(double score, double maximum)
        {
            int result;

            try
            {
                result = Convert.ToInt32((100 / maximum) * score);
            }
            catch (Exception)
            {
                result = 0;
            }

            return result;
        }
    }
}
