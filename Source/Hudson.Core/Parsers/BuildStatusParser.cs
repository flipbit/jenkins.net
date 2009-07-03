using Hudson.Domain;

namespace Hudson.Parsers
{
    /// <summary>
    /// Parses a string and turns it into a <see cref="BuildStatus"/> enumeration.
    /// </summary>
    public class BuildStatusParser
    {
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static BuildStatus Parse(string input)
        {
            var status = BuildStatus.Unknown;

            if (input != null)
            {
                switch (input.ToLower())
                {
                    case "blue_anime":
                        status = BuildStatus.Building;
                        break;
                    case "red_anime":
                        status = BuildStatus.Building;
                        break;
                    case "blue":
                        status = BuildStatus.Passed;
                        break;
                    case "red":
                        status = BuildStatus.Failed;
                        break;
                    default:
                        status = BuildStatus.Unknown;
                        break;
                }
            }

            return status;
        }
    }
}