// ---------------------------------------------------------------------------------------------------
// <copyright file="PaymentGatewayDetails.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-05</date>
// <summary>
//     The PaymentGatewayDetails class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class PaymentGatewayDetails
    /// </summary>
    [Table("PaymentGatewayDetails")]
    public class PaymentGatewayDetails
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }     

        /// <summary>
        /// Gets or sets the payment gateway key.
        /// </summary>
        /// <value>
        /// The payment gateway key.
        /// </value>
        public string PaymentGatewayKey { get; set; }

        /// <summary>
        /// Gets or sets the payment gateway value.
        /// </summary>
        /// <value>
        /// The payment gateway value.
        /// </value>
        public string PaymentGatewayValue { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        /// <value>
        /// The modified on.
        /// </value>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the payment gateway reference identifier.
        /// </summary>
        /// <value>
        /// The payment gateway reference identifier.
        /// </value>
        public int? PaymentGatewayRefId { get; set; }

        /// <summary>
        /// Gets or sets the payment gateway.
        /// </summary>
        /// <value>
        /// The payment gateway.
        /// </value>
        [ForeignKey("PaymentGatewayRefId")]
        public virtual PaymentGateway PaymentGateway { get; set; } 
    }
}
