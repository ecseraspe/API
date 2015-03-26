// ---------------------------------------------------------------------------------------------------
// <copyright file="G2SRequestModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gaurav Barar</author>
// <date>2014-12-28</date>
// <summary>
//     The G2SRequestModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

/// <summary>
/// The DBSchema namespace.
/// </summary>
namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Youffer.Resources.Enum;

    /// <summary>
    /// Class G2SRequest
    /// </summary>
    public class G2SRequestModel
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>The currency.</value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>The name of the item.</value>
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets the item amount.
        /// </summary>
        /// <value>The item amount.</value>
        public string ItemAmount { get; set; }

        /// <summary>
        /// Gets or sets the number of items.
        /// </summary>
        /// <value>The number of items.</value>
        public string NumberOfItems { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        /// <value>The total amount.</value>
        public string TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the item number.
        /// </summary>
        /// <value>The item number.</value>
        public string ItemNumber { get; set; }

        /// <summary>
        /// Gets or sets the item quantity.
        /// </summary>
        /// <value>The item quantity.</value>
        public string ItemQuantity { get; set; }

        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        /// <value>The encoding.</value>
        public string Encoding { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        public string TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        /// <value>The invoice identifier.</value>
        public string InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the success URL.
        /// </summary>
        /// <value>The success URL.</value>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// Gets or sets the error URL.
        /// </summary>
        /// <value>The error URL.</value>
        public string ErrorUrl { get; set; }

        /// <summary>
        /// Gets or sets the pending URL.
        /// </summary>
        /// <value>The pending URL.</value>
        public string PendingUrl { get; set; }

        /// <summary>
        /// Gets or sets the notify URL.
        /// </summary>
        /// <value>The notify URL.</value>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>The client identifier.</value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client interest.
        /// </summary>
        /// <value>The client interest.</value>
        public string ClientInterest { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

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
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>The zip.</value>
        public string Zip { get; set; }
    }
}
