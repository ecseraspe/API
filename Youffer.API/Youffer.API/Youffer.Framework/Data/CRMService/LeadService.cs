// ---------------------------------------------------------------------------------------------------
// <copyright file="LeadService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-19</date>
// <summary>
//     The LeadService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data.CRMService
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Common.CRMService;
    using CRM;
    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService;
    using Youffer.Resources.CRMModel;
    using Youffer.Resources.Enum;
    using Youffer.Resources.Models;
    using Youffer.Resources.MySqlDbSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The LeadService class
    /// </summary>
    public class LeadService : ILeadService
    {
        /// <summary>
        /// VTiger service instance
        /// </summary>
        private readonly IRepository<vtiger_contactdetails> vTigerMySql;

        /// <summary>
        /// VTiger service instance
        /// </summary>
        private readonly IVTigerService vTigerService;

        /// <summary>
        /// The youffer review service
        /// </summary>
        private readonly IYoufferContactService youfferContactService;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The user review service
        /// </summary>
        private readonly IUserReviewService userReviewService;

        /// <summary>
        ///  Initializes a new instance of the <see cref="LeadService" /> class.
        /// </summary>
        /// <param name="vTigerService">VTiger Service</param>
        /// <param name="loggerService">Logger Service</param>
        /// <param name="youfferContactService">YoufferContact Service.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="userReviewService">User Review Service.</param>
        /// <param name="vTigerMySql"> the vTigerMySql</param>
        public LeadService(IVTigerService vTigerService, ILoggerService loggerService, IYoufferContactService youfferContactService, IMapperFactory mapperFactory, IUserReviewService userReviewService, IRepository<vtiger_contactdetails> vTigerMySql)
        {
            this.vTigerService = vTigerService;
            this.LoggerService = loggerService;
            this.youfferContactService = youfferContactService;
            this.mapperFactory = mapperFactory;
            this.userReviewService = userReviewService;
            this.vTigerMySql = vTigerMySql;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        ///  Adding Lead into CRM
        /// </summary>
        /// <param name="lead"> the Lead entity. </param>
        /// <returns> VTigerLead entity </returns>
        public VTigerLead CreateLead(VTigerLead lead)
        {
            try
            {
                lead = this.vTigerService.Create<VTigerLead>(lead);
                lead.cf_769 = lead.cf_769 != null ? lead.cf_769[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
                lead.cf_771 = lead.cf_771 != null ? lead.cf_771[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Adding Lead :- " + ex.Message);
                lead = new VTigerLead();
            }

            return lead;
        }

        /// <summary>
        ///  Retrieving Lead from CRM
        /// </summary>
        /// <param name="leadId"> the Lead id. </param>
        /// <returns> VTigerLead entity </returns>
        public VTigerLead ReadLead(string leadId)
        {
            VTigerLead lead = new VTigerLead();
            try
            {
                lead = this.vTigerService.Retrieve<VTigerLead>(leadId);
                //// Making it array of string for mapping as array turns into string because of Jayrock.Json
                lead.cf_769 = lead.cf_769 != null ? lead.cf_769[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
                lead.cf_771 = lead.cf_771 != null ? lead.cf_771[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Retrieving Lead :- " + ex.Message);
            }

            return lead;
        }

        /// <summary>
        ///  Updating Lead in CRM
        /// </summary>
        /// <param name="lead">the Lead entity. </param>
        /// <returns> VTigerLead entity </returns>
        public VTigerLead UpdateLead(VTigerLead lead)
        {
            try
            {
                lead = this.vTigerService.Update<VTigerLead>(lead);
                lead.cf_769 = lead.cf_769 != null ? lead.cf_769[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
                lead.cf_771 = lead.cf_771 != null ? lead.cf_771[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Updating Lead :- " + ex.Message);
            }

            return lead;
        }

        /// <summary>
        /// Searches the leads.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns>List of VTigerLead object.</returns>
        public List<VTigerLead> SearchLeads(SearchModelDto searchModel)
        {
            List<VTigerLead> lst = new List<VTigerLead>();

            try
            {
                ////To Do : Review and Sorting
                StringBuilder sbQuery = new StringBuilder();
                List<string> paramArray = new List<string>();
                sbQuery.Append("select * from Leads ");
                if (searchModel != null)
                {
                    if (searchModel.AgeFrom > 0)
                    {
                        var date = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
                        var dob = date.AddYears(-searchModel.AgeFrom).ToString("yyyy-MM-dd");
                        paramArray.Add("cf_783 <= '" + dob + "'");
                    }

                    if (searchModel.AgeTo > 0)
                    {
                        var date = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
                        var dob = date.AddYears(-searchModel.AgeTo).ToString("yyyy-MM-dd");
                        paramArray.Add("cf_783 >= '" + dob + "'");
                    }

                    if (!string.IsNullOrEmpty(searchModel.Country))
                    {
                        paramArray.Add("country = '" + searchModel.Country + "' ");
                    }

                    if (!string.IsNullOrEmpty(searchModel.State))
                    {
                        paramArray.Add("state = '" + searchModel.State + "' ");
                    }

                    if (!string.IsNullOrEmpty(searchModel.Gender))
                    {
                        paramArray.Add("cf_785 = '" + searchModel.Gender + "' ");
                    }

                    if (!string.IsNullOrEmpty(searchModel.InterestName))
                    {
                        paramArray.Add("cf_769 like '%" + searchModel.InterestName + "%' ");
                    }

                    if (!string.IsNullOrEmpty(searchModel.SubInterestName))
                    {
                        paramArray.Add("cf_771 like '%" + searchModel.SubInterestName + "%' ");
                    }

                    if (!string.IsNullOrEmpty(searchModel.OnlyActiveClient.ToString()) && searchModel.OnlyActiveClient)
                    {
                        paramArray.Add("cf_803 = " + (searchModel.OnlyActiveClient ? 1 : 0) + " ");
                    }

                    if (!string.IsNullOrEmpty(searchModel.OnlyReviewedClient.ToString()) && searchModel.OnlyReviewedClient)
                    {
                        ////paramArray.Add("");
                    }

                    paramArray.Add("phone != ' ' ");

                    if (paramArray.Any())
                    {
                        string param = paramArray.Aggregate((a, b) => Convert.ToString(a) + " and " + Convert.ToString(b));

                        if (!string.IsNullOrWhiteSpace(param))
                        {
                            sbQuery.Append(" where ");
                            sbQuery.Append(param);
                        }
                    }

                    if (searchModel.SortByColumn != SortBy.Undefined && searchModel.SortDirection != SortDirection.Undefined)
                    {
                        sbQuery.Append(" order by " + searchModel.SortByColumn.ToString() + " " + searchModel.SortDirection.ToString());
                    }

                    searchModel.LastPageId = searchModel.LastPageId < 1 ? 1 : searchModel.LastPageId;
                    var startVal = (searchModel.LastPageId - 1) * searchModel.FetchCount;

                    sbQuery.Append(" LIMIT " + startVal + ", " + searchModel.FetchCount);
                }

                sbQuery.Append(";");

                lst = this.GetQueryResult<VTigerLead>(sbQuery.ToString()).Where(x => x.phone.Trim() != string.Empty).ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("SearchLeads :- " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Gets the dashboard.
        /// </summary>
        /// <param name="orgModel">The org model.</param>
        /// <param name="lastPageId">The last page identifier.</param>
        /// <param name="fetchCnt">The fetch count.</param>
        /// <param name="interest"> The interest name. </param>
        /// <returns>List of VTigerLead object</returns>
        public List<VTigerLead> GetDashboard(OrganisationModel orgModel, int lastPageId, int fetchCnt, string interest)
        {
            List<VTigerLead> lst = new List<VTigerLead>();
            lastPageId = lastPageId < 1 ? 1 : lastPageId;
            var startVal = (lastPageId - 1) * fetchCnt;

            try
            {
                if (!string.IsNullOrWhiteSpace(interest))
                {
                    string query = "select * from Leads where cf_771 like '%" + interest + "%' and cf_767 = 1 LIMIT " + startVal + " , " + fetchCnt + ";";
                    lst = this.GetQueryResult<VTigerLead>(query).ToList();
                }
                else
                {
                    if (orgModel.SubBusinessType != null)
                    {
                        string country = orgModel.BillCountry;
                        if (string.IsNullOrEmpty(country))
                        {
                            country = "United States";
                        }

                        ////To Do : Include Rating as well and implement paging
                        foreach (string interestName in orgModel.SubBusinessType)
                        {
                            string query = "select * from Leads where cf_771 like '%" + interestName + "%' and cf_767 = 1 and phone != ' ' and country = '" + country + "';";
                            List<VTigerLead> lstRec = this.GetQueryResult<VTigerLead>(query).Where(x => x.phone.Trim() != string.Empty).ToList();
                            lst.AddRange(lstRec);
                        }
                    }

                    lst = lst.Distinct().GroupBy(x => x.lead_no).Select(grp => grp.First()).ToList();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetDashboard :- " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        ///  Deleting Lead from CRM
        /// </summary>
        /// <param name="leadId"> the Lead id. </param>
        /// <returns> bool object </returns>
        public bool DeleteLead(string leadId)
        {
            try
            {
                this.vTigerService.Delete(leadId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Deleting Lead :- " + ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>List of StatisticsModelDto object.</returns>
        public List<StatisticsModelDto> GetStatistics(string companyId)
        {
            List<StatisticsModelDto> lstStatsModel = new List<StatisticsModelDto>();
            VTigerAccount acc = this.vTigerService.Retrieve<VTigerAccount>(companyId);
            string query = string.Empty;

            var lst = new List<VTigerLead>();
            try
            {
                List<DictModel> lstClients = new List<DictModel>();
                List<VTigerLead> leadList = new List<VTigerLead>();
                string[] subInt = acc.cf_777[0] != null ? acc.cf_777[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
                foreach (var item in subInt)
                {
                    query = "select * from Leads where phone != ' ' and country = '" + acc.bill_country + "' and cf_771 like '%" + item + "%' and createdtime <= '" + DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss") + "' and createdtime > '" + DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd hh:mm:ss") + "' and createdtime >= '" + acc.createdtime.ToString("yyyy-MM-dd hh:mm:ss") + "';";
                    IEnumerable<VTigerLead> dtLead = this.GetQueryResult<VTigerLead>(query).Where(x => x.phone.Trim() != string.Empty);
                    leadList.AddRange(dtLead.ToList());
                }

                lstClients = leadList.GroupBy(d => d.createdtime.ToShortDateString()).Select(g => new DictModel { Key = g.Key, Value = g.Count().ToString() }).ToList();

                query = "select * from Potentials where related_to = '" + acc.id + "' and cf_841 = 1 and cf_855 = 0 and cf_883 = 0 and createdtime <= '" + DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss") + "' and createdtime > '" + DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd hh:mm:ss") + "';";
                IEnumerable<VTigerPotential> dtPot = this.GetQueryResult<VTigerPotential>(query);
                List<DictModel> lstPurchasedClients = dtPot.ToList().GroupBy(d => d.createdtime.ToShortDateString()).Select(g => new DictModel { Key = g.Key, Value = g.Count().ToString() }).ToList();

                decimal avgRating = this.userReviewService.GetOrgAvgRating(acc.id);

                lstStatsModel.Add(new StatisticsModelDto { Clients = lstClients, PurchasedClients = lstPurchasedClients, AvgRating = avgRating });
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetStatistics :-  " + ex.Message);
            }

            return lstStatsModel;
        }

        /// <summary>
        /// Trending search.
        /// </summary>
        /// <returns>List of string.</returns>
        public List<string> TrendingSearch()
        {
            List<string> lst = new List<string>();
            try
            {
                string query = "select cf_771 from Leads;";
                List<VTigerLead> lstVTigerLead = this.GetQueryResult<VTigerLead>(query).ToList();

                foreach (var item in lstVTigerLead)
                {
                    string[] arr = item.cf_771 != null ? item.cf_771[0].Split(new string[] { " |##| " }, StringSplitOptions.None) : new string[] { };
                    for (int i = 0; i < arr.Length; i++)
                    {
                        lst.Add(arr[i]);
                    }
                }

                lst = lst.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("TrendingSearch :-  " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Gets the query result.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable object.</returns>
        public DataTable GetQueryResult(string query)
        {
            try
            {
                DataTable dt = this.vTigerService.Query(query);
                return dt;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Executing  Query :-" + query + ":- exception is :-  " + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Get the Query Result Based on Entity
        /// </summary>
        /// <typeparam name="T"> Type of entity</typeparam>
        /// <param name="query"> The query values</param>
        /// <returns> IEnumerable object</returns>
        public IEnumerable<T> GetQueryResult<T>(string query) where T : VTigerEntity
        {
            try
            {
                return this.vTigerService.Query<T>(query);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Executing  Query :-" + query + ":- exception is :-  " + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Gets the meta data.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="fieldIndex">Index of the field.</param>
        /// <returns>List of VTigerPicklistItem object.</returns>
        public List<VTigerPicklistItem> GetMetaData(VTigerType module, int fieldIndex)
        {
            VTigerType vType = (VTigerType)module;
            List<VTigerPicklistItem> lst = new List<VTigerPicklistItem>();
            try
            {
                VTigerObjectType vObjType = this.vTigerService.Describe(vType);
                lst = vObjType.fields[fieldIndex].type.picklistValues.ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMetaData :-  " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Blocks the lead.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <returns>bool object</returns>
        public bool BlockLead(string contactId)
        {
            try
            {
                string query = "select * from Leads where cf_773 = '" + contactId + "' and cf_767 = 1;";
                VTigerLead lead = this.GetQueryResult<VTigerLead>(query).FirstOrDefault();
                lead.cf_885 = true;
                this.vTigerService.Update<VTigerLead>(lead);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Blocking Lead :- " + ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// THe Dashboard data
        /// </summary>
        /// <param name="orgCRMId"> org Id</param>
        /// <param name="lastPageId"> last page id</param>
        /// <param name="fetchCnt">fetch count</param>
        /// <returns>List of VTigerDashBoardData</returns>
        public List<VTigerDashBoardData> GetNewDashboard(string orgCRMId, int lastPageId, int fetchCnt)
        {
            List<VTigerDashBoardData> lst = new List<VTigerDashBoardData>();
            lastPageId = lastPageId < 1 ? 1 : lastPageId;
            var startVal = (lastPageId - 1) * fetchCnt;
            try
            {
                using (MySqlContext context = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(context);
                    lst = tmp.SqlQuery<VTigerDashBoardData>("CALL GetDashboardData({0}, {1}, {2});", orgCRMId, startVal, fetchCnt).ToList();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Getting New Dashboard:- " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Get the Top Leads
        /// </summary>
        /// <param name="topLeadsDto">The top leads Dto.</param>
        /// <returns>List of VTigerDashBoardData</returns>
        public List<VTigerDashBoardData> GetTopLeads(TopLeadsDto topLeadsDto)
        {
            List<VTigerDashBoardData> lst = new List<VTigerDashBoardData>();
            topLeadsDto.LastPageId = topLeadsDto.LastPageId < 1 ? 1 : topLeadsDto.LastPageId;
            var startVal = (topLeadsDto.LastPageId - 1) * topLeadsDto.FetchCount;
            topLeadsDto.Interest = string.IsNullOrWhiteSpace(topLeadsDto.Interest) ? string.Empty : topLeadsDto.Interest;
            try
            {
                using (MySqlContext context = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(context);
                    lst = tmp.SqlQuery<VTigerDashBoardData>("CALL GetTopLeads({0}, {1}, {2}, {3});", topLeadsDto.Interest, startVal, topLeadsDto.FetchCount, topLeadsDto.Country).ToList();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Getting TopLeads:- " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Get the searched Leads
        /// </summary>
        /// <param name="orgId"> The org Id</param>
        /// <param name="model"> lThe search model</param> 
        /// <returns>List of VTigerDashBoardData</returns>
        public List<VTigerDashBoardData> GetSearchedLeads(string orgId, SearchModelDto model)
        {
            List<VTigerDashBoardData> lst = new List<VTigerDashBoardData>();
            model.LastPageId = model.LastPageId < 1 ? 1 : model.LastPageId;
            var startVal = (model.LastPageId - 1) * model.FetchCount;

            try
            {
                using (MySqlContext context = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(context);
                    lst = tmp.SqlQuery<VTigerDashBoardData>("CALL GetSearchedLeads({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11});", orgId, model.SubInterestName, model.Country, model.Gender, model.OnlyActiveClient, model.OnlyReviewedClient, model.AgeFrom, model.AgeTo, startVal, model.FetchCount, model.State, model.InterestName).ToList();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Getting TopLeads:- " + ex.Message);
            }

            return lst;
        }
    }
}
