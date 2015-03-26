// ---------------------------------------------------------------------------------------------------
// <copyright file="BuyUserStatusMessage.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2015-2-23</date>
// <summary>
//     The BuyUserStatusMessage class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    /// <summary>
    /// Class BuyUserStatusMessage
    /// </summary>
    public class BuyUserStatusMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuyUserStatusMessage" /> class.
        /// </summary>
        public BuyUserStatusMessage()
        {
            this.ErrorMessage = string.Empty;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is sucess or not
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        public string ErrorMessage { get; set; }

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
