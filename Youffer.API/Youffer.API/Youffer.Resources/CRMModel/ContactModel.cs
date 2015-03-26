// ---------------------------------------------------------------------------------------------------
// <copyright file="ContactModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-19</date>
// <summary>
//     The ContactModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.CRMModel
{
    using System;
    using Youffer.Resources.Enum;

    /// <summary>
    /// The ContactModel class
    /// </summary>
    public class ContactModel : BaseCrmModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactModel" /> class.
        /// </summary>
        public ContactModel()
        {
            this.MainInterest = new string[] { };
            this.SubInterest = null;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The databse id.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
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
        /// <value>The OS type.</value>
        public OSType OSType { get; set; }

        /// <summary>
        /// Gets or sets the timezone.
        /// </summary>
        /// <value>The timezone.</value>
        public string Timezone { get; set; }

        /// <summary>
        /// Gets or sets the salutationtype.
        /// </summary>
        /// <value>The type of the salutation.</value>
        public string SalutationType { get; set; }

        /// <summary>
        /// Gets or sets the homephone.
        /// </summary>
        /// <value>The home phone.</value>
        public string HomePhone { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the mobilephone.
        /// </summary>
        /// <value>The mobile phone.</value>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Gets or sets the accountid.
        /// </summary>
        /// <value>The account identifier.</value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the leadsource.
        /// </summary>
        /// <value>The lead source.</value>
        public LeadSource LeadSource { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>The fax.</value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>The department.</value>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        /// <value>The birthday.</value>
        public string Birthday { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the contact identifier.
        /// </summary>
        /// <value>The contact identifier.</value>
        public string ContactId { get; set; }

        /// <summary>
        /// Gets or sets the assistant.
        /// </summary>
        /// <value>The assistant.</value>
        public string Assistant { get; set; }

        /// <summary>
        /// Gets or sets the yahoo identifier.
        /// </summary>
        /// <value>The yahoo identifier.</value>
        public string YahooId { get; set; }

        /// <summary>
        /// Gets or sets the assistant phone.
        /// </summary>
        /// <value>The assistant phone.</value>
        public string AssistantPhone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [do not call].
        /// </summary>
        /// <value><c>true</c> if [do not call]; otherwise, <c>false</c>.</value>
        public bool DoNotCall { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [email opt out].
        /// </summary>
        /// <value><c>true</c> if [email opt out]; otherwise, <c>false</c>.</value>
        public bool EmailOptOut { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ContactModel"/> is reference.
        /// </summary>
        /// <value><c>true</c> if reference; otherwise, <c>false</c>.</value>
        public bool Reference { get; set; }

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
        /// Gets or sets a value indicating whether this <see cref="ContactModel"/> is portal.
        /// </summary>
        /// <value><c>true</c> if portal; otherwise, <c>false</c>.</value>
        public bool Portal { get; set; }

        /// <summary>
        /// Gets or sets the support start date.
        /// </summary>
        /// <value>The support start date.</value>
        public string SupportStartDate { get; set; }

        /// <summary>
        /// Gets or sets the support end date.
        /// </summary>
        /// <value>The support end date.</value>
        public string SupportEndDate { get; set; }

        /// <summary>
        /// Gets or sets the mailing street.
        /// </summary>
        /// <value>The mailing street.</value>
        public string MailingStreet { get; set; }

        /// <summary>
        /// Gets or sets the other street.
        /// </summary>
        /// <value>The other street.</value>
        public string OtherStreet { get; set; }

        /// <summary>
        /// Gets or sets the mailing city.
        /// </summary>
        /// <value>The mailing city.</value>
        public string MailingCity { get; set; }

        /// <summary>
        /// Gets or sets the other city.
        /// </summary>
        /// <value>The other city.</value>
        public string OtherCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the mailing.
        /// </summary>
        /// <value>The state of the mailing.</value>
        public string MailingState { get; set; }

        /// <summary>
        /// Gets or sets the state of the other.
        /// </summary>
        /// <value>The state of the other.</value>
        public string OtherState { get; set; }

        /// <summary>
        /// Gets or sets the mailing zip.
        /// </summary>
        /// <value>The mailing zip.</value>
        public string MailingZip { get; set; }

        /// <summary>
        /// Gets or sets the other zip.
        /// </summary>
        /// <value>The other zip.</value>
        public string OtherZip { get; set; }

        /// <summary>
        /// Gets or sets the mailing country.
        /// </summary>
        /// <value>The mailing country.</value>
        public string MailingCountry { get; set; }

        /// <summary>
        /// Gets or sets the other country.
        /// </summary>
        /// <value>The other country.</value>
        public string OtherCountry { get; set; }

        /// <summary>
        /// Gets or sets the mailing po box.
        /// </summary>
        /// <value>The mailing po box.</value>
        public string MailingPOBox { get; set; }

        /// <summary>
        /// Gets or sets the other po box.
        /// </summary>
        /// <value>The other po box.</value>
        public string OtherPOBox { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
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
        /// Gets or sets the FB Url.
        /// </summary>
        public string FBUrl { get; set; }

        /// <summary>
        /// Gets or sets the google plus URL.
        /// </summary>
        /// <value>
        /// The google plus URL.
        /// </value>
        public string GooglePlusURL { get; set; }

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
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the payment mode
        /// </summary>
        public PaymentMode PaymentMode { get; set; }

        /// <summary>
        /// Gets or sets the paypal id.
        /// </summary>
        public string PaypalId { get; set; }

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
    }
}
