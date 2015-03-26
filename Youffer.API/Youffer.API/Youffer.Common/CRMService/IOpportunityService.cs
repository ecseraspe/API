// ---------------------------------------------------------------------------------------------------
// <copyright file="IOpportunityService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-19</date>
// <summary>
//     The IOpportunityService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.CRMService
{
    using System.Collections.Generic;
    using Youffer.CRM;
    using Youffer.Resources.MySqlDbSchema;

    /// <summary>
    /// The IOpportunityService class
    /// </summary>
    public interface IOpportunityService
    {
        /// <summary>
        ///  Adding Opportunity into CRM
        /// </summary>
        /// <param name="opportunity"> The Opportunity entity. </param>
        /// <returns> VTigerPotential entity </returns>
        VTigerPotential CreateOpportunity(VTigerPotential opportunity);

        /// <summary>
        ///  Retrieving Opportunity from CRM
        /// </summary>
        /// <param name="opportunityId"> The Opportunity id. </param>
        /// <returns> VTigerPotential entity </returns>
        VTigerPotential ReadOpportunity(string opportunityId);

        /// <summary>
        ///  Updating Opportunity in CRM
        /// </summary>
        /// <param name="opportunity"> The Opportunity entity. </param>
        /// <returns> VTigerPotential entity </returns>
        VTigerPotential UpdateOpportunity(VTigerPotential opportunity);

        /// <summary>
        ///  Deleting Opportunity from CRM
        /// </summary>
        /// <param name="opportunityId"> The Opportunity id. </param>
        /// <returns> bool object </returns>
        bool DeleteOpportunity(string opportunityId);

        /// <summary>
        /// Gets the opportunities.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>
        /// List of VTigerPotential object.
        /// </returns>
        List<VTigerPotential> GetOpportunities(string companyId, int lastpageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc");

        /// <summary>
        /// Reports the company.
        /// </summary>
        /// <param name="lst">The lst.</param>
        /// <returns>Boolean object.</returns>
        bool ReportCompany(List<VTigerPotential> lst);

        /// <summary>
        /// Reports the user.
        /// </summary>
        /// <param name="lst">The lst.</param>
        /// <returns>Boolean object.</returns>
        bool ReportUser(List<VTigerPotential> lst);

        /// <summary>
        /// Marks the not call.
        /// </summary>
        /// <param name="lst">The lst.</param>
        /// <returns>Boolean object.</returns>
        bool MarkNotCall(List<VTigerPotential> lst);

        /// <summary>
        /// Gets the owner companies.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of VTigerPotential object.</returns>
        List<VTigerPotential> GetOwnerCompanies(string userId);

        /// <summary>
        /// Gets the unreviewd clients.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastpageId">The last page id. </param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>List of VTigerPotential object.</returns>
        List<VTigerPotential> GetUnReviewdClients(string companyId, int lastpageId, int fetchCount);

        /// <summary>
        /// Gets the purchased user total amount.
        /// </summary>
        /// <param name="crmContactId">The CRM contact identifier.</param>
        /// <returns>Decimal value</returns>
        decimal GetPurchasedUserTotalAmount(string crmContactId);

        /// <summary>
        /// Checks if user blocked.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>Bool object</returns>
        bool CheckIfUserBlocked(string contactId, string orgId);

        /// <summary>
        /// Gets the unreviewd clients.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="searchText"> The searchText </param>
        /// <param name="lastpageId">The last page id. </param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>List of VtigerPotentialData object.</returns>
        List<VtigerPotentialData> GetUnReviewdClients1(string companyId, string searchText, int lastpageId, int fetchCount);

        /// <summary>
        /// Gets the Count ofunreviewd clients.
        /// </summary>
        /// <param name="companyId">The company identifier.</param> 
        /// <param name="searchText"> The searchText </param>
        /// <returns> Result Count.</returns>
        int GetUnReviewdClientsCount(string companyId, string searchText);

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <param name="crmOrgId">The company identifier.</param>
        /// <param name="searchText"> THe searchText</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>
        /// List of VtigerPotentialData object.
        /// </returns>
        List<VtigerPotentialData> GetMyClients(string crmOrgId, string searchText, int lastpageId, int fetchCount, string sortBy, string direction);

        /// <summary>
        /// Gets the opportunity.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <returns>VTigerPotential object.</returns>
        VTigerPotential GetOpportunity(string companyId, string userId, string interest);
    }
}
