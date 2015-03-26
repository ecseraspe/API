// ---------------------------------------------------------------------------------------------------
// <copyright file="SendUpdateCreditBalanceMessageDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-19</date>
// <summary>
//     The SendUpdateCreditBalanceMessageDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.MySqlDbSchema
{
    /// <summary>
    /// Class SendUpdateCreditBalanceMessageDto
    /// </summary>
    public class SendUpdateCreditBalanceMessageDto
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
        public decimal CreditBalance { get; set; }

        /// <summary>
        /// Gets or sets the GCM identifier.
        /// </summary>
        public string GCMId { get; set; }

        /// <summary>
        /// Gets or sets the ud identifier.
        /// </summary>
        public string UDId { get; set; }
    }
}
