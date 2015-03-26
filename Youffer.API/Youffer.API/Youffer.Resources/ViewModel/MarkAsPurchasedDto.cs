// ---------------------------------------------------------------------------------------------------
// <copyright file="MarkAsPurchasedDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-23</date>
// <summary>
//     The MarkAsPurchasedDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    /// <summary>
    /// Class MarkAsPurchasedDto
    /// </summary>
    public class MarkAsPurchasedDto
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the interest.
        /// </summary>
        /// <value>
        /// The interest.
        /// </value>
        public string Interest { get; set; }
    }
}
