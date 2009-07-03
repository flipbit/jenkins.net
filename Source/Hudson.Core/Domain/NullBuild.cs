using System;

namespace Hudson.Domain
{
    /// <summary>
    /// Represents a null <see cref="Build"/>.
    /// </summary>
    public class NullBuild : Build
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullBuild"/> class.
        /// </summary>
        /// <param name="buildDescriptor">The build descriptor.</param>
        public NullBuild(BuildDescriptor buildDescriptor)
        {
            if (buildDescriptor != null)
            {
                Number = buildDescriptor.Number;
                Url = buildDescriptor.Url;            
            }
            else
            {
                Url = new Uri("http://null.build/");
            }

            Description = "Unable to load this build";            
        }
    }
}
