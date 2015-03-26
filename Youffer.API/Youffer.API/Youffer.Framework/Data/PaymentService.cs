// ---------------------------------------------------------------------------------------------------
// <copyright file="PaymentService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gauav Barar</author>
// <date>2014-12-28</date>
// <summary>
//     The PaymentService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.Models;

    /// <summary>
    /// Class PaymentService.
    /// </summary>
    public class PaymentService : IPaymentService
    {
        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The invoice repository
        /// </summary>
        private readonly IRepository<Invoice> invoiceRepository;

        /// <summary>
        /// The g2srequest repository
        /// </summary>
        private readonly IRepository<G2SRequest> g2SRequestRepository;

        /// <summary>
        /// The g2sresponse repository
        /// </summary>
        private readonly IRepository<G2SResponse> g2SResponseRepository;

        /// <summary>
        /// The g2SDMN repository
        /// </summary>
        private readonly IRepository<G2SDMN> g2SDMNRepository;

        /// <summary>
        /// The paypal pay transaction repository
        /// </summary>
        private readonly IRepository<PaypalPayTransaction> paypalPayTransactionRepository;

        /// <summary>
        /// The paypal pay details repository
        /// </summary>
        private readonly IRepository<PaypalPaymentDetails> paypalPayDetailsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentService"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="invoiceRepository">The invoice repository.</param>
        /// <param name="g2SRequestRepository">The g2 s request repository.</param>
        /// <param name="g2SResponseRepository">The g2 s response repository.</param>
        /// <param name="g2SDMNRepository">The g2 SDMN repository.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="paypalPayTransactionRepository">The Paypal transaction repository</param>
        /// <param name="paypalPayDetailsRepository">The Paypal details repository</param>
        public PaymentService(ILoggerService loggerService, IRepository<Invoice> invoiceRepository, IRepository<G2SRequest> g2SRequestRepository, IRepository<G2SResponse> g2SResponseRepository, IRepository<G2SDMN> g2SDMNRepository, IMapperFactory mapperFactory, IRepository<PaypalPayTransaction> paypalPayTransactionRepository, IRepository<PaypalPaymentDetails> paypalPayDetailsRepository)
        {
            this.LoggerService = loggerService;
            this.mapperFactory = mapperFactory;
            this.g2SDMNRepository = g2SDMNRepository;
            this.g2SRequestRepository = g2SRequestRepository;
            this.g2SResponseRepository = g2SResponseRepository;
            this.invoiceRepository = invoiceRepository;
            this.paypalPayTransactionRepository = paypalPayTransactionRepository;
            this.paypalPayDetailsRepository = paypalPayDetailsRepository;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="invoiceModel">The invoice model.</param>
        /// <returns>returns int</returns>
        public long CreateInvoice(InvoiceModel invoiceModel)
        {
            try
            {
                Invoice invoice = this.mapperFactory.GetMapper<InvoiceModel, Invoice>().Map(invoiceModel);
                invoice.CreatedOn = DateTime.UtcNow;
                invoice.ModifiedOn = invoice.CreatedOn;
                this.invoiceRepository.Insert(invoice);
                this.invoiceRepository.Commit();
                return invoice.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="invoiceModel">The invoice model.</param>
        /// <returns>returns int</returns>
        public bool UpdateInvoice(InvoiceModel invoiceModel)
        {
            try
            {
                Invoice invoice = this.mapperFactory.GetMapper<InvoiceModel, Invoice>().Map(invoiceModel);
                invoice.ModifiedOn = DateTime.UtcNow;
                this.invoiceRepository.Update(invoice);
                this.invoiceRepository.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// gets invoice by id.
        /// </summary>
        /// <param name="id">The invoice id.</param>
        /// <returns>returns invoice model</returns>
        public InvoiceModel GetInvoice(long id)
        {
            Invoice invoice = this.invoiceRepository.Find(g => g.Id == id).FirstOrDefault();
            InvoiceModel invoiceModel = this.mapperFactory.GetMapper<Invoice, InvoiceModel>().Map(invoice);
            return invoiceModel;
        }

        /// <summary>
        /// Inserts the g2s request.
        /// </summary>
        /// <param name="g2SRequestModel">The invoice model.</param>
        /// <returns>returns int of the inserted row</returns>
        public long InsertG2SRequest(G2SRequestModel g2SRequestModel)
        {
            try
            {
                G2SRequest g2SRequest = this.mapperFactory.GetMapper<G2SRequestModel, G2SRequest>().Map(g2SRequestModel);
                g2SRequest.CreatedOn = DateTime.UtcNow;
                this.g2SRequestRepository.Insert(g2SRequest);
                this.g2SRequestRepository.Commit();
                return g2SRequest.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Inserts the g2s response.
        /// </summary>
        /// <param name="g2SResponseModel">The invoice model.</param>
        /// <returns>returns int of the inserted row</returns>
        public long InsertG2SResponse(G2SResponseModel g2SResponseModel)
        {
            try
            {
                G2SResponse g2SResponse = this.mapperFactory.GetMapper<G2SResponseModel, G2SResponse>().Map(g2SResponseModel);
                g2SResponse.CreatedOn = DateTime.UtcNow;
                this.g2SResponseRepository.Insert(g2SResponse);
                this.g2SResponseRepository.Commit();
                return g2SResponse.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Inserts the g2S DMN.
        /// </summary>
        /// <param name="g2SDMNModel">The invoice model.</param>
        /// <returns>returns int of the inserted row</returns>
        public long InsertG2SDMN(G2SDMNModel g2SDMNModel)
        {
            try
            {
                G2SDMN g2SDMN = this.mapperFactory.GetMapper<G2SDMNModel, G2SDMN>().Map(g2SDMNModel);
                g2SDMN.CreatedOn = DateTime.UtcNow;
                this.g2SDMNRepository.Insert(g2SDMN);
                this.g2SDMNRepository.Commit();
                return g2SDMN.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Inserts paypal transactions
        /// </summary>
        /// <param name="paypalPayTransaction">paypal transaction to insert</param>
        /// <returns> bool object </returns>
        public bool InsertPayPalTransaction(PaypalPayTransactionModel paypalPayTransaction)
        {
            try
            {
                PaypalPayTransaction table = this.mapperFactory.GetMapper<PaypalPayTransactionModel, PaypalPayTransaction>().Map(paypalPayTransaction);
                this.paypalPayTransactionRepository.Insert(table);
                this.paypalPayTransactionRepository.Commit();
                return true;
            }
            catch
            {
                // log the error;
                return false;
            }
        }

        /// <summary>
        /// Gets the paypalPayment
        /// </summary>
        /// <param name="paytoken">Token for payment</param>
        /// <param name="for2CO">Check for 2CO</param>
        /// <returns>PaypalPayTransactionModel object</returns>
        public PaypalPayTransactionModel GetPaypalPaymentId(Guid paytoken, bool for2CO = false)
        {
            PaypalPayTransactionModel model = null;
            PaypalPayTransaction paypalPayTransaction = new PaypalPayTransaction();
            try
            {
                if (for2CO)
                {
                    paypalPayTransaction = this.paypalPayTransactionRepository.AsEnumerable()
                        .Where(p => p.PayToken.Equals(paytoken) && p.IsPaymentDone == true).FirstOrDefault();
                }
                else
                {
                    paypalPayTransaction = this.paypalPayTransactionRepository.AsEnumerable()
                        .Where(p => p.PayToken.Equals(paytoken) && p.IsPaymentDone == false).FirstOrDefault();
                }

                model = this.mapperFactory.GetMapper<PaypalPayTransaction, PaypalPayTransactionModel>().Map(paypalPayTransaction);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetPaypalPaymentUd :- " + ex.Message);
            }

            return model;
        }

        /// <summary>
        /// Finalize the paypal transaction
        /// </summary>
        /// <param name="model">model to finalize</param>
        /// <returns>bool object</returns>
        public bool CompletePaypalTransaction(PaypalPaymentDetailsModel model)
        {
            bool result = false;
            try
            {
                PaypalPaymentDetails table = this.mapperFactory.GetMapper<PaypalPaymentDetailsModel, PaypalPaymentDetails>().Map(model);
                this.paypalPayDetailsRepository.Insert(table);
                PaypalPayTransaction trans = this.paypalPayTransactionRepository.Find(p => p.Id.Equals(model.TransactionId)).FirstOrDefault();
                trans.IsPaymentDone = true;
                this.paypalPayTransactionRepository.Update(trans);
                this.paypalPayTransactionRepository.Commit();
                this.paypalPayDetailsRepository.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("CompletePaypalTransaction :- " + ex.Message);
                result = false;                
            }

            return result;
        }
    }
}
