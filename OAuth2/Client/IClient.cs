using System.Collections.Specialized;
using OAuth2.Configuration;
using OAuth2.Models;

namespace OAuth2.Client
{
    /// <summary>
    /// Defines API for doing user authentication using certain third-party service.
    /// </summary>
    /// <remarks>
    /// Standard flow is:
    /// - client is used to generate login link (<see cref="GetLoginLinkUri"/>)
    /// - hosting app renders page with generated login link
    /// - user clicks login link - this leads to redirect to third-party service site
    /// - user authenticates and allows app access their basic information
    /// - third-party service redirects user to hosting app
    /// - hosting app reads user information using <see cref="GetUserInfo"/> method
    /// </remarks>
    public interface IClient
    {
        /// <summary>
        /// Friendly name of provider (third-party authentication service). 
        /// Defined by client implementation developer and supposed to be unique.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns URI of service which should be called in order to start authentication process. 
        /// You should use this URI when rendering login link.
        /// </summary>
        string GetLoginLinkUri(string state = null);

        /// <summary>
        /// Returns URI of service which should be called in order to start authentication process.
        /// This will generate a redirect login url based on the x-forwarded-host passed in.
        /// </summary>
        string GetCustomDomainLoginLinkUri(bool isSecure, string redirectDomain, string state = null);

        /// <summary>
        /// State which was posted as additional parameter 
        /// to service and then received along with main answer.
        /// </summary>
        string State { get; }

        /// <summary>
        /// Obtains user information using third-party authentication service 
        /// using data provided via callback request.
        /// </summary>
        /// <param name="parameters">
        /// Callback request payload (parameters).
        /// <example>Request.QueryString</example>
        /// </param>
        UserInfo GetUserInfo(NameValueCollection parameters);

        /// <summary>
        /// Obtains user information using OAuth2 service and data provided via callback request.
        /// Use case is for customers with custom domains (i.e. Whitelabel)
        /// </summary>
        /// <returns>The user info for custom domain.</returns>
        /// <param name="parameters">Query Parameters.</param>
        /// <param name="isSecure">Specifies whether or not the request is https or not.</param>
        /// <param name="customDomain">Custom domain for whitelabel company.</param>
        UserInfo GetCustomDomainUserInfo(NameValueCollection parameters, bool isSecure, string customDomain);

        /// <summary>
        /// Client configuration object.
        /// </summary>
        IClientConfiguration Configuration { get; }
    }
}
