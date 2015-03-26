// ---------------------------------------------------------------------------------------------------
// <copyright file="CreditCardInfo.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The CreditCardInfo class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Spreedly
{
    /// <summary>
    /// The credit card info.
    /// </summary>
    public class CreditCardInfo
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardInfo"/> class.
        /// </summary>
        public CreditCardInfo()
        {
            this.FirstName = this.FullName = this.LastName = string.Empty;
            this.State = this.City = this.Country = this.ZipCode = this.PhNumber = string.Empty;
            this.Address1 = this.Address2 = string.Empty;
            this.CreditCardYear = this.CrediCardCVV = this.CreditCardMonth = 0;
            this.CreditCardNumber = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the address 1.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the credit card CVV.
        /// </summary>
        public int CrediCardCVV { get; set; }

        /// <summary>
        /// Gets or sets the credit card month.
        /// </summary>
        public int CreditCardMonth { get; set; }

        /// <summary>
        /// Gets or sets the credit card number.
        /// </summary>
        public string CreditCardNumber { get; set; }

        /// <summary>
        /// Gets or sets the credit card year.
        /// </summary>
        public int CreditCardYear { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the ph number.
        /// </summary>
        public string PhNumber { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        public string ZipCode { get; set; }

        #endregion
    }
}