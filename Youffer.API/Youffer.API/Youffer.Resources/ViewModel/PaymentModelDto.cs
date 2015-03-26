// ---------------------------------------------------------------------------------------------------
// <copyright file="PaymentModelDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-10</date>
// <summary>
//     The PaymentModelDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using Youffer.Resources.Enum;

    /// <summary>
    /// Class PaymentModelDto
    /// </summary>
    public class PaymentModelDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentModelDto"/> class.
        /// </summary>
        public PaymentModelDto()
        {
        }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        public PaymentMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the pay pal identifier.
        /// </summary>
        /// <value>
        /// The pay pal identifier.
        /// </value>
        public string PayPalId { get; set; }

        /// <summary>
        /// Gets or sets the credit card no.
        /// </summary>
        /// <value>
        /// The credit card no.
        /// </value>
        public string CreditCardNo { get; set; }

        /// <summary>
        /// Gets or sets the CCV.
        /// </summary>
        /// <value>
        /// The CCV.
        /// </value>
        public string CCV { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        public string ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the number on card.
        /// </summary>
        /// <value>
        /// The number on card.
        /// </value>
        public string NumberOnCard { get; set; }
    }
}
