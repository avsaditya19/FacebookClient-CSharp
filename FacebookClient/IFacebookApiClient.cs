namespace FacebookClient
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Facebook API Client
    /// </summary>
    public interface IFacebookApiClient
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="accessToken">The access token.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="argsList">The list of Fields needed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> GetAsync<T>(string accessToken, string endpoint, IEnumerable<string> argsList);

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="accessToken">The access token.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="data">The data.</param>
        /// <param name="argsList">The list of Fields needed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> PostAsync<T>(string accessToken, string endpoint, object data, IEnumerable<string> argsList);
    }
}