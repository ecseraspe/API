// ---------------------------------------------------------------------------------------------------
// <copyright file="FacebookAuthProvider.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-19</date>
// <summary>
//     The FacebookAuthProvider class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Providers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.Facebook;

    /// <summary>
    /// The FacebookAuthProvider class
    /// </summary>
    public class FacebookAuthProvider : FacebookAuthenticationProvider
    {
        /// <summary>
        /// Invoked whenever Facebook succesfully authenticates a user
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="T:System.Security.Claims.ClaimsIdentity" />.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the completed operation.
        /// </returns>
        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            return Task.FromResult<object>(null);
        }
    }
}