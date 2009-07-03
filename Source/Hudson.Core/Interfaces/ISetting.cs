namespace Hudson.Interfaces
{
    /// <summary>
    /// Represents a configuration setting
    /// </summary>
    public interface ISetting
    {
        /// <summary>
        /// Gets the value of the setting.
        /// </summary>
        /// <value>The value.</value>
        string Value { get;  }
    }
}
