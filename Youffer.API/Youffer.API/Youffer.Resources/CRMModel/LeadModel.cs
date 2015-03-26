// ---------------------------------------------------------------------------------------------------
// <copyright file="LeadModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-19</date>
// <summary>
//     The LeadModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.CRMModel
{
    using System;
    using Youffer.Resources.Enum;

    /// <summary>
    /// The LeadModel class
    /// </summary>
    public class LeadModel : BaseCrmModel
    {
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
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the type of the salutation.
        /// </summary>
        /// <value>The type of the salutation.</value>
        public string SalutationType { get; set; }

        /// <summary>
        /// Gets or sets the lead number.
        /// </summary>
        /// <value>The lead number.</value>
        public string LeadNumber { get; set; }

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        /// <value>The birthday.</value>
        public string Birthday { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>The mobile.</value>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>The fax.</value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the designation.
        /// </summary>
        /// <value>The designation.</value>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the lead source.
        /// </summary>
        /// <value>The lead source.</value>
        public LeadSource LeadSource { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>The website.</value>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the industry.
        /// </summary>
        /// <value>The industry.</value>
        public Industry Industry { get; set; }

        /// <summary>
        /// Gets or sets the lead status.
        /// </summary>
        /// <value>The lead status.</value>
        public LeadStatus LeadStatus { get; set; }

        /// <summary>
        /// Gets or sets the annual revenue.
        /// </summary>
        /// <value>The annual revenue.</value>
        public double AnnualRevenue { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>The rating.</value>
        public Rating Rating { get; set; }

        /// <summary>
        /// Gets or sets the number of employees.
        /// </summary>
        /// <value>The number of employees.</value>
        public int NumberOfEmployees { get; set; }

        /// <summary>
        /// Gets or sets the yahoo identifier.
        /// </summary>
        /// <value>The yahoo identifier.</value>
        public string YahooId { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>The created time.</value>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the modified time.
        /// </summary>
        /// <value>The modified time.</value>
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the lane.
        /// </summary>
        /// <value>The lane.</value>
        public string Lane { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the po box.
        /// </summary>
        /// <value>The po box.</value>
        public string POBox { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive.
        /// </summary>
        /// <value>The is active.</value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets OwnerContactId.
        /// </summary>
        /// <value>The OwnerContactId.</value>
        public string OwnerContactId { get; set; }

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
        /// Gets or sets the facebook URL.
        /// </summary>
        /// <value>The facebook URL.</value>
        public string FacebookURL { get; set; }

        /// <summary>
        /// Gets or sets the google plus URL.
        /// </summary>
        /// <value>
        /// The google plus URL.
        /// </value>
        public string GooglePlusURL { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageURL { get; set; }

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
        public bool IsAvailable { get; set; }

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
        /// Gets or sets the timezone.
        /// </summary>
        /// <value>
        /// The timezone.
        /// </value>
        public string Timezone { get; set; }

        /// <summary>
        /// Gets or sets the payment mode
        /// </summary>
        public PaymentMode PaymentMode { get; set; }

        /// <summary>
        /// Gets or sets the paypal id.
        /// </summary>
        public string PaypalId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user blocked.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is user blocked; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserBlocked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is online.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is online; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnline { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the purchased with credit count.
        /// </summary>
        public int PurchasedWithCreditCount { get; set; }
    }
}
