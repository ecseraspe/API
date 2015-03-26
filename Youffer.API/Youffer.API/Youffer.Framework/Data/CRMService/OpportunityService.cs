// ---------------------------------------------------------------------------------------------------
// <copyright file="OpportunityService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-19</date>
// <summary>
//     The OpportunityService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data.CRMService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Youffer.Common.CRMService;
    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.CRM;
    using Youffer.DataService;
    using Youffer.Resources.MySqlDbSchema;

    /// <summary>
    /// The OpportunityService class.
    /// </summary>
    public class OpportunityService : IOpportunityService
    {
        #region Service Instance
        /// <summary>
        /// VTiger service instance
        /// </summary>
        private readonly IVTigerService vTigerService;

        /// <summary>
        /// The youffer contact service
        /// </summary>
        private readonly IYoufferLeadService youfferLeadService;

        /// <summary>
        /// The youffer contact service
        /// </summary>
        private readonly IYoufferContactService youfferContactService;

        /// <summary>
        /// The lead service
        /// </summary>
        private readonly ILeadService leadService;

        /// <summary>
        /// The user review service
        /// </summary>
        private readonly IUserReviewService userReviewService;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="OpportunityService" /> class.
        /// </summary>
        /// <param name="vTigerService">the vtiger service</param>
        /// <param name="loggerService">the logger service</param>
        /// <param name="youfferLeadService">The youffer lead service.</param>
        /// <param name="youfferContactService">The youffer contact service.</param>
        /// <param name="leadService"> The lead service.</param>
        /// <param name="userReviewService">The user review service.</param>
        public OpportunityService(IVTigerService vTigerService, ILoggerService loggerService, IYoufferLeadService youfferLeadService, IYoufferContactService youfferContactService, ILeadService leadService, IUserReviewService userReviewService)
        {
            this.vTigerService = vTigerService;
            this.LoggerService = loggerService;
            this.youfferLeadService = youfferLeadService;
            this.youfferContactService = youfferContactService;
            this.leadService = leadService;
            this.userReviewService = userReviewService;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        ///  Adding Opportunity into CRM
        /// </summary>
        /// <param name="opportunity"> The Opportunity entity. </param>
        /// <returns> VTigerPotential entity </returns>
        public VTigerPotential CreateOpportunity(VTigerPotential opportunity)
        {
            try
            {
                opportunity = this.vTigerService.Create<VTigerPotential>(opportunity);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Adding Opportunity :- " + ex.Message);
                opportunity = new VTigerPotential();
            }

            return opportunity;
        }

        /// <summary>
        ///  Retrieving Opportunity from CRM
        /// </summary>
        /// <param name="opportunityId"> The Opportunity id. </param>
        /// <returns> VTigerPotential entity </returns>
        public VTigerPotential ReadOpportunity(string opportunityId)
        {
            VTigerPotential opportunity = new VTigerPotential();
            try
            {
                opportunity = this.vTigerService.Retrieve<VTigerPotential>(opportunityId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Retrieving Opportunity :- " + ex.Message);
            }

            return opportunity;
        }

        /// <summary>
        ///  Updating Opportunity in CRM
        /// </summary>
        /// <param name="opportunity"> The Opportunity entity. </param>
        /// <returns> VTigerPotential entity </returns>
        public VTigerPotential UpdateOpportunity(VTigerPotential opportunity)
        {
            try
            {
                opportunity = this.vTigerService.Update<VTigerPotential>(opportunity);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Updating Opportunity :- " + ex.Message);
            }

            return opportunity;
        }

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
        public List<VTigerPotential> GetOpportunities(string companyId, int lastpageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
        {
            List<VTigerPotential> lst = new List<VTigerPotential>();

            lastpageId = lastpageId < 1 ? 1 : lastpageId;
            var startVal = (lastpageId - 1) * fetchCount;

            try
            {
                string query = "select * from Potentials where related_to = '" + companyId + "' and cf_841 = 1 and cf_855 = 0 and cf_883 = 0 order by potential_no desc LIMIT " + startVal + " , " + fetchCount + ";";
                lst = this.leadService.GetQueryResult<VTigerPotential>(query).OrderByDescending(x => x.createdtime).ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetOpportunities :- " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Gets the opportunity.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <returns>VTigerPotential object.</returns>
        public VTigerPotential GetOpportunity(string companyId, string userId, string interest)
        {
            VTigerPotential pot = new VTigerPotential();

            try
            {
                string query = "select * from Potentials where related_to = '" + companyId + "' and contact_id = '" + userId + "' and cf_853 = '" + interest + "';";
                pot = this.leadService.GetQueryResult<VTigerPotential>(query).FirstOrDefault();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Get Opportunity :- " + ex.Message);
            }

            return pot;
        }

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
        public List<VtigerPotentialData> GetMyClients(string crmOrgId, string searchText, int lastpageId, int fetchCount, string sortBy, string direction)
        {
            List<VtigerPotentialData> lst = new List<VtigerPotentialData>();

            lastpageId = lastpageId < 1 ? 1 : lastpageId;
            var startVal = (lastpageId - 1) * fetchCount;

            try
            {
                using (MySqlContext context = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(context);
                    lst = tmp.SqlQuery<VtigerPotentialData>("CALL GetMyClients({0}, {1}, {2}, {3} );", crmOrgId, searchText, startVal, fetchCount).ToList();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMyClients :- " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Gets the owner companies.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of VTigerPotential object.</returns>
        public List<VTigerPotential> GetOwnerCompanies(string userId)
        {
            List<VTigerPotential> lst = new List<VTigerPotential>();
            try
            {
                string query = "select * from Potentials where contact_id = '" + userId + "' and cf_841 = 1 and cf_855 = 0 AND cf_883 = 0 order by createdtime desc ;";
                lst = this.leadService.GetQueryResult<VTigerPotential>(query).ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetOwnerCompanies :- " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Marks the not call.
        /// </summary>
        /// <param name="lst">The lst.</param>
        /// <returns>Boolean object.</returns>
        public bool MarkNotCall(List<VTigerPotential> lst)
        {
            bool isSuccess = true;
            try
            {
                foreach (var item in lst)
                {
                    VTigerPotential pot = this.vTigerService.Retrieve<VTigerPotential>(item.id);
                    pot.cf_843 = true;
                    this.vTigerService.Update(pot);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("MarkNotCall :- " + ex.Message);
            }

            return isSuccess;
        }

        /// <summary>
        /// Reports the company.
        /// </summary>
        /// <param name="lst">The lst.</param>
        /// <returns>Boolean object.</returns>
        public bool ReportCompany(List<VTigerPotential> lst)
        {
            bool isSuccess = true;
            try
            {
                foreach (var item in lst)
                {
                    VTigerPotential pot = this.vTigerService.Retrieve<VTigerPotential>(item.id);
                    pot.cf_855 = true;
                    this.vTigerService.Update(pot);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("ReportCompany :- " + ex.Message);
            }

            return isSuccess;
        }

        /// <summary>
        /// Reports the user.
        /// </summary>
        /// <param name="lst">The lst.</param>
        /// <returns>Boolean object.</returns>
        public bool ReportUser(List<VTigerPotential> lst)
        {
            bool isSuccess = true;
            try
            {
                foreach (var item in lst)
                {
                    VTigerPotential pot = this.vTigerService.Retrieve<VTigerPotential>(item.id);
                    pot.cf_883 = true;
                    this.vTigerService.Update(pot);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("ReportUser :- " + ex.Message);
            }

            return isSuccess;
        }

        /// <summary>
        ///  Deleting Opportunity from CRM
        /// </summary>
        /// <param name="opportunityId"> The Opportunity id. </param>
        /// <returns> bool object </returns>
        public bool DeleteOpportunity(string opportunityId)
        {
            bool isSuccess = true;
            try
            {
                VTigerPotential pot = this.vTigerService.Retrieve<VTigerPotential>(opportunityId);
                pot.cf_841 = false;
                this.vTigerService.Update(pot);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("DeleteOpportunity :- " + ex.Message);
            }

            return isSuccess;
        }

        /// <summary>
        /// Gets the unreviewd clients.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastpageId">The last page id. </param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>List of VTigerPotential object.</returns>
        public List<VTigerPotential> GetUnReviewdClients(string companyId, int lastpageId, int fetchCount)
        {
            lastpageId = lastpageId < 1 ? 1 : lastpageId;
            var startVal = (lastpageId - 1) * fetchCount;

            List<VTigerPotential> lst = new List<VTigerPotential>();
            string orgCRMId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;

            try
            {
                List<string> reviewedUsersCRMId = this.userReviewService.GetReviewedUserIDs(orgCRMId);
                var fieldName = " contact_id ";
                reviewedUsersCRMId = reviewedUsersCRMId.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                if (reviewedUsersCRMId.Any())
                {
                    string param = reviewedUsersCRMId.Aggregate((a, b) => Convert.ToString(a) + "'  and  " + fieldName + " != '" + Convert.ToString(b));
                    var query = "select * from Potentials where  contact_id != '" + param + "' and cf_841 = 1 and cf_855 = 0 and cf_883 = 0 and related_to = " + orgCRMId + " LIMIT " + startVal + ", " + fetchCount + ";";
                    lst = this.leadService.GetQueryResult<VTigerPotential>(query).ToList();
                }
                else
                {
                    var query = "select * from Potentials where cf_841 = 1 and cf_855 = 0 and cf_883 = 0 and related_to = " + orgCRMId + " LIMIT " + startVal + ", " + fetchCount + ";";
                    lst = this.leadService.GetQueryResult<VTigerPotential>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetUnReviewdClients :- " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Gets the unreviewd clients.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="searchText"> The searchText </param>
        /// <param name="lastpageId">The last page id. </param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>List of VtigerPotentialData object.</returns>
        public List<VtigerPotentialData> GetUnReviewdClients1(string companyId, string searchText, int lastpageId, int fetchCount)
        {
            lastpageId = lastpageId < 1 ? 1 : lastpageId;
            var startVal = (lastpageId - 1) * fetchCount;

            List<VtigerPotentialData> lst = new List<VtigerPotentialData>();
            string orgCrmId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;

            try
            {
                using (MySqlContext context = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(context);
                    lst = tmp.SqlQuery<VtigerPotentialData>("CALL GetUnReviewdClients({0}, {1}, {2}, {3} );", orgCrmId, searchText, startVal, fetchCount).ToList();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetUnReviewdClients :- " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Gets the Count ofunreviewd clients.
        /// </summary>
        /// <param name="companyId">The company identifier.</param> 
        /// <param name="searchText"> The searchText </param>
        /// <returns> Result Count.</returns>
        public int GetUnReviewdClientsCount(string companyId, string searchText)
        {
            int count = 0;
            string orgCrmId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;
            try
            {
                using (MySqlContext context = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(context);
                    count = tmp.SqlQuery<int>("CALL GetUnReviewdClientsCount({0}, {1});", orgCrmId, searchText).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetUnReviewdClients :- " + ex.Message);
            }

            return count;
        }

        /// <summary>
        /// Gets the purchased user total amount.
        /// </summary>
        /// <param name="crmContactId">The CRM contact identifier.</param>
        /// <returns>Decimal value</returns>
        public decimal GetPurchasedUserTotalAmount(string crmContactId)
        {
            decimal total = 0;
            try
            {
                string query = "select * from Potentials where contact_id = '" + crmContactId + "';";
                var lst = this.leadService.GetQueryResult<VTigerPotential>(query).ToList();
                total = Convert.ToDecimal(lst.Sum(t => t.amount));
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetPurchasedUserTotalAmount - " + ex.Message);
            }

            return total;
        }

        /// <summary>
        /// Checks if user blocked.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>Bool object</returns>
        public bool CheckIfUserBlocked(string contactId, string orgId)
        {
            bool isBlocked = false;
            try
            {
                string query = "select * from Potentials where contact_id = '" + contactId + "' and related_to = '" + orgId + "' and cf_883 = 1;";
                var lst = this.leadService.GetQueryResult<VTigerPotential>(query).ToList();

                isBlocked = lst.Count > 0;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("CheckIfUserBlocked - " + ex.Message);
            }

            return isBlocked;
        }
    }
}
