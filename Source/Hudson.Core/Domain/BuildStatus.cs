namespace Hudson.Domain
{
    /// <summary>
    /// Represents the status of a <see cref="Build"/>
    /// </summary>
    public enum BuildStatus
    {
        /// <summary>
        /// The <see cref="Build"/> is in an unknown state.
        /// </summary>
        Unknown,
        /// <summary>
        /// The <see cref="Build"/> passed.
        /// </summary>
        Passed,
        /// <summary>
        /// The <see cref="Build"/> failed.
        /// </summary>
        Failed,
        /// <summary>
        /// The <see cref="Build"/> is currently building.
        /// </summary>
        Building
    }
}