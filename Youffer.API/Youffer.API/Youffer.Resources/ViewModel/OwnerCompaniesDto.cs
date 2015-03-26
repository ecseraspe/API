// ---------------------------------------------------------------------------------------------------
// <copyright file="OwnerCompaniesDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-12</date>
// <summary>
//     The OwnerCompaniesDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System;

    /// <summary>
    /// Class OwnerCompaniesDtocs
    /// </summary>
    public class OwnerCompaniesDto
    {
        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageURL { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can call.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can call; otherwise, <c>false</c>.
        /// </value>
        public bool CanCall { get; set; }

        /// <summary>
        /// Gets or sets the purchased on.
        /// </summary>
        /// <value>
        /// The purchased on.
        /// </value>
        public DateTime PurchasedOn { get; set; }
    }
}
