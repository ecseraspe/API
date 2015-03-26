// ---------------------------------------------------------------------------------------------------
// <copyright file="PaypalDetails.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Pankaj Nema</author>
// <date>2015-01-16</date>
// <summary>
//     The PaypalDetails class
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
    /// Paypal details
    /// </summary>
    public class PaypalDetails
    {
        /// <summary>
        /// Gets or sets the Details Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the Currency.
        /// </summary>
        public string Currency { get; set; }        
    }
}
