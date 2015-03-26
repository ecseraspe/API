// ---------------------------------------------------------------------------------------------------
// <copyright file="UserModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The UserModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    using System.ComponentModel.DataAnnotations;
    using Youffer.Resources.Enum;

    /// <summary>
    /// The UserModel class
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email id")]
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public string UserRole { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public int Role { get; set; }

        /// <summary>
        /// Gets or sets the type of the main business.
        /// </summary>
        /// <value>
        /// The type of the main business.
        /// </value>
        public string MainBusinessType { get; set; }

        /// <summary>
        /// Gets or sets the type of the sub business.
        /// </summary>
        /// <value>
        /// The type of the sub business.
        /// </value>
        public string SubBusinessType { get; set; }

        /// <summary>
        /// Gets or sets the website URL.
        /// </summary>
        /// <value>
        /// The website URL.
        /// </value>
        public string WebsiteURL { get; set; }

        /// <summary>
        /// Gets or sets the facebook URL.
        /// </summary>
        /// <value>
        /// The facebook URL.
        /// </value>
        public string FacebookURL { get; set; }

        /// <summary>
        /// Gets or sets the google plus URL.
        /// </summary>
        /// <value>
        /// The google plus URL.
        /// </value>
        public string GooglePlusURL { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the GCM Id.
        /// </summary>
        /// <value>The GCM Id.</value> 
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
        /// <value>The OS type.</value> 
        public OSType OSType { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
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
        /// Gets or sets the timezone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; set; }

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