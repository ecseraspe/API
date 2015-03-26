// ---------------------------------------------------------------------------------------------------
// <copyright file="OrganisationModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-19</date>
// <summary>
//     The OrganisationModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.CRMModel
{
    using System;
    using Youffer.Resources.Enum;

    /// <summary>
    /// The OrganisationModel class
    /// </summary>
    public class OrganisationModel : BaseCrmModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The databse id.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        /// <value>The name of the account.</value>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>The account number.</value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>The website.</value>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>The fax.</value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the ticker symbol.
        /// </summary>
        /// <value>The ticker symbol.</value>
        public string TickerSymbol { get; set; }

        /// <summary>
        /// Gets or sets the other phone.
        /// </summary>
        /// <value>The other phone.</value>
        public string OtherPhone { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>The account identifier.</value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the email1.
        /// </summary>
        /// <value>The email1.</value>
        public string Email1 { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>The employees.</value>
        public int Employees { get; set; }

        /// <summary>
        /// Gets or sets the email2.
        /// </summary>
        /// <value>The email2.</value>
        public string Email2 { get; set; }

        /// <summary>
        /// Gets or sets the ownership.
        /// </summary>
        /// <value>The ownership.</value>
        public string Ownership { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>The rating.</value>
        public Rating Rating { get; set; }

        /// <summary>
        /// Gets or sets the industry.
        /// </summary>
        /// <value>The industry.</value>
        public Industry Industry { get; set; }

        /// <summary>
        /// Gets or sets the sic code.
        /// </summary>
        /// <value>The sic code.</value>
        public string SicCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the account.
        /// </summary>
        /// <value>The type of the account.</value>
        public AccountType AccountType { get; set; }

        /// <summary>
        /// Gets or sets the annual revenue.
        /// </summary>
        /// <value>The annual revenue.</value>
        public double AnnualRevenue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [email opt out].
        /// </summary>
        /// <value><c>true</c> if [email opt out]; otherwise, <c>false</c>.</value>
        public bool EmailOptOut { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [notify owner].
        /// </summary>
        /// <value><c>true</c> if [notify owner]; otherwise, <c>false</c>.</value>
        public bool NotifyOwner { get; set; }

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
        /// Gets or sets the bill address.
        /// </summary>
        /// <value>
        /// The bill address.
        /// </value>
        public string BillAddress { get; set; }

        /// <summary>
        /// Gets or sets the bill street.
        /// </summary>
        /// <value>The bill street.</value>
        public string BillStreet { get; set; }

        /// <summary>
        /// Gets or sets the ship street.
        /// </summary>
        /// <value>The ship street.</value>
        public string ShipStreet { get; set; }

        /// <summary>
        /// Gets or sets the bill city.
        /// </summary>
        /// <value>The bill city.</value>
        public string BillCity { get; set; }

        /// <summary>
        /// Gets or sets the ship city.
        /// </summary>
        /// <value>The ship city.</value>
        public string ShipCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the bill.
        /// </summary>
        /// <value>The state of the bill.</value>
        public string BillState { get; set; }

        /// <summary>
        /// Gets or sets the state of the ship.
        /// </summary>
        /// <value>The state of the ship.</value>
        public string ShipState { get; set; }

        /// <summary>
        /// Gets or sets the bill code.
        /// </summary>
        /// <value>The bill code.</value>
        public string BillCode { get; set; }

        /// <summary>
        /// Gets or sets the ship code.
        /// </summary>
        /// <value>The ship code.</value>
        public string ShipCode { get; set; }

        /// <summary>
        /// Gets or sets the bill country.
        /// </summary>
        /// <value>The bill country.</value>
        public string BillCountry { get; set; }

        /// <summary>
        /// Gets or sets the ship country.
        /// </summary>
        /// <value>The ship country.</value>
        public string ShipCountry { get; set; }

        /// <summary>
        /// Gets or sets the bill po box.
        /// </summary>
        /// <value>The bill po box.</value>
        public string BillPOBox { get; set; }

        /// <summary>
        /// Gets or sets the ship po box.
        /// </summary>
        /// <value>The ship po box.</value>
        public string ShipPOBox { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

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
        /// Gets or sets the timezone.
        /// </summary>
        /// <value>
        /// The timezone.
        /// </value>
        public string Timezone { get; set; }

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
        /// <value>
        /// <c>true</c> if this instance is image uploaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsImageUploaded { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the cash balance.
        /// </summary>
        public decimal CashBalance { get; set; }

        /// <summary>
        /// Gets or sets the creditbalance.
        /// </summary>
        public decimal CreditBalance { get; set; }

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; set; }
    }
}
