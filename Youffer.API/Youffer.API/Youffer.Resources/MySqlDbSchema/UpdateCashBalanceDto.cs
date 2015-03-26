// ---------------------------------------------------------------------------------------------------
// <copyright file="UpdateCashBalanceDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-20</date>
// <summary>
//     The UpdateCashBalanceDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.MySqlDbSchema
{
    /// <summary>
    /// Class UpdateCashBalanceDto
    /// </summary>
    public class UpdateCashBalanceDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the org identifier.
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// Gets or sets the credit balance.
        /// </summary>
        public decimal CashBalance { get; set; }
    }
}
