// ---------------------------------------------------------------------------------------------------
// <copyright file="SmsService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-26</date>
// <summary>
//     The SmsService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Sms
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Plivo.API;
    using RestSharp;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.Common.SMS;

    /// <summary>
    /// Class SmsService
    /// </summary>
    public class SmsService : ISmsService
    {
        /// <summary>
        /// The Plivo Rest API
        /// </summary>
        private readonly RestAPI plivo;

        /// <summary>
        /// The logger service
        /// </summary>
        private readonly ILoggerService loggerService;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The From  Number
        /// </summary>
        private readonly string fromNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsService" /> class.
        /// </summary>
        /// <param name="authToken"> The plivo AuthToken. </param>
        /// <param name="accountSid"> The plivo Account Sid. </param>
        /// <param name="fromNumber"> the plivo from number. </param> 
        /// <param name="loggerService"> The logger service</param>
        /// <param name="mapperFactory">the mapper factory</param>
        public SmsService(string authToken, string accountSid, string fromNumber, ILoggerService loggerService, IMapperFactory mapperFactory)
        {
            try
            {
                this.plivo = new RestAPI(accountSid, authToken);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex.Message);
            }

            this.fromNumber = fromNumber;
            this.loggerService = loggerService;
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Send Verification Code.
        /// </summary>
        /// <param name="toNumber"> Receivers number</param>
        /// <param name="message"> message string </param>
        /// <returns>string object</returns>
        public string SendVerificationCode(string toNumber, string message)
        {
            string messageUUID = string.Empty;
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>() 
                {
                    { "src", this.fromNumber },
                    { "dst", toNumber },
                    { "text", message }
                };

                IRestResponse<MessageResponse> resp = this.plivo.send_message(dict);
                if (resp.Data != null)
                {
                    messageUUID = resp.Data.message_uuid[0];
                }
                else
                {
                    this.loggerService.LogException("SendVerificationCode : " + resp.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendVerificationCode : " + ex.Message);
            }

            return messageUUID;
        }
    }
}
