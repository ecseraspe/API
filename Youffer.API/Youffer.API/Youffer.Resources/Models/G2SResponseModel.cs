// ---------------------------------------------------------------------------------------------------
// <copyright file="G2SResponseModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gaurav Barar</author>
// <date>2014-12-28</date>
// <summary>
//     The G2SResponseModel class
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
    /// Class G2SResponse
    /// </summary>
    public class G2SResponseModel
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the PPP status.
        /// </summary>
        /// <value>The PPP status.</value>
        public string PPPStatus { get; set; }

        /// <summary>
        /// Gets or sets the card company.
        /// </summary>
        /// <value>The card company.</value>
        public string CardCompany { get; set; }

        /// <summary>
        /// Gets or sets the name on card.
        /// </summary>
        /// <value>The name on card.</value>
        public string NameOnCard { get; set; }

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
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>The zip.</value>
        public string Zip { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>The currency.</value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the interest.
        /// </summary>
        /// <value>The interest.</value>
        public string Interest { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the merchant site identifier.
        /// </summary>
        /// <value>The merchant site identifier.</value>
        public string MerchantSiteId { get; set; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>The merchant identifier.</value>
        public string MerchantId { get; set; }

        /// <summary>
        /// Gets or sets the merchant locale.
        /// </summary>
        /// <value>The merchant locale.</value>
        public string MerchantLocale { get; set; }

        /// <summary>
        /// Gets or sets the request version.
        /// </summary>
        /// <value>The request version.</value>
        public string RequestVersion { get; set; }

        /// <summary>
        /// Gets or sets the PPP transaction identifier.
        /// </summary>
        /// <value>The PPP transaction identifier.</value>
        public string PPPTransactionID { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the custom data.
        /// </summary>
        /// <value>The custom data.</value>
        public string CustomData { get; set; }

        /// <summary>
        /// Gets or sets the payment method.
        /// </summary>
        /// <value>The payment method.</value>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        /// <value>The invoice identifier.</value>
        public string InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the response time stamp.
        /// </summary>
        /// <value>The response time stamp.</value>
        public string ResponseTimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the ex error code.
        /// </summary>
        /// <value>The ex error code.</value>
        public string ExErrCode { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public string ErrCode { get; set; }

        /// <summary>
        /// Gets or sets the authentication code.
        /// </summary>
        /// <value>The authentication code.</value>
        public string AuthCode { get; set; }

        /// <summary>
        /// Gets or sets the reason code.
        /// </summary>
        /// <value>The reason code.</value>
        public string ReasonCode { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the token identifier.
        /// </summary>
        /// <value>The token identifier.</value>
        public string TokenId { get; set; }

        /// <summary>
        /// Gets or sets the response checksum.
        /// </summary>
        /// <value>The response checksum.</value>
        public string ResponseChecksum { get; set; }

        /// <summary>
        /// Gets or sets the advance response checksum.
        /// </summary>
        /// <value>The advance response checksum.</value>
        public string AdvanceResponseChecksum { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        /// <value>The total amount.</value>
        public string TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public string TransactionID { get; set; }

        /// <summary>
        /// Gets or sets the dynamic descriptor.
        /// </summary>
        /// <value>The dynamic descriptor.</value>
        public string DynamicDescriptor { get; set; }

        /// <summary>
        /// Gets or sets the unique cc.
        /// </summary>
        /// <value>The unique cc.</value>
        public string UniqueCC { get; set; }

        /// <summary>
        /// Gets or sets the item number.
        /// </summary>
        /// <value>The item number.</value>
        public string ItemNumber { get; set; }

        /// <summary>
        /// Gets or sets the item amount.
        /// </summary>
        /// <value>The item amount.</value>
        public string ItemAmount { get; set; }

        /// <summary>
        /// Gets or sets the item quantity.
        /// </summary>
        /// <value>The item quantity.</value>
        public string ItemQuantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid { get; set; }
    }
}
