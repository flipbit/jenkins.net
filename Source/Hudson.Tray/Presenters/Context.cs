using Hudson.Interfaces;
using Hudson.Tray.Properties;

namespace Hudson.Tray.Presenters
{
    /// <summary>
    /// An HTTP context.
    /// </summary>
    public class Context : IContext 
    {
        private bool Refresh()
        {
            Settings.Default.Reload();

            Username = Settings.Default.Username;
            Password = Settings.Default.Password;

            return !string.IsNullOrEmpty(Username);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public Context()
        {
            Refresh();
        }


        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="IContext"/> requires authentication.
        /// </summary>
        /// <value><c>true</c> if requires authentication; otherwise, <c>false</c>.</value>
        public bool RequireAuthentication
        {
            get
            {
                return Refresh();
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