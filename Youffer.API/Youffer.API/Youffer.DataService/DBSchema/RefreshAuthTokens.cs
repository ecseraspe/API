// ---------------------------------------------------------------------------------------------------
// <copyright file="RefreshAuthTokens.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The RefreshAuthTokens class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The RefreshToken class
    /// </summary>
    public class RefreshAuthTokens
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshAuthTokens"/> class.
        /// </summary>
        public RefreshAuthTokens()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Gets the created on.
        /// </summary>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        [Required]
        [MaxLength(50)]
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
        [Required]
        public string ProtectedTicket { get; set; }
    }
}