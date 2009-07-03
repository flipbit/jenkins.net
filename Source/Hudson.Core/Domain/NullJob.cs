namespace Hudson.Domain
{
    /// <summary>
    /// Represents a null Job
    /// </summary>
    public class NullJob : Job
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullJob"/> class.
        /// </summary>
        /// <param name="jobDescriptor">The job descriptor.</param>
        public NullJob(JobDescriptor jobDescriptor)
        {
            Url = jobDescriptor.Url;
            Name = jobDescriptor.Name;
            Status = jobDescriptor.Status;
            Description = "Unable to load this job";
        }
    }
}
