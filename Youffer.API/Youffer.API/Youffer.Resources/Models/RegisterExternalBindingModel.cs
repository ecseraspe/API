// ---------------------------------------------------------------------------------------------------
// <copyright file="RegisterExternalBindingModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-19</date>
// <summary>
//     The RegisterExternalBindingModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    using System.ComponentModel.DataAnnotations;
    using Youffer.Resources.Enum;

    /// <summary>
    /// The RegisterExternalBindingModel class
    /// </summary>
    public class RegisterExternalBindingModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary> 
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        [Required]
        public string Provider { get; set; }

        /// <summary>
        /// Gets or sets the external access token.
        /// </summary>
        [Required]
        public string ExternalAccessToken { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>The user role.</value>
        public Roles UserRole { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the request id from Mobile device or not
        /// </summary>
        public bool IsFromMobile { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the facebook Id.
        /// </summary>
        public string FacebookId { get; set; }

        /// <summary>
        /// Gets or sets the google plus identifier.
        /// </summary>
        public string GooglePlusId { get; set; }

        /// <summary>
        /// Gets or sets the gcm Id.
        /// </summary>
        public string GCMId { get; set; }

        /// <summary>
        /// Gets or sets the ud identifier.
        /// </summary>
        /// <value>
        /// The ud identifier.
        /// </value>
        public string UDId { get; set; }

        /// <summary>
        /// Gets or sets the OS type.
        /// </summary>
        public OSType OSType { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the timezone.
        /// </summary>
        public string Timezone { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        [Required]
        public int Role { get; set; }

        /// <summary>
        /// Gets or sets the os.
        /// </summary>
        /// <value>The os.</value>
        public int OS { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }
    }
}