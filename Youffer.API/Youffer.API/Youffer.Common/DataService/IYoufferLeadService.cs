// ---------------------------------------------------------------------------------------------------
// <copyright file="IYoufferLeadService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-28</date>
// <summary>
//     The IYoufferLeadService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using System.Collections.Generic;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Class IYoufferLeadService.
    /// </summary>
    public interface IYoufferLeadService
    {
        /// <summary>
        /// Gets the mapping entry by lead CRM identifier.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <returns>The LeadOpportunityMappingDto object.</returns>
        LeadOpportunityMappingDto GetMappingEntryByLeadCRMId(string leadCRMId);

        /// <summary>
        /// Makes the mapping entry.
        /// </summary>
        /// <param name="leadId">The lead identifier.</param>
        /// <param name="opportunityId">The opportunity identifier.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="price">The price.</param>
        /// <param name="interest">The interest</param>
        /// <param name="userId">The user id.</param>
        /// <returns>LeadOpportunityMappingDto object.</returns>
        LeadOpportunityMappingDto MakeMappingEntry(string leadId, string opportunityId, string companyId, double price, string interest, string userId);

        /// <summary>
        /// Gets the mapping entry by lead and org CRM identifier.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <param name="orgCRMId">The org CRM identifier.</param>
        /// <returns>List of LeadOpportunityMappingDto object.</returns>
        List<LeadOpportunityMappingDto> GetMappingEntryByLeadAndOrgCRMId(string leadCRMId, string orgCRMId);

        /// <summary>
        /// Gets the mapping entry by lead and org CRM identifier and interest.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <param name="orgCRMId">The org CRM identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <returns>LeadOpportunityMappingDto object.></returns>
        LeadOpportunityMappingDto GetMappingEntryByLeadAndOrgCRMIdAndInterest(string leadCRMId, string orgCRMId, string interest);

        /// <summary>
        /// Gets the mapping entry by lead and org CRM identifier and interest.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <returns>LeadOpportunityMappingDto object.></returns>
        LeadOpportunityMappingDto GetMappingEntryByLeadCRMIdAndInterest(string leadCRMId, string interest);

        /// <summary>
        /// Gets the mapping entry by user id and org CRM identifier and interest.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="orgCRMId">The org CRM identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <returns>LeadOpportunityMappingDto object.></returns>
        LeadOpportunityMappingDto GetMappingEntryByContactAndOrgCRMIdAndInterest(string userId, string orgCRMId, string interest);
    }
}
