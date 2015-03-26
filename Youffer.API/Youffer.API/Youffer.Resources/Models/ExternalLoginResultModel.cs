// ---------------------------------------------------------------------------------------------------
// <copyright file="ExternalLoginResultModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-25</date>
// <summary>
//     The ExternalLoginResultModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    /// <summary>
    /// Class ExternalLoginResultModel.
    /// </summary>
    public class ExternalLoginResultModel
    {
        /// <summary>
        /// Gets or sets the external access token.
        /// </summary>
        public string ExternalAccessToken { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ExternalLoginResultModel"/> is has local account.
        /// </summary>
        public bool HasLocalAccount { get; set; }

        /// <summary>
        /// Gets or sets the name of the external user.
        /// </summary>
        public string ExternalUserName { get; set; }
    }
}
