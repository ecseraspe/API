// ---------------------------------------------------------------------------------------------------
// <copyright file="AuthClients.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The AuthClients class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Youffer.Resources.Enum;

    /// <summary>
    /// The Client class
    /// </summary>
    public class AuthClients
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthClients"/> class.
        /// </summary>
        public AuthClients()
        {
            this.CreatedOn = DateTime.UtcNow;
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
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        [Required]
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the application.
        /// </summary>
        /// <value>
        /// The type of the application.
        /// </value>
        public ApplicationTypes ApplicationType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AuthClients"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the refresh token life time.
        /// </summary>
        /// <value>
        /// The refresh token life time.
        /// </value>
        public int RefreshTokenLifeTime { get; set; }

        /// <summary>
        /// Gets or sets the allowed origin.
        /// </summary>
        /// <value>
        /// The allowed origin.
        /// </value>
        [MaxLength(100)]
        public string AllowedOrigin { get; set; }
    }
}