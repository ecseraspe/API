// ---------------------------------------------------------------------------------------------------
// <copyright file="PaypalDetailsWrapper.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Pankaj Nema</author>
// <date>2015-01-16</date>
// <summary>
//     The PaypalDetailsWrapper class
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
    /// Paypal details Wrapper
    /// </summary>
    public class PaypalDetailsWrapper
    {
        /// <summary>
        /// Gets or sets the PaypalDetails.
        /// </summary>
        public PaypalDetails PaypalDetails { get; set; }

        /// <summary>
        /// Gets or sets the Details.
        /// </summary>
        public List<PayPalDetailsModel> ItemDetails { get; set; }
    }
}
