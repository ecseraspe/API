// ---------------------------------------------------------------------------------------------------
// <copyright file="PaypalPayTransaction.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Pankaj Nema</author>
// <date>2015-01-16</date>
// <summary>
//     The PaypalPayTransaction class
// </summary>
// ---------------------------------------------------------------------------------------------------

/// <summary>
/// Class PaypalPayTransaction.
/// </summary>
namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PaypalPayTransaction")]
    public class PaypalPayTransaction
    {
        /// <summary>
        /// Gets the repository Id.
        /// </summary>
        /// <value>The repository identifier.</value>
        [NotMapped]
        public object RepositoryId
        {
            get
            {
                return this.Id;
            }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
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
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public Guid PayToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsPaymentDone
        /// </summary>
        /// <value>
        /// The  IsPaymentDone.
        /// </value>
        public bool IsPaymentDone { get; set; }

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
        public decimal Amount { get; set; }

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
        /// Gets or sets a value indicating whether IsPaypalTransaction
        /// </summary>
        /// <value>
        /// The  IsPaypalTransaction.
        /// </value>
        public bool IsPaypalTransaction { get; set; }
    }
}
