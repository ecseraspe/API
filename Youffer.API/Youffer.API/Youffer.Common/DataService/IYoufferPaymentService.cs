// ---------------------------------------------------------------------------------------------------
// <copyright file="IYoufferPaymentService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-11</date>
// <summary>
//     The IYoufferPaymentService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using System.Collections.Generic;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Interface IYoufferPaymentService
    /// </summary>
    public interface IYoufferPaymentService
    {
        /// <summary>
        /// Gets the user purchase details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of LeadOpportunityMappingDto object.</returns>
        List<LeadOpportunityMappingDto> GetUserPurchaseDetails(string userId);

        /// <summary>
        /// Gets the purchased user total amount.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <returns>decimal object.</returns>
        decimal GetPurchasedUserTotalAmount(string contactId);

        /// <summary>
        /// Requests the payment.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <returns>Boolean object.</returns>
        bool RequestPayment(string leadCRMId);

        /// <summary>
        /// Saves the pay pal details.
        /// </summary>
        /// <param name="payPalDetailsDto">The pay pal details dto.</param>
        /// <returns>PayPalDetailsDto object.</returns>
        PayPalDetailsDto SavePayPalDetails(PayPalDetailsDto payPalDetailsDto);
    }
}
