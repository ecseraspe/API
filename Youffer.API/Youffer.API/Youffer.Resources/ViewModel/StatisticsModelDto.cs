// ---------------------------------------------------------------------------------------------------
// <copyright file="StatisticsModelDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-8</date>
// <summary>
//     The StatisticsModelDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System.Collections.Generic;
    using Youffer.Resources.Models;

    /// <summary>
    /// The StatisticsModelDto class
    /// </summary>
    public class StatisticsModelDto
    {
        /// <summary>
        /// Gets or sets the total clients.
        /// </summary>
        /// <value>
        /// The total clients.
        /// </value>
        public List<DictModel> Clients { get; set; }

        /// <summary>
        /// Gets or sets the purchased clients.
        /// </summary>
        /// <value>
        /// The purchased clients.
        /// </value>
        public List<DictModel> PurchasedClients { get; set; }

        /// <summary>
        /// Gets or sets the average rating.
        /// </summary>
        /// <value>
        /// The average rating.
        /// </value>
        public decimal AvgRating { get; set; }
    }
}
