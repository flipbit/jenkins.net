using System.Collections.Generic;
using Hudson.Domain;

namespace Hudson.Models.Selectors
{
    public class TextSelector
    {
        public string Select(IList<JobModel> jobs)
        {
            var result = BuildStatus.Unknown;

            var text = "There are no jobs defined on the hudson server.";

            foreach(var job in jobs)
            {
                if (job.BuildStatus <= result) continue;

                result = job.BuildStatus;

                text = job.Comment;
            }

            return text;
        }
    }
}
