using System;

namespace Hudson.Domain
{
    /// <summary>
    /// A build of a job in hudson
    /// </summary>
    public class Build : BuildDescriptor
    {
        /// <summary>
        /// Gets or sets the revision.
        /// </summary>
        /// <value>The revision.</value>
        public string Revision { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Build"/> is building.
        /// </summary>
        /// <value><c>true</c> if building; otherwise, <c>false</c>.</value>
        public bool Building { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public long Duration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [keep log].
        /// </summary>
        /// <value><c>true</c> if [keep log]; otherwise, <c>false</c>.</value>
        public bool KeepLog { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Build"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the full name of the display.
        /// </summary>
        /// <value>The full name of the display.</value>
        public string FullDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the user who started this build.
        /// </summary>
        /// <value>The user.</value>
        public string User { get; set; }
    }
}