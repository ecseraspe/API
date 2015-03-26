// ---------------------------------------------------------------------------------------------------
// <copyright file="PaypalPaymentDetails.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Pankaj Nema</author>
// <date>2015-01-16</date>
// <summary>
//     The PaypalPaymentDetails class
// </summary>
// ---------------------------------------------------------------------------------------------------

/// <summary>
/// Class PaypalPaymentDetails.
/// </summary>
namespace Youffer.DataService.DBSchema
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    [Table("PaypalPaymentDetails")]
    public class PaypalPaymentDetails
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
        public long TransactionId { get; set; }
        
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public string PayerId { get; set; }
        
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public string Email { get; set; }
        
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public string LastName { get; set; }
       
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public string Address { get; set; }
        
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public string Status { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether IsComplete
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public bool IsComplete { get; set; }
    }
}
