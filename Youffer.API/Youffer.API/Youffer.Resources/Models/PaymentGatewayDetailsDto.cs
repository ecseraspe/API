// ---------------------------------------------------------------------------------------------------
// <copyright file="PaymentGatewayDetailsDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-06</date>
// <summary>
//     The PaymentGatewayDetailsDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    public class PaymentGatewayDetailsDto
    {
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
    }
}
