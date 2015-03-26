// ---------------------------------------------------------------------------------------------------
// <copyright file="UserBalanceModelDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-26</date>
// <summary>
//     The UserBalanceModelDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    /// <summary>
    /// Class UserBalanceModelDto
    /// </summary>
    public class UserBalanceModelDto
    {
        /// <summary>
        /// Gets or sets the cash balance.
        /// </summary>
        public decimal CashBalance { get; set; }

        /// <summary>
        /// Gets or sets the creditbalance.
        /// </summary>
        public decimal CreditBalance { get; set; }
    }
}
