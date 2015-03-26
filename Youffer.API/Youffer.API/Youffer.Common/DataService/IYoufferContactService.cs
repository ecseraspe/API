// ---------------------------------------------------------------------------------------------------
// <copyright file="IYoufferContactService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-21</date>
// <summary>
//     The IYoufferContactService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The IYoufferContactService interface
    /// </summary>
    public interface IYoufferContactService
    {
        /// <summary>
        /// Get Mapping details on the basis of contact Id.
        /// </summary>
        /// <param name="contactId"> The contact id. </param>
        /// <returns> ContactLeadMappingDto object </returns>
        ContactLeadMappingDto GetMappingEntryByContactId(string contactId);

        /// <summary>
        /// Get Mapping details on the basis of CRM contact Id.
        /// </summary>
        /// <param name="contactCRMId"> The CRM contact id. </param>
        /// <returns> ContactLeadMappingDto object </returns>
        ContactLeadMappingDto GetMappingEntryByCrMContactId(string contactCRMId);

        /// <summary>
        /// Gets the mapping entry by CRM lead identifier.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <returns>ContactLeadMappingDto object.</returns>
        ContactLeadMappingDto GetMappingEntryByCrmLeadId(string leadCRMId);

        /// <summary>
        /// Makes the mapping entry.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="leadId">The lead identifier.</param>
        /// <param name="createNewLead">if set to <c>true</c> [create new lead].</param>
        /// <returns>ContactLeadMappingDto object.</returns>
        ContactLeadMappingDto MakeMappingEntry(string contactId, string leadId, bool createNewLead);

        /// <summary>
        /// Gets the org CRM identifier.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>ApplicationUserDto object.</returns>
        ApplicationUserDto GetOrgCRMId(string companyId);

        /// <summary>
        /// Gets the org identifier from CRM identifier.
        /// </summary>
        /// <param name="orgCRMId">The org CRM identifier.</param>
        /// <returns>ApplicationUserDto object.</returns>
        ApplicationUserDto GetOrgIdFromCRMId(string orgCRMId);
    }
}
