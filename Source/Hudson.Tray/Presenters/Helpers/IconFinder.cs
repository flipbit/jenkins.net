using System.Drawing;
using Hudson.Domain;
using Hudson.Tray.Properties;

namespace Hudson.Tray.Presenters.Helper
{
    /// <summary>
    /// Finds the icon used to represent a <see cref="BuildStatus"/>.
    /// </summary>
    public class IconFinder
    {
        /// <summary>
        /// Finds the icon for the specified build status.
        /// </summary>
        /// <param name="buildStatus">The build status.</param>
        /// <returns></returns>
        public Icon Find(BuildStatus buildStatus)
        {
            Icon icon;

            switch (buildStatus)
            {
                case BuildStatus.Building:
                    icon = Resources.Yellow;
                    break;

                case BuildStatus.Failed:
                    icon = Resources.Red;
                    break;

                case BuildStatus.Passed:
                    icon = Resources.Blue;
                    break;

                default:
                    icon = Resources.Grey;
                    break;
            }

            return icon;
        }
    }
}
