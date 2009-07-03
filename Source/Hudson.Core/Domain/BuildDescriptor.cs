using System;

namespace Hudson.Domain
{
    /// <summary>
    /// Descriptor containing the basic information for a <see cref="Build"/> object.
    /// </summary>
    public class BuildDescriptor
    {
        private int number;

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public int Number
        {
            get { return number; }
            set
            {
                if (Url != null)
                {
                    Url = new Uri(Url.ToString().Replace("/" + number + "/", "/" + value + "/"));
                }

                number = value;
            }
        }

        /// <summary>
        /// Gets or sets the URL of the build.
        /// </summary>
        /// <value>The URL.</value>
        public Uri Url { get; set; }
    }
}
