// ---------------------------------------------------------------------------------------------------
// <copyright file="OrgResultModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-04</date>
// <summary>
//     The OrgResultModel interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    using Youffer.Resources.Enum;

    /// <summary>
    /// The OrgResultModel class.
    /// </summary>
    public class OrgResultModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The databse id.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the external access token.
        /// </summary>
        /// <value>The external access token.</value>
        public string ExternalAccessToken { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the secondary email.
        /// </summary>
        /// <value>
        /// The secondary email.
        /// </value>
        public string SecondaryEmail { get; set; }

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
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the other phone.
        /// </summary>
        /// <value>
        /// The other phone.
        /// </value>
        public string OtherPhone { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>
        /// The fax.
        /// </value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the annual revenue.
        /// </summary>
        /// <value>
        /// The annual revenue.
        /// </value>
        public double AnnualRevenue { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>The employees.</value>
        public int Employees { get; set; }

        /// <summary>
        /// Gets or sets the country details.
        /// </summary>
        /// <value>
        /// The country details.
        /// </value>
        public CountryModel CountryDetails { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the po box.
        /// </summary>
        /// <value>
        /// The po box.
        /// </value>
        public string POBox { get; set; }

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
        /// Gets or sets the industry.
        /// </summary>
        /// <value>
        /// The industry.
        /// </value>
        public Industry Industry { get; set; }

        /// <summary>
        /// Gets or sets the Main Business Type.
        /// </summary>
        /// <value>The Main Business Type.</value>
        public string[] MainBusinessType { get; set; }

        /// <summary>
        /// Gets or sets the Sub Business Type.
        /// </summary>
        /// <value>The Sub Business Type.</value>
        public string[] SubBusinessType { get; set; }

        /// <summary>
        /// Gets or sets the payment details.
        /// </summary>
        /// <value>
        /// The payment details.
        /// </value>
        public string PaymentDetails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is image uploaded.
        /// </summary>
        public bool IsImageUploaded { get; set; }

        /// <summary>
        /// Gets or sets the cash balance.
        /// </summary>
        public decimal CashBalance { get; set; }

        /// <summary>
        /// Gets or sets the creditbalance.
        /// </summary>
        public decimal CreditBalance { get; set; }

        /// <summary>
        /// Gets or sets the calling code.
        /// </summary>
        public string CallingCode { get; set; }
    }
}
