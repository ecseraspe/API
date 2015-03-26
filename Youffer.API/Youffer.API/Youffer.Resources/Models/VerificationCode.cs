// ---------------------------------------------------------------------------------------------------
// <copyright file="VerificationCode.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-27</date>
// <summary>
//     The VerificationCode class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    /// <summary>
    /// the verification code class
    /// </summary>
    public class VerificationCode
    {
        /// <summary>
        /// Gets or sets the Phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public string CountryCode { get; set; }
    }
}
