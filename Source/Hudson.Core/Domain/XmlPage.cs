using System;
using System.Xml;

namespace Hudson.Domain
{
    /// <summary>
    /// Represents a Page downloaded via the HTTP protocol from the internet.
    /// </summary>
    public class XmlPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlPage"/> class.
        /// </summary>
        public XmlPage(Uri url)
        {
            Url = url;

            IsValid = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlPage"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="xml">The XML.</param>
        public XmlPage(Uri url, string xml)
        {
            Contents = new XmlDocument();

            try
            {
                Contents.LoadXml(xml);

                IsValid = true;
            }
            catch (XmlException)
            {
                IsValid = false;
            }
            
            Url = url;            
        }

        public Uri Url { get; private set; }

        /// <summary>
        /// Gets or sets the contents.
        /// </summary>
        /// <value>The contents.</value>
        public XmlDocument Contents { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="XmlPage"/> is valid.
        /// </summary>
        /// <value><c>true</c> if valid; otherwise, <c>false</c>.</value>
        public bool IsValid { get; private set; }
    }
}