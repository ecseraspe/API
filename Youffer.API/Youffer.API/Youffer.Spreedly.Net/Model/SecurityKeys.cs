// ---------------------------------------------------------------------------------------------------
// <copyright file="SecurityKeys.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The SecurityKeys class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Model
{
    /// <summary>
    /// The security keys.
    /// </summary>
    internal class SecurityKeys : UsernamePasswordKeys
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityKeys" /> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <param name="redactedTokens">The redacted tokens.</param>
        internal SecurityKeys(string username, string password, string gatewayToken, params string[] redactedTokens) : base(username, password)
        {
            this.LastGatewayToken = gatewayToken;
            this.RedactedTokens = redactedTokens;
        }

        /// <summary>
        /// Gets or sets the last gateway token.
        /// </summary>
        internal string LastGatewayToken { get; set; }

        /// <summary>
        /// Gets or sets the redacted tokens.
        /// </summary>
        internal string[] RedactedTokens { get; set; }
    }
}