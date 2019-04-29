namespace FacebookClient
{
    using System;

    /// <summary>
    /// Facebook Options from App settings
    /// </summary>
    public class FacebookOptions
    {
        /// <summary>
        /// Gets or sets the fb graph API URL.
        /// </summary>
        /// <value>
        /// The FB graph API URL.
        /// </value>
        public Uri GraphApiBaseUrl { get; set; }
    }
}