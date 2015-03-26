// ---------------------------------------------------------------------------------------------------
// <copyright file="IRequestPaymentService.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-20</date>
// <summary>
//     The IRequestPaymentService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.CRMService
{
    using Youffer.CRM;

    /// <summary>
    /// Interface IRequestPaymentService
    /// </summary>
    public interface IRequestPaymentService
    {
        /// <summary>
        /// Creates the payment request.
        /// </summary>
        /// <param name="requestPayment">The request payment.</param>
        /// <returns>VTigerRequestPayment object.</returns>
        VTigerRequestPayment CreatePaymentRequest(VTigerRequestPayment requestPayment);
    }
}
