// ---------------------------------------------------------------------------------------------------
// <copyright file="IPaymentService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gaurav Barar</author>
// <date>2014-12-28</date>
// <summary>
//     The IPaymentService class
// </summary>
// ---------------------------------------------------------------------------------------------------

/// <summary>
/// The DataService namespace.
/// </summary>
namespace Youffer.Common.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.Models;

    /// <summary>
    /// Interface ICommonService
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="invoiceModel">The invoice model.</param>
        /// <returns>returns int of the inserted invoice</returns>
        long CreateInvoice(InvoiceModel invoiceModel);

        /// <summary>
        /// gets invoice by id.
        /// </summary>
        /// <param name="id">The invoice id.</param>
        /// <returns>returns invoice id</returns>
        InvoiceModel GetInvoice(long id);

        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="invoiceModel">The invoice model.</param>
        /// <returns>returns true or false</returns>
        bool UpdateInvoice(InvoiceModel invoiceModel);

        /// <summary>
        /// Inserts the g2s request.
        /// </summary>
        /// <param name="g2SRequestModel">The G2SRequest model.</param>
        /// <returns>returns int of the inserted row.</returns>
        long InsertG2SRequest(G2SRequestModel g2SRequestModel);

        /// <summary>
        /// Inserts the g2s response.
        /// </summary>
        /// <param name="g2SResponseModel">The G2SResponse model.</param>
        /// <returns>returns int of the inserted row.</returns>
        long InsertG2SResponse(G2SResponseModel g2SResponseModel);

        /// <summary>
        /// Inserts the g2S DMN.
        /// </summary>
        /// <param name="g2SDMNModel">The G2SDMN model.</param>
        /// <returns>returns int of the inserted row.</returns>
        long InsertG2SDMN(G2SDMNModel g2SDMNModel);

        /// <summary>
        /// Inserts paypal transactions
        /// </summary>
        /// <param name="paypalPayTransaction">paypal transaction to insert</param>
        /// <returns> bool object </returns>
        bool InsertPayPalTransaction(PaypalPayTransactionModel paypalPayTransaction);

        /// <summary>
        /// Gets the paypalPayment
        /// </summary>
        /// <param name="paytoken">Token for payment</param>
        /// <param name="for2CO">flag to differentiate between 2co and paypal</param>
        /// <returns>PaypalPayTransactionModel object</returns>
        PaypalPayTransactionModel GetPaypalPaymentId(Guid paytoken, bool for2CO = false);

        /// <summary>
        /// Finalize the paypal transaction
        /// </summary>
        /// <param name="model">model to finalize</param>
        /// <returns>bool object</returns>
        bool CompletePaypalTransaction(PaypalPaymentDetailsModel model);
    }
}
