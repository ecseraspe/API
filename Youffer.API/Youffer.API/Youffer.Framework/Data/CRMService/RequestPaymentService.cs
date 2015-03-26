// ---------------------------------------------------------------------------------------------------
// <copyright file="RequestPaymentService.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-20</date>
// <summary>
//     The RequestPaymentService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data.CRMService
{
    using System;
    using Youffer.Common.CRMService;
    using Youffer.Common.LogService;
    using Youffer.CRM;

    /// <summary>
    /// Class RequestPaymentService
    /// </summary>
    public class RequestPaymentService : IRequestPaymentService
    {
        /// <summary>
        /// VTiger service instance
        /// </summary>
        private readonly IVTigerService vTigerService;

        /// <summary>
        ///  Initializes a new instance of the <see cref="RequestPaymentService" /> class.
        /// </summary>
        /// <param name="vTigerService">the vtiger service</param>
        /// <param name="loggerService">the logger service</param>
        public RequestPaymentService(IVTigerService vTigerService, ILoggerService loggerService)
        {
            this.vTigerService = vTigerService;
            this.LoggerService = loggerService;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Creates the payment request.
        /// </summary>
        /// <param name="requestPayment">The request payment.</param>
        /// <returns>VTigerRequestPayment object.</returns>
        public VTigerRequestPayment CreatePaymentRequest(VTigerRequestPayment requestPayment)
        {
            try
            {
                requestPayment = this.vTigerService.Create<VTigerRequestPayment>(requestPayment);
                return requestPayment;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Create Payment Request :- " + ex.Message);
            }

            return new VTigerRequestPayment();
        }
    }
}
