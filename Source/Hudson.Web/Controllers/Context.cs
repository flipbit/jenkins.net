using Hudson.Interfaces;

namespace Hudson.Web.Controllers
{
    /// <summary>
    /// An HTTP context.
    /// </summary>
    public class Context : IContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="IContext"/> requires authentication.
        /// </summary>
        /// <value><c>true</c> if requires authentication; otherwise, <c>false</c>.</value>
        public bool RequireAuthentication
        {
            get
            {
                return !string.IsNullOrEmpty(Username);
            }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
}