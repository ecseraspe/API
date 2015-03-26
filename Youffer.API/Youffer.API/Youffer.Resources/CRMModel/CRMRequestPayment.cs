// ---------------------------------------------------------------------------------------------------
// <copyright file="CRMRequestPayment.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-20</date>
// <summary>
//     The CRMRequestPayment class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.CRMModel
{
    using System;

    /// <summary>
    /// Class CRMRequestPayment
    /// </summary>
    public class CRMRequestPayment : BaseCrmModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CRMRequestPayment" /> class.
        /// </summary>
        public CRMRequestPayment()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the request payment no.
        /// </summary>
        public string RequestPaymentNo { get; set; }

        /// <summary>
        /// Gets or sets the contact identifier.
        /// </summary>
        public string ContactId { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is approved.
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the created on
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the Modified on
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}
