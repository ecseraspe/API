// ---------------------------------------------------------------------------------------------------
// <copyright file="PaymentGatewayDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-06</date>
// <summary>
//     The PaymentGatewayDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class PaymentGatewayDto
    /// </summary>
    public class PaymentGatewayDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the payment gateway details dto.
        /// </summary>
        /// <value>
        /// The payment gateway details dto.
        /// </value>
        [IgnoreDataMember]
        public List<PaymentGatewayDetailsDto> PaymentGatewayDetailsInfo { get; set; }

        /// <summary>
        /// Gets or sets the payment gateway information.
        /// </summary> 
        public Dictionary<string, string> PaymentGatewayInfoDict { get; set; }
    }
}
