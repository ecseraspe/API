// ---------------------------------------------------------------------------------------------------
// <copyright file="UserResultModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-28</date>
// <summary>
//     The UserResultModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    using System;
    using System.Collections.Generic;
    using Youffer.Resources.Enum;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Class UserResultModel.
    /// </summary>
    public class UserResultModel
    {
        /// <summary>
        /// Gets or sets the external access token.
        /// </summary>
        /// <value>The external access token.</value>
        public AccessTokenModel ExternalAccessToken { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The databse id.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        /// <value>The birthday.</value>
        public string Birthday { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country details.
        /// </summary>
        /// <value>
        /// The country details.
        /// </value>
        public CountryModel CountryDetails { get; set; }

        /// <summary>
        /// Gets or sets the timezone.
        /// </summary>
        /// <value>The timezone.</value>
        public string Timezone { get; set; }

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
        /// Gets or sets the image url.
        /// </summary>
        /// <value>The gcm id.</value>
        public string ImageURL { get; set; }

        /// <summary>
        /// Gets or sets the gcm id.
        /// </summary>
        /// <value>The gcm id.</value>
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
        /// Gets or sets the user role.
        /// </summary>
        public Roles UserRole { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        public decimal Rank { get; set; }

        /// <summary>
        /// Gets or sets the bio.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Main Interest.
        /// </summary>
        /// <value>The Main Interest.</value>
        public string[] MainInterest { get; set; }

        /// <summary>
        /// Gets or sets the Sub Interest.
        /// </summary>
        /// <value>The Sub Interest.</value>
        public string[] SubInterest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsAvailable or not.
        /// </summary>
        public bool? IsAvailable { get; set; }

        /// <summary>
        /// Gets or sets the available from.
        /// </summary>
        /// <value>The available from.</value>
        public string AvailableFrom { get; set; }

        /// <summary>
        /// Gets or sets the available to.
        /// </summary>
        /// <value>The available to.</value>
        public string AvailableTo { get; set; }

        /// <summary>
        /// Gets or sets the availability.
        /// </summary>
        /// <value>The availability.</value>
        public Availability Availability { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the Payment details
        /// </summary>
        public PaymentModelDto PaymentDetails { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the user reviews.
        /// </summary>
        /// <value>
        /// The user reviews.
        /// </value>
        public List<UserReviewsDto> UserReviews { get; set; }

        /// <summary>
        /// Gets or sets the Created on
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can call.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can call; otherwise, <c>false</c>.
        /// </value>
        public bool CanCall { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is online.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is online; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnline { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [mark purchased].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [mark purchased]; otherwise, <c>false</c>.
        /// </value>
        public bool MarkPurchased { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        public string CountryCode { get; set; }
    }
}
