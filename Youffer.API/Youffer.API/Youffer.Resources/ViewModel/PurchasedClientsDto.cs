// ---------------------------------------------------------------------------------------------------
// <copyright file="PurchasedClientsDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-12</date>
// <summary>
//     The PurchasedClientsDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using Youffer.Resources.Models;

    /// <summary>
    /// Class PurchasedClientsDto
    /// </summary>
    public class PurchasedClientsDto
    {
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageURL { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the birth day.
        /// </summary>
        /// <value>
        /// The birth day.
        /// </value>
        public string BirthDay { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public decimal Rank { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public decimal Rating { get; set; }

        /// <summary>
        /// Gets or sets the review.
        /// </summary>
        /// <value>
        /// The review.
        /// </value>
        public string Review { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the interest.
        /// </summary>
        /// <value>
        /// The interest.
        /// </value>
        public string Interest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is available.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is available; otherwise, <c>false</c>.
        /// </value>
        public bool? IsAvailable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is online.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is online; otherwise, <c>false</c>.
        /// </value>        
        public bool IsOnline { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can call.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can call; otherwise, <c>false</c>.
        /// </value>
        public bool CanCall { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [mark purchased].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [mark purchased]; otherwise, <c>false</c>.
        /// </value>
        public bool MarkPurchased { get; set; }

        /// <summary>
        /// Gets or sets the country det.
        /// </summary>
        /// <value>
        /// The country det.
        /// </value>
        public CountryModel CountryDet { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }
    }
}
