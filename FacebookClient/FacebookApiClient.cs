namespace FacebookClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    /// <summary>
    /// Facebook Client
    /// </summary>
    /// <seealso cref="IFacebookClient" />
    public class FacebookApiClient : IFacebookApiClient, IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposedValue; // To detect redundant calls

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookClient"/> class.
        /// </summary>
        /// <param name="facebookOptions">Facebook Options from Config File</param>
        public FacebookApiClient(IOptions<FacebookOptions> facebookOptions)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = facebookOptions.Value.GraphApiBaseUrl
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region IDisposable Support

        /// <summary>
        /// Finalizes an instance of the <see cref="FacebookClient"/> class.
        /// override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ///
        /// </summary>
        ~FacebookApiClient()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// This code added to correctly implement the disposable pattern.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="accessToken">The access token.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="argsList">The list of Fields needed.</param>
        /// <returns>Object of Type T</returns>
        public async Task<T> GetAsync<T>(string accessToken, string endpoint, IEnumerable<string> argsList)
        {
            var args = argsList != null && argsList.ToList().Any()
                ? $"fields={string.Join(",", argsList)}"
                : string.Empty;

            var response = await _httpClient.GetAsync(new Uri($"/{endpoint}?access_token={accessToken}&{args}", UriKind.Relative))
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return default(T);
            }

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="accessToken">The access token.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="data">The data.</param>
        /// <param name="argsList">The list of Fields needed.</param>
        /// <returns>Object of Type T</returns>
        public async Task<T> PostAsync<T>(string accessToken, string endpoint, object data, IEnumerable<string> argsList)
        {
            var args = argsList != null && argsList.ToList().Any()
                ? $"fields={string.Join(",", argsList)}"
                : string.Empty;

            var payload = GetPayload(data);
            var response = await _httpClient.PostAsync(new Uri($"/{endpoint}?access_token={accessToken}&{args}", UriKind.Relative), payload)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return default(T);
            }

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // free unmanaged resources (unmanaged objects) and override a finalizer below.
            // set large fields to null.
            _httpClient?.Dispose();
            _disposedValue = true;
        }

        /// <summary>
        /// Gets the payload.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Payload</returns>
        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}