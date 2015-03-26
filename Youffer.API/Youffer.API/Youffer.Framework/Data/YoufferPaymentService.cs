// ---------------------------------------------------------------------------------------------------
// <copyright file="YoufferPaymentService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-11</date>
// <summary>
//     The YoufferPaymentService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Class YoufferPaymentService
    /// </summary>
    public class YoufferPaymentService : IYoufferPaymentService
    {
        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The payment repository
        /// </summary>
        private readonly IRepository<LeadOpportunityMapping> paymentRepository;

        /// <summary>
        /// The PayPalDetails repository
        /// </summary>
        private readonly IRepository<PayPalDetails> payPalDetailsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="YoufferPaymentService"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="paymentRepository">The payment repository.</param>
        /// <param name="payPalDetailsRepository"> The PayPalDetails repository</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        public YoufferPaymentService(ILoggerService loggerService, IRepository<LeadOpportunityMapping> paymentRepository, IRepository<PayPalDetails> payPalDetailsRepository, IMapperFactory mapperFactory)
        {
            this.LoggerService = loggerService;
            this.paymentRepository = paymentRepository;
            this.mapperFactory = mapperFactory;
            this.payPalDetailsRepository = payPalDetailsRepository;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Gets the user purchase details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of LeadOpportunityMappingDto object.</returns>
        public List<LeadOpportunityMappingDto> GetUserPurchaseDetails(string userId)
        {
            List<LeadOpportunityMappingDto> lstLeadOppMapping = new List<LeadOpportunityMappingDto>();

            try
            {
                List<LeadOpportunityMapping> lst = this.paymentRepository.Find(x => x.ContactId == userId).ToList();
                lstLeadOppMapping = this.mapperFactory.GetMapper<List<LeadOpportunityMapping>, List<LeadOpportunityMappingDto>>().Map(lst);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetUserPurchaseDetails - " + ex.Message);
            }

            return lstLeadOppMapping;
        }

        /// <summary>
        /// Gets the purchased user total amount.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <returns>decimal object.</returns>
        public decimal GetPurchasedUserTotalAmount(string contactId)
        {
            decimal total = 0;
            try
            {
                var lst = this.paymentRepository.Find(x => x.ContactId == contactId && x.IsPending).ToList();
                total = lst.Sum(t => t.Price);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetPurchasedUserTotalAmount - " + ex.Message);
            }

            return total;
        }

        /// <summary>
        /// Requests the payment.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <returns>Boolean object.</returns>
        public bool RequestPayment(string leadCRMId)
        {
            bool isSuccess = true;

            try
            {
                object[] sqlCol = { new SqlParameter("@LeadCRMId", leadCRMId) };
                this.paymentRepository.SqlQuery<LeadOpportunityMappingDto>("RequestPayment @LeadCRMId ", sqlCol).FirstOrDefault();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("RequestPayment - " + ex.Message);
            }

            return isSuccess;
        }

        /// <summary>
        /// Saves the pay pal details.
        /// </summary>
        /// <param name="payPalDetailsDto">The pay pal details dto.</param>
        /// <returns>PayPalDetailsDto object.</returns>
        public PayPalDetailsDto SavePayPalDetails(PayPalDetailsDto payPalDetailsDto)
        {
            PayPalDetails payPalDetails = new PayPalDetails();
            payPalDetails = this.mapperFactory.GetMapper<PayPalDetailsDto, PayPalDetails>().Map(payPalDetailsDto);

            try
            {
                this.payPalDetailsRepository.Insert(payPalDetails);
                this.payPalDetailsRepository.Commit();

                payPalDetailsDto.Id = payPalDetails.Id;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("SavePayPalDetails - " + ex.Message);
            }

            return payPalDetailsDto;
        }
    }
}
