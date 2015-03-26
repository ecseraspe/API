// ---------------------------------------------------------------------------------------------------
// <copyright file="PaymentGateway.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-06</date>
// <summary>
//     The PaymentGateway class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class PaymentGateway
    /// </summary>
    [Table("PaymentGateway")]
    public class PaymentGateway
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the payment configuration reference identifier.
        /// </summary>
        /// <value>
        /// The payment configuration reference identifier.
        /// </value>
        public int? PaymentConfigRefId { get; set; }

        /// <summary>
        /// Gets or sets the payment configuration information.
        /// </summary>
        /// <value>
        /// The payment configuration information.
        /// </value>
        [ForeignKey("PaymentConfigRefId")]
        public virtual PaymentConfigInfo PaymentConfigInfo { get; set; }

        /// <summary>
        /// Gets or sets the payment gateway details.
        /// </summary>
        /// <value>
        /// The payment gateway details.
        /// </value>
        public virtual ICollection<PaymentGatewayDetails> PaymentGatewayDetails { get; set; }
    }
}
