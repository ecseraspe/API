// ---------------------------------------------------------------------------------------------------
// <copyright file="TopLeadsDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-24</date>
// <summary>
//     The TopLeadsDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    /// <summary>
    /// Class TopLeadsDto
    /// </summary>
    public class TopLeadsDto
    {
        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the interest.
        /// </summary>
        public string Interest { get; set; }

        /// <summary>
        /// Gets or sets the last page identifier.
        /// </summary>
        public int LastPageId { get; set; }

        /// <summary>
        /// Gets or sets the fetch count.
        /// </summary>
        public int FetchCount { get; set; }
    }
}
