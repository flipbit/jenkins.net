using System.IO;
using System.Web.Mvc;
using Hudson.Web.Models;

namespace Hudson.Web.Helpers
{
    /// <summary>
    /// Displays an image for the given user
    /// </summary>
    public static class UserImageExtension
    {
        /// <summary>
        /// Displays the image for the user.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static string UserImage(this HtmlHelper<BuildModel> html, string userName)
        {
            var image = "unknown";

            if (File.Exists(html.ViewContext.HttpContext.Server.MapPath(@"~\images\" + userName + ".png")))
            {
                image = userName;
            }

            return image;
        }
    }
}
