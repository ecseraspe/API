// ---------------------------------------------------------------------------------------------------
// <copyright file="IYoufferSmsService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-27</date>
// <summary>
//     The IYoufferSmsService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using Youffer.Resources.Models;

   /// <summary>
   /// The youffer service interface
   /// </summary>
    public interface IYoufferSmsService
    {
        /// <summary>
        /// Insert update Sms Verification Code record
        /// </summary>
        /// <param name="sms"> The Sms verification dto object</param>
        /// <returns> SmsVerificationDto object </returns>
        SmsVerificationDto InsUpdSms(SmsVerificationDto sms);

        /// <summary>
        /// Check the give code is matched or not
        /// </summary>
        /// <param name="userId"> The UserId</param>
        /// <param name="code"> The verification Code</param>
        /// <returns> SmsVerificationDto object </returns>
        SmsVerificationDto IsCodeVerified(string userId, string code);
    }
}
