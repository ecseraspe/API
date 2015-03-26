// ---------------------------------------------------------------------------------------------------
// <copyright file="ClientDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The ClientDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using Youffer.Resources.Enum;

    /// <summary>
    /// The ClientDto class
    /// </summary>
    public class ClientDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the application.
        /// </summary>
        /// <value>
        /// The type of the application.
        /// </value>
        public ApplicationTypes ApplicationType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ClientDto"/> is active.
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
        public string AllowedOrigin { get; set; }
    }
}