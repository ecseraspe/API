// ---------------------------------------------------------------------------------------------------
// <copyright file="ILeadService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-19</date>
// <summary>
//     The ILeadService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.CRMService
{
    using System.Collections.Generic;
    using System.Data;
    using Youffer.CRM;
    using Youffer.Resources.CRMModel;
    using Youffer.Resources.MySqlDbSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The ILeadService interface
    /// </summary>
    public interface ILeadService
    {
        /// <summary>
        ///  Adding Lead into CRM
        /// </summary>
        /// <param name="lead"> the lead entity. </param>
        /// <returns> VTigerLead entity </returns>
        VTigerLead CreateLead(VTigerLead lead);

        /// <summary>
        ///  Retrieving Lead from CRM
        /// </summary>
        /// <param name="leadId"> the Lead id. </param>
        /// <returns> VTigerLead entity </returns>
        VTigerLead ReadLead(string leadId);

        /// <summary>
        ///  Updating Lead in CRM
        /// </summary>
        /// <param name="lead">the Lead entity. </param>
        /// <returns> VTigerLead entity </returns>
        VTigerLead UpdateLead(VTigerLead lead);

        /// <summary>
        ///  Deleting lead from CRM
        /// </summary>
        /// <param name="leadId"> the Lead id. </param>
        /// <returns> bool object </returns>
        bool DeleteLead(string leadId);

        /// <summary>
        /// Blocks the lead.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <returns>bool object</returns>
        bool BlockLead(string contactId);

        /// <summary>
        /// Searches the leads.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns>List of VTigerLead object.</returns>
        List<VTigerLead> SearchLeads(SearchModelDto searchModel);

        /// <summary>
        /// Gets the dashboard.
        /// </summary>
        /// <param name="orgModel">The org model.</param>
        /// <param name="lastPageId">The last page identifier.</param>
        /// <param name="fetchCnt">The fetch count.</param>
        /// <param name="interest"> The interest name. </param>
        /// <returns>List of VTigerLead object</returns>
        List<VTigerLead> GetDashboard(OrganisationModel orgModel, int lastPageId, int fetchCnt, string interest);

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>List of StatisticsModelDto object.</returns>
        List<StatisticsModelDto> GetStatistics(string companyId);

        /// <summary>
        /// Trending search.
        /// </summary>
        /// <returns>List of string.</returns>
        List<string> TrendingSearch();

        /// <summary>
        /// Gets the query result.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable object.</returns>
        DataTable GetQueryResult(string query);

        /// <summary>
        /// Gets the meta data.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="fieldIndex">Index of the field.</param>
        /// <returns>List of VTigerPicklistItem object.</returns>
        List<VTigerPicklistItem> GetMetaData(VTigerType module, int fieldIndex);

        /// <summary>
        /// Get the Query Result Based on Entity
        /// </summary>
        /// <typeparam name="T"> Type of entity</typeparam>
        /// <param name="query"> The query values</param>
        /// <returns> IEnumerable  object</returns>
        IEnumerable<T> GetQueryResult<T>(string query) where T : VTigerEntity;

        /// <summary>
        /// THe Dashboard data
        /// </summary>
        /// <param name="orgCRMId"> org Id</param>
        /// <param name="lastPageId"> last page id</param>
        /// <param name="fetchCnt">fetch count</param>
        /// <returns>List of VTigerDashBoardData</returns>
        List<VTigerDashBoardData> GetNewDashboard(string orgCRMId, int lastPageId, int fetchCnt);

        /// <summary>
        /// Get the Top Leads
        /// </summary>
        /// <param name="topLeadsDto">The top leads Dto.</param>
        /// <returns>List of VTigerDashBoardData</returns>
        List<VTigerDashBoardData> GetTopLeads(TopLeadsDto topLeadsDto);

        /// <summary> 
        /// Get the searched Leads
        /// </summary>
        /// <param name="orgId"> The org Id</param>
        /// <param name="model"> lThe search model</param> 
        /// <returns>List of VTigerDashBoardData</returns>
        List<VTigerDashBoardData> GetSearchedLeads(string orgId, SearchModelDto model);
    }
}
