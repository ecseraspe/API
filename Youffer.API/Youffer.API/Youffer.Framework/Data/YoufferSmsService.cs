// ---------------------------------------------------------------------------------------------------
// <copyright file="YoufferSmsService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-27</date>
// <summary>
//     The YoufferSmsService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.Models;

    /// <summary>
    /// The Youffer Sms Service class
    /// </summary>
    public class YoufferSmsService : IYoufferSmsService
    {
        /// <summary>
        ///  Gets the User Repository.
        /// </summary>
        private readonly IRepository<SmsVerification> smsRepository;

        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        ///  Initializes a new instance of the <see cref="YoufferSmsService" /> class.
        /// </summary>
        /// <param name="smsRepository"> The Sms Repository. </param>
        /// <param name="loggerService"> The Logger Service. </param>
        /// <param name="mapperFactory">The mapper factory.</param>        
        public YoufferSmsService(IRepository<SmsVerification> smsRepository, ILoggerService loggerService, IMapperFactory mapperFactory)
        {
            this.smsRepository = smsRepository;
            this.LoggerService = loggerService;
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Insert update Sms Verification Code record
        /// </summary>
        /// <param name="sms"> The Sms verification dto object</param>
        /// <returns> SmsVerificationDto object </returns>
        public SmsVerificationDto InsUpdSms(SmsVerificationDto sms)
        {
            try
            {
                object[] sqlCol = { new SqlParameter("@SmsId", sms.Id), new SqlParameter("@UserId", sms.UserId), new SqlParameter("@PhoneNumber", sms.PhoneNumber), new SqlParameter("@Code", sms.Code), new SqlParameter("@Message", sms.Message), new SqlParameter("@MessageStatus", sms.MessageStatus), new SqlParameter("@MessageSid", sms.MessageSid) };
                SmsVerification smsVerification = this.smsRepository.SqlQuery<SmsVerification>("InsUpdSms @SmsId, @UserId, @PhoneNumber, @Code, @Message, @MessageStatus, @MessageSid", sqlCol).FirstOrDefault();
                sms = this.mapperFactory.GetMapper<SmsVerification, SmsVerificationDto>().Map(smsVerification);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Ins Upd Sms :- " + ex.Message);
            }

            return sms;
        }

        /// <summary>
        /// Check the give code is matched or not
        /// </summary>
        /// <param name="userId"> The UserId</param>
        /// <param name="code"> The verification Code</param>
        /// <returns> SmsVerificationDto object </returns>
        public SmsVerificationDto IsCodeVerified(string userId, string code)
        {
            try
            {
                var sms = this.smsRepository.Find(x => x.UserId == userId && x.Code == code && x.IsActive).FirstOrDefault() ?? new SmsVerification();
                SmsVerificationDto smsVerificationDetails = this.mapperFactory.GetMapper<SmsVerification, SmsVerificationDto>().Map(sms);
                return smsVerificationDetails;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("verify Code :- " + ex.Message);
            }

            return new SmsVerificationDto();
        }
    }
}
