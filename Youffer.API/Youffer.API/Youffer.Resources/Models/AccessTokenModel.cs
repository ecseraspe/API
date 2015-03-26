// ---------------------------------------------------------------------------------------------------
// <copyright file="AccessTokenModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-9</date>
// <summary>
//     The AccessTokenModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    /// <summary>
    /// Class AccessTokenModel
    /// </summary>
    public class AccessTokenModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        /// <value>
        /// The expires in.
        /// </value>
        public string ExpiresIn { get; set; }

        /// <summary>
        /// Gets or sets the issued.
        /// </summary>
        /// <value>
        /// The issued.
        /// </value>
        public string Issued { get; set; }

        /// <summary>
        /// Gets or sets the expires.
        /// </summary>
        /// <value>
        /// The expires.
        /// </value>
        public string Expires { get; set; }
    }
}
