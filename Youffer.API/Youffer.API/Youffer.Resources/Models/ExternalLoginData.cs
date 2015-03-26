// ---------------------------------------------------------------------------------------------------
// <copyright file="ExternalLoginData.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-19</date>
// <summary>
//     The ExternalLoginData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    using System.Security.Claims;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The ExternalLoginData class
    /// </summary>
    public class ExternalLoginData
    {
        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        public string ProviderKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the external access token.
        /// </summary>
        public string ExternalAccessToken { get; set; }

        /// <summary>
        /// Froms the identity.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns>ExternalLoginData object</returns>
        public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (providerKeyClaim == null || string.IsNullOrEmpty(providerKeyClaim.Issuer) || string.IsNullOrEmpty(providerKeyClaim.Value))
            {
                return null;
            }

            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
                return null;
            }

            return new ExternalLoginData
            {
                LoginProvider = providerKeyClaim.Issuer,
                ProviderKey = providerKeyClaim.Value,
                UserName = identity.FindFirstValue(ClaimTypes.Name),
                ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken"),
            };
        }
    }
}
