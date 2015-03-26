// ---------------------------------------------------------------------------------------------------
// <copyright file="PaypalPaymentDetailsModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Pankaj Nema</author>
// <date>2015-01-16</date>
// <summary>
//     The PaypalPaymentDetailsModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class PayPalDetails.
    /// </summary>
    public class PaypalPaymentDetailsModel
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
        /// Gets or sets a value indicating whether IsComplete.
        /// </summary>
        /// <value><c>true</c> if [IsComplete]; otherwise, <c>false</c>.</value>
        public bool IsComplete { get; set; }
    }
}
