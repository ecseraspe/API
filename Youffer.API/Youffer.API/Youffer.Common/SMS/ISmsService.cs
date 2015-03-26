// ---------------------------------------------------------------------------------------------------
// <copyright file="ISmsService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-26</date>
// <summary>
//     The ISmsService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.SMS
{
    /// <summary>
    /// The Sms service interface
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// Send Verification Code.
        /// </summary>
        /// <param name="toNumber"> Receivers number</param>
        /// <param name="message"> message string </param>
        /// <returns>string object</returns>
        string SendVerificationCode(string toNumber, string message);
    }
}
