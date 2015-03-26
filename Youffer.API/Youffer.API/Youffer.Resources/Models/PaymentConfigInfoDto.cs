// ---------------------------------------------------------------------------------------------------
// <copyright file="PaymentConfigInfoDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2015-1-29</date>
// <summary>
//     The PaymentConfigInfoDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    using System.Collections.Generic;
    using Youffer.Resources.Enum;

    /// <summary>
    /// the payment  config info class
    /// </summary>
    public class PaymentConfigInfoDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the payment for.
        /// </summary>
        public PaymentConfigType PaymentForType { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the Currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the Processing Amount.
        /// </summary>
        public decimal ProcessingAmount { get; set; }

        /// <summary>
        /// Gets or sets the Processing Fee Percentage.
        /// </summary>
        public decimal ProcessingFeePercentage { get; set; }

        /// <summary>
        /// Gets or sets the payment gateway dto.
        /// </summary> 
        public List<PaymentGatewayDto> PaymentGatewayInfo { get; set; }       
    }
}
