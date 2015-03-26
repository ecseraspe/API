// ---------------------------------------------------------------------------------------------------
// <copyright file="PaypalPayTransactionModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Pankaj Nema</author>
// <date>2015-01-16</date>
// <summary>
//     The PaypalPayTransactionModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

/// <summary>
/// Class PaypalPayTransactionModel.
/// </summary>
namespace Youffer.Resources.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PaypalPayTransactionModel
    {        
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created on.</value>
        public DateTime CreatedOn { get; set; }
        
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public string CompanyId { get; set; }
        
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public string ClientId { get; set; }
        
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public string Need { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public Guid PayToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the IsPaymentDone.
        /// </summary>
        /// <value><c>true</c> if [IsPaymentDone]; otherwise, <c>false</c>.</value>
        public bool IsPaymentDone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsPaypalTransaction
        /// </summary>
        /// <value><c>true</c> if [IsPaypalTransaction]; otherwise, <c>false</c>.</value>
        public bool IsPaypalTransaction { get; set; }
    }
}
