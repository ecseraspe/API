// ---------------------------------------------------------------------------------------------------
// <copyright file="BuyUserModelDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-20</date>
// <summary>
//     The BuyUserModelDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    /// <summary>
    /// Class BuyUserModelDto
    /// </summary>
    public class BuyUserModelDto
    {
        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the interest.
        /// </summary>
        public string Interest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [purchased from credit].
        /// </summary>
        public bool PurchasedFromCredit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [purchased from cash].
        /// </summary>
        public bool PurchasedFromCash { get; set; }
    }
}
