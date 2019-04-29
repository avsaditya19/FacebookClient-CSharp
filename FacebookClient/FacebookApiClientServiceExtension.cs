namespace FacebookClient
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Add <see cref="FacebookApiClient"/>
    /// </summary>
    public static class FacebookApiClientServiceExtension
    {
        /// <summary>
        /// Adds Facebook API Client via Dependency Injection.
        /// Pass the <see cref="Uri"/> Graph API Base Url.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> The ServiceCollection.</param>
        /// <param name="GraphApiBaseUrl"><see cref="Uri"/> The Facebook Marketing Graph API Base Url.</param>
        public static void AddFacebookApiClient(this IServiceCollection services, Uri GraphApiBaseUrl)
        {
            services.Configure<FacebookOptions>(facebookOptions => facebookOptions.GraphApiBaseUrl = GraphApiBaseUrl);
            services.AddSingleton<IFacebookApiClient, FacebookApiClient>();
        }
    }
}
