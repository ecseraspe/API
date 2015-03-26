// ---------------------------------------------------------------------------------------------------
// <copyright file="CheckOutDetailsModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gaurav Sharma</author>
// <date>2015-01-29</date>
// <summary>
//     The CheckOutDetailsModel class
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
    /// CheckOut details model
    /// </summary>
    public class CheckOutDetailsModel
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

        /// <summary>
        /// Gets or sets the ProcessingFee.
        /// </summary>
        public decimal ProcessingFee { get; set; }

        /// <summary>
        /// Gets or sets the 2CO URL.
        /// </summary>
        public string URL2CO { get; set; }

        /// <summary>
        /// Gets or sets the 2CO Currency Code.
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the 2CO Mode.
        /// </summary>
        public string Mode2CO { get; set; }

        /// <summary>
        /// Gets or sets the 2CO Account Number.
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// Gets or sets the User Id.
        /// </summary>
        public string UserId { get; set; }
    }
}
