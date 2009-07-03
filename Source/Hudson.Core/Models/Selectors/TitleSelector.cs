using System.Collections.Generic;
using Hudson.Domain;

namespace Hudson.Models.Selectors
{
    public class TitleSelector
    {
        public string Select(IList<JobModel> jobs)
        {
            var result = BuildStatus.Unknown;

            var title = "No Jobs Defined";

            foreach(var job in jobs)
            {
                if (job.BuildStatus <= result) continue;

                title = job.Name + " " + job.BuildStatus;

                result = job.BuildStatus;
            }

            return title;
        }
    }
}
