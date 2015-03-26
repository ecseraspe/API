// ---------------------------------------------------------------------------------------------------
// <copyright file="PayPalDetailsModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Pankaj Nema</author>
// <date>2015-01-16</date>
// <summary>
//     The PayPalDetailsModel class
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
    /// Paypal details model
    /// </summary>
    public class PayPalDetailsModel
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the Need.
        /// </summary>
        public string Need { get; set; }
        
        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        public decimal Price { get; set; }
    }
}
