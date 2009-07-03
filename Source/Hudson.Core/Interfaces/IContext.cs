namespace Hudson.Interfaces
{
    /// <summary>
    /// Interface to represent a security context
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="IContext"/> requires authentication.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if requires authentication; otherwise, <c>false</c>.
        /// </value>
        bool RequireAuthentication { get; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        string Password { get; set; }
    }
}
