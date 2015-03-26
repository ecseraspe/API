// ---------------------------------------------------------------------------------------------------
// <copyright file="GoogleAuthProvider.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-19</date>
// <summary>
//     The GoogleAuthProvider class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Providers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.Google;

    /// <summary>
    /// The GoogleAuthProvider class
    /// </summary>
    public class GoogleAuthProvider : IGoogleOAuth2AuthenticationProvider
    {
        /// <summary>
        /// Called when a Challenge causes a redirect to authorize endpoint in the Google OAuth 2.0 middleware
        /// </summary>
        /// <param name="context">Contains redirect URI and <see cref="T:Microsoft.Owin.Security.AuthenticationProperties" /> of the challenge</param>
        public void ApplyRedirect(GoogleOAuth2ApplyRedirectContext context)
        {
            context.Response.Redirect(context.RedirectUri);
        }

        /// <summary>
        /// Invoked whenever Google succesfully authenticates a user
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="T:System.Security.Claims.ClaimsIdentity" />.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the completed operation.
        /// </returns>
        public Task Authenticated(GoogleOAuth2AuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Invoked prior to the <see cref="T:System.Security.Claims.ClaimsIdentity" /> being saved in a local cookie and the browser being redirected to the originally requested URL.
        /// </summary>
        /// <param name="context">Contains context information and authentication ticket of the return endpoint.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the completed operation.
        /// </returns>
        public Task ReturnEndpoint(GoogleOAuth2ReturnEndpointContext context)
        {
            return Task.FromResult<object>(null);
        }
    }
}