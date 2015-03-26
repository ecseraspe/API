// ---------------------------------------------------------------------------------------------------
// <copyright file="RefreshTokenDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The RefreshTokenDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System;

    /// <summary>
    /// The RefreshTokenDto class
    /// </summary>
    public class RefreshTokenDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the issued UTC.
        /// </summary>
        /// <value>
        /// The issued UTC.
        /// </value>
        public DateTime IssuedUtc { get; set; }

        /// <summary>
        /// Gets or sets the expires UTC.
        /// </summary>
        /// <value>
        /// The expires UTC.
        /// </value>
        public DateTime ExpiresUtc { get; set; }

        /// <summary>
        /// Gets or sets the protected ticket.
        /// </summary>
        /// <value>
        /// The protected ticket.
        /// </value>
        public string ProtectedTicket { get; set; }
    }
}