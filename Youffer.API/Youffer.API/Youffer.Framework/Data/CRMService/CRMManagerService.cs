// ---------------------------------------------------------------------------------------------------
// <copyright file="CRMManagerService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-20</date>
// <summary>
//     The CRMManagerService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data.CRMService
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Youffer.Common.CRMService;
    using Youffer.Common.DataService;
    using Youffer.Common.Helper;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.Common.MaxmindGeoIP2;
    using Youffer.Common.Notification;
    using Youffer.Common.SMS;
    using Youffer.CRM;
    using Youffer.Resources.Constants;
    using Youffer.Resources.CRMModel;
    using Youffer.Resources.Enum;
    using Youffer.Resources.Models;
    using Youffer.Resources.MySqlDbSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The CRMManagerService class
    /// </summary>
    public class CRMManagerService : ICRMManagerService
    {
        /// <summary>
        /// Gets the  Opportunity Service 
        /// </summary>
        private readonly IOpportunityService opportunityService;

        /// <summary>
        /// Gets the youffer contact Service 
        /// </summary>
        private readonly IYoufferContactService youfferContactService;

        /// <summary>
        /// Gets the youffer lead Service 
        /// </summary>
        private readonly IYoufferLeadService youfferLeadService;

        /// <summary>
        /// Gets the  Lead Service 
        /// </summary>
        private readonly ILeadService leadService;

        /// <summary>
        /// Gets the  Contact Service 
        /// </summary>
        private readonly IContactService contactService;

        /// <summary>
        /// Gets the  Organisation Service 
        /// </summary>
        private readonly IOrganisationService organisationService;

        /// <summary>
        /// Gets the user Service 
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The youffer interest service
        /// </summary>
        private readonly IYoufferInterestService youfferInterestService;

        /// <summary>
        /// The note service
        /// </summary>
        private readonly INoteService noteService;

        /// <summary>
        /// The note service
        /// </summary>
        private readonly IUserReviewService userReviewService;

        /// <summary>
        /// The youffer payment service
        /// </summary>
        private readonly IYoufferPaymentService youfferPaymentService;

        /// <summary>
        /// The Push Notification Service
        /// </summary>
        private readonly IPushMessageService pushService;

        /// <summary>
        /// The SMS service
        /// </summary>
        private readonly ISmsService smsService;

        /// <summary>
        /// The SMS service
        /// </summary>
        private readonly IYoufferSmsService youfferSmsService;

        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// The contact us service
        /// </summary>
        private readonly IContactUsService contactUsService;

        /// <summary>
        /// The request payment service
        /// </summary>
        private readonly IRequestPaymentService requestPaymentService;

        /// <summary>
        /// The ip2location service
        /// </summary>
        private readonly IIP2LocationService ip2LocationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CRMManagerService" /> class.
        /// </summary>
        /// <param name="loggerService"> The logger service. </param>
        /// <param name="opportunityService"> The Oppertunity Service. </param>
        /// <param name="leadService"> The lead service. </param>
        /// <param name="contactService"> The contact service. </param>
        /// <param name="organisationService"> The Organistaion service. </param>
        /// <param name="mapperFactory"> the mapper factory. </param>
        /// <param name="userService"> The user service. </param>
        /// <param name="youfferContactService"> the youffer contact service. </param>
        /// <param name="youfferLeadService"> the youffer lead service. </param>
        /// <param name="youfferInterestService"> The interest service</param>
        /// <param name="noteService"> the note service</param>
        /// <param name="userReviewService">The User Review Service</param>
        /// <param name="youfferPaymentService"> the youffer payment service. </param>
        /// <param name="pushService"> the push notification service. </param>
        /// <param name="smsService"> the sms service</param>
        /// <param name="youfferSmsService"> the youffer smsService</param>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="contactUsService">The ContactUs service.</param>
        /// <param name="requestPaymentService">The RequestPayment service.</param>
        /// <param name="ip2LocationService">The IP2Location service.</param>
        public CRMManagerService(
            ILoggerService loggerService,
            IOpportunityService opportunityService,
            ILeadService leadService,
            IContactService contactService,
            IOrganisationService organisationService,
            IMapperFactory mapperFactory,
            IUserService userService,
            IYoufferContactService youfferContactService,
            IYoufferLeadService youfferLeadService,
            IYoufferInterestService youfferInterestService,
            INoteService noteService,
            IUserReviewService userReviewService,
            IYoufferPaymentService youfferPaymentService,
            IPushMessageService pushService,
            ISmsService smsService,
            IYoufferSmsService youfferSmsService,
            INotificationService notificationService,
            IContactUsService contactUsService,
            IRequestPaymentService requestPaymentService,
            IIP2LocationService ip2LocationService)
        {
            this.opportunityService = opportunityService;
            this.organisationService = organisationService;
            this.contactService = contactService;
            this.leadService = leadService;
            this.LoggerService = loggerService;
            this.mapperFactory = mapperFactory;
            this.userService = userService;
            this.youfferContactService = youfferContactService;
            this.youfferLeadService = youfferLeadService;
            this.youfferInterestService = youfferInterestService;
            this.noteService = noteService;
            this.userReviewService = userReviewService;
            this.youfferPaymentService = youfferPaymentService;
            this.pushService = pushService;
            this.smsService = smsService;
            this.youfferSmsService = youfferSmsService;
            this.notificationService = notificationService;
            this.contactUsService = contactUsService;
            this.requestPaymentService = requestPaymentService;
            this.ip2LocationService = ip2LocationService;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        #region CRM Contacts

        /// <summary>
        /// Add Contacts
        /// </summary>
        /// <param name="contactInfo"> The contact Model. </param>
        /// <param name="appUser"> The application user. </param>
        /// <returns> Contact Model </returns>
        public ContactModel AddContact(ContactModel contactInfo, ApplicationUserDto appUser = null)
        {
            try
            {
                var contact = this.mapperFactory.GetMapper<ContactModel, VTigerContact>().Map(contactInfo);
                var contactDetail = this.contactService.CreateContact(contact);
                contactInfo = this.mapperFactory.GetMapper<VTigerContact, ContactModel>().Map(contactDetail);

                if (appUser != null)
                {
                    appUser.CRMId = contactInfo.Id;
                    this.userService.UpdateUser(appUser);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Add Contact :- " + ex.Message);
            }

            return contactInfo;
        }

        /// <summary>
        /// Get the contact from the CRM 
        /// </summary>
        /// <param name="contactId"> The Contact Id. </param>
        /// <returns> Contact Model </returns>
        public ContactModel GetContact(string contactId)
        {
            ContactModel contactInfo = new ContactModel();
            try
            {
                string crmContactId = this.userService.GetContact(contactId).CRMId;
                if (!string.IsNullOrEmpty(crmContactId))
                {
                    var contactDetail = this.contactService.ReadContact(crmContactId);
                    contactInfo = this.mapperFactory.GetMapper<VTigerContact, ContactModel>().Map(contactDetail);

                    var leadId = this.youfferContactService.GetMappingEntryByContactId(contactId).LeadId;
                    if (!string.IsNullOrEmpty(leadId))
                    {
                        var lastLead = this.leadService.ReadLead(leadId);
                        var lastLeadVal = this.mapperFactory.GetMapper<VTigerLead, ContactModel>().Map(lastLead);
                        contactInfo.MainInterest = lastLeadVal.MainInterest;
                        contactInfo.SubInterest = lastLeadVal.SubInterest;
                    }
                }
                else
                {
                    this.LoggerService.LogException("Get Contact :- No contact found for contact id " + contactId);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Get Contact :- " + ex.Message);
            }

            return contactInfo;
        }

        /// <summary>
        /// Updates the contact.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="contactInfo">The contact information.</param>
        /// <returns>
        /// ContactModel object.
        /// </returns>
        public ContactModel UpdateContact(string userId, ContactModel contactInfo)
        {
            try
            {
                string crmContactId = this.userService.GetContact(userId).CRMId;
                if (!string.IsNullOrEmpty(crmContactId))
                {
                    var leadId = this.youfferContactService.GetMappingEntryByContactId(userId).LeadId;
                    contactInfo.Id = crmContactId;
                    string[] lastSubInterest = new string[] { };
                    ContactModel lastLeadVal = new ContactModel();
                    var lastLead = new VTigerLead();
                    if (!string.IsNullOrEmpty(leadId))
                    {
                        lastLead = this.leadService.ReadLead(leadId);
                        lastLeadVal = this.mapperFactory.GetMapper<VTigerLead, ContactModel>().Map(lastLead);
                        lastSubInterest = lastLeadVal.SubInterest;
                    }

                    var contact = this.mapperFactory.GetMapper<ContactModel, VTigerContact>().Map(contactInfo);
                    var contactDetail = this.contactService.UpdateContact(contact);
                    if (contactDetail == null)
                    {
                        return null;
                    }

                    var lead = this.mapperFactory.GetMapper<ContactModel, VTigerLead>().Map(contactInfo);
                    lead.id = leadId;
                    VTigerLead leadInfo = new VTigerLead();
                    leadInfo = lastLead;
                    bool isSubIntSame = false;

                    if (contactInfo.SubInterest == null)
                    {
                        isSubIntSame = true;
                    }
                    else
                    {
                        isSubIntSame = !lastSubInterest.Except(contactInfo.SubInterest).Any() && !contactInfo.SubInterest.Except(lastSubInterest).Any();
                    }

                    if (!string.IsNullOrEmpty(leadId) && (contactInfo.SubInterest != null && contactInfo.SubInterest.Length == 0))
                    {
                        // Deactivate last lead
                        lastLead.cf_767 = false;
                        lastLead.cf_769 = lastLead.cf_771 = new string[] { };
                        leadInfo = this.leadService.UpdateLead(lastLead);

                        this.youfferContactService.MakeMappingEntry(userId, lead.id, false);
                    }
                    else if ((!isSubIntSame && !string.IsNullOrEmpty(leadId)) || !string.IsNullOrEmpty(leadId))
                    {
                        ////Getting leads main interest from DB based on its sub interest
                        string subInterestList = lead.cf_771.Aggregate((a, b) => Convert.ToString(a) + "," + Convert.ToString(b));
                        string[] mainInterest = this.youfferInterestService.GetMainBusinessTypeFromSub(subInterestList).Select(x => x.ParentBusinessTypeName).Distinct().ToArray();

                        lead.cf_769 = mainInterest;
                        lead.cf_773 = crmContactId;
                        lead.cf_1032 = lastLead.cf_1032;
                        leadInfo = this.leadService.UpdateLead(lead);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(lead.id) && ((contactInfo.MainInterest != null && contactInfo.MainInterest.Length > 0) || (contactInfo.SubInterest != null && contactInfo.SubInterest.Length > 0)))
                        {
                            string subInterestList = lead.cf_771.Aggregate((a, b) => Convert.ToString(a) + "," + Convert.ToString(b));
                            string[] mainInterest = this.youfferInterestService.GetMainBusinessTypeFromSub(subInterestList).Select(x => x.ParentBusinessTypeName).Distinct().ToArray();
                            lead.cf_767 = true;
                            lead.cf_769 = mainInterest;
                            lead.cf_773 = crmContactId;
                            leadInfo = this.leadService.CreateLead(lead);
                            this.youfferContactService.MakeMappingEntry(userId, leadInfo.id, true);
                        }
                    }

                    contactInfo = this.mapperFactory.GetMapper<VTigerContact, ContactModel>().Map(contactDetail);
                    contactInfo.Id = userId;
                    contactInfo.MainInterest = leadInfo.cf_769;
                    contactInfo.SubInterest = leadInfo.cf_771;
                    contactInfo.IsActive = leadInfo.cf_767;
                }
                else
                {
                    this.LoggerService.LogException("Update Contact :- No contact found for contact id " + userId);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Update Contact :- " + ex.Message);
            }

            return contactInfo;
        }

        /// <summary>
        /// Delete User from Youffer and Conatct from the CRM.
        /// </summary>
        /// <param name="appUser"> The application user. </param>
        /// <returns> bool object </returns>
        public bool DeleteContact(ApplicationUserDto appUser)
        {
            try
            {
                appUser.IsDeleted = true;
                appUser = this.userService.UpdateUser(appUser);
                if (appUser.IsDeleted)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Add Contact :- " + ex.Message);
            }

            return false;
        }

        #endregion

        #region CRM Organisation

        /// <summary>
        /// Add Organisation
        /// </summary>
        /// <param name="organisationInfo"> The Organisation Model. </param>
        /// <param name="appUser"> The application user. </param>
        /// <returns> Organisation Model </returns>
        public OrganisationModel AddOrganisation(OrganisationModel organisationInfo, ApplicationUserDto appUser = null)
        {
            try
            {
                var org = this.mapperFactory.GetMapper<OrganisationModel, VTigerAccount>().Map(organisationInfo);
                var orgDetail = this.organisationService.CreateOrganistaion(org);
                organisationInfo = this.mapperFactory.GetMapper<VTigerAccount, OrganisationModel>().Map(orgDetail);

                if (appUser != null)
                {
                    appUser.CRMId = organisationInfo.Id;
                    this.userService.UpdateUser(appUser);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Add Organisation :- " + ex.Message);
            }

            return organisationInfo;
        }

        /// <summary>
        /// Gets the Organisation from the CRM 
        /// </summary>
        /// <param name="organisationId"> The Organisation Id. </param>
        /// <returns> Organisation Model </returns>
        public OrganisationModel GetOrganisation(string organisationId)
        {
            OrganisationModel organisationInfo = new OrganisationModel();
            try
            {
                string crmId = this.userService.GetContact(organisationId).CRMId;
                if (!string.IsNullOrEmpty(crmId))
                {
                    var organisationDetail = this.organisationService.ReadOrganisation(crmId);
                    organisationInfo = this.mapperFactory.GetMapper<VTigerAccount, OrganisationModel>().Map(organisationDetail);
                }
                else
                {
                    this.LoggerService.LogException("Get Organisation :- No Organisation found for Organisation id :" + organisationId);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Get Organisation :- " + ex.Message);
            }

            return organisationInfo;
        }

        /// <summary>
        /// Updates the organisation.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        /// <param name="orgInfo">The org information.</param>
        /// <returns>OrganisationModel object.</returns>
        public OrganisationModel UpdateOrganisation(string orgId, OrganisationModel orgInfo)
        {
            try
            {
                string crmId = this.userService.GetContact(orgId).CRMId;
                if (!string.IsNullOrEmpty(crmId))
                {
                    orgInfo.Id = crmId;
                    var org = this.mapperFactory.GetMapper<OrganisationModel, VTigerAccount>().Map(orgInfo);
                    var orgDetail = this.organisationService.UpdateOrganisation(org);
                    if (orgDetail == null)
                    {
                        return null;
                    }

                    orgInfo = this.mapperFactory.GetMapper<VTigerAccount, OrganisationModel>().Map(orgDetail);
                    orgInfo.Id = orgId;
                }
                else
                {
                    this.LoggerService.LogException("Update Organisation :- No Organisation found for Organisation id " + orgId);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Update Organisation :- " + ex.Message);
            }

            return orgInfo;
        }

        #endregion

        #region CRM Lead

        /// <summary>
        /// Gets the Lead from the CRM 
        /// </summary>
        /// <param name="leadId"> The Lead Id. </param>
        /// <returns> LeadModel Model </returns>
        public LeadModel GetLead(string leadId)
        {
            LeadModel leadInfo = new LeadModel();
            try
            {
                var leadDetail = this.leadService.ReadLead(leadId);
                leadInfo = this.mapperFactory.GetMapper<VTigerLead, LeadModel>().Map(leadDetail);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Get Lead :- " + ex.Message);
            }

            return leadInfo;
        }

        /// <summary>
        /// Update the lead in the CRM
        /// </summary>
        /// <param name="leadId">The Lead Id.</param>
        /// <param name="leadInfo">The Lead Entity.</param>
        /// <returns>The Lead Model</returns>
        public LeadModel UpdateLead(string leadId, LeadModel leadInfo)
        {
            try
            {
                var lead = this.mapperFactory.GetMapper<LeadModel, VTigerLead>().Map(leadInfo);
                var leadDetail = this.leadService.UpdateLead(lead);
                leadInfo = this.mapperFactory.GetMapper<VTigerLead, LeadModel>().Map(leadDetail);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Update Lead :- " + ex.Message);
            }

            return leadInfo;
        }

        /// <summary>
        /// Searches the leads.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns>List of VTigerLead object.</returns>
        public List<VTigerLead> SearchLeads(SearchModelDto searchModel)
        {
            List<VTigerLead> lst = this.leadService.SearchLeads(searchModel);
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
            return this.leadService.GetSearchedLeads(orgId, model);
        }

        /// <summary>
        /// Gets the dashboard.
        /// </summary>
        /// <param name="orgModel">The org model.</param>
        /// <param name="lastPageId">The last page identifier.</param>
        /// <param name="fetchCnt">The fetch count.</param>
        /// <param name="interest"> The interest name. </param>
        /// <returns>
        /// List of VTigerLead object.
        /// </returns>
        public List<VTigerLead> GetDashboard(OrganisationModel orgModel, int lastPageId, int fetchCnt, string interest)
        {
            List<VTigerLead> lst = this.leadService.GetDashboard(orgModel, lastPageId, fetchCnt, interest);
            return lst;
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
            List<VTigerDashBoardData> lst = this.leadService.GetNewDashboard(orgCRMId, lastPageId, fetchCnt);
            return lst;
        }

        /// <summary>
        /// Get the Query Result Based on Entity
        /// </summary>
        /// <typeparam name="T"> Type of entity</typeparam>
        /// <param name="interest"> The interest values</param>
        /// <param name="lastseenId"> The last seen id</param>
        /// <param name="fetchCount"> The fetch count</param>
        /// <returns> IEnumerable object</returns>
        public IEnumerable<T> GetHomePageLeads<T>(string interest, int lastseenId, int fetchCount) where T : VTigerEntity
        {
            var query = "select * from Leads where cf_767 = 1 LIMIT " + lastseenId + "," + fetchCount + ";";
            if (!string.IsNullOrWhiteSpace(interest))
            {
                query = "select * from Leads where cf_771 like '%" + interest + "%' and cf_767 = 1 LIMIT " + lastseenId + "," + fetchCount + ";";
            }

            return this.leadService.GetQueryResult<T>(query);
        }

        /// <summary>
        /// Get the Top Leads
        /// </summary>
        /// <param name="topLeadsDto">The top leads Dto.</param>
        /// <returns>List of VTigerDashBoardData</returns>
        public List<VTigerDashBoardData> GetTopLeads(TopLeadsDto topLeadsDto)
        {
            topLeadsDto.Country = this.ip2LocationService.GetCountryData(topLeadsDto.IPAddress).CountryName;
            return this.leadService.GetTopLeads(topLeadsDto);
        }

        /// <summary>
        /// Searches the client for review page.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastpageId">The last page id. </param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>List of VTigerPotential object.</returns>
        public List<VTigerPotential> SearchClientForReviewPage(string searchText, string companyId, int lastpageId, int fetchCount)
        {
            List<VTigerPotential> lst = this.opportunityService.GetUnReviewdClients(companyId, lastpageId, fetchCount);
            return lst;
        }

        /// <summary>
        /// Searches the client for my client page.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="companyId"> the company id. </param>
        /// <param name="lastpageId">The last page id.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>List of VTigerPotential object.</returns>
        public List<VTigerPotential> SearchClientForMyClientPage(string searchText, string companyId, int lastpageId, int fetchCount)
        {
            string orgCRMId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;
            List<VTigerPotential> lst = this.opportunityService.GetOpportunities(orgCRMId, lastpageId, fetchCount);
            return lst;
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
        public List<VtigerPotentialData> GetMyClients(string crmOrgId, string searchText = "", int lastpageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
        {
            return this.opportunityService.GetMyClients(crmOrgId, searchText, lastpageId, fetchCount, sortBy, direction);
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
            List<VTigerPotential> lst = this.opportunityService.GetUnReviewdClients(companyId, lastpageId, fetchCount);
            return lst;
        }

        /// <summary>
        /// Gets the unreviewd clients.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastpageId">The last page id. </param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="searchText"> The searchText </param>
        /// <returns>List of VtigerPotentialData object.</returns>
        public List<VtigerPotentialData> GetUnReviewdClients1(string companyId, int lastpageId, int fetchCount, string searchText = "")
        {
            return this.opportunityService.GetUnReviewdClients1(companyId, searchText, lastpageId, fetchCount);
        }

        /// <summary>
        /// Gets the Count of unreviewd clients.
        /// </summary>
        /// <param name="companyId">The company identifier.</param> 
        /// <param name="searchText"> The searchText </param>
        /// <returns> Result Count.</returns>
        public int GetUnReviewdClientsCount(string companyId, string searchText = "")
        {
            return this.opportunityService.GetUnReviewdClientsCount(companyId, searchText);
        }

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>List of StatisticsModelDto object.</returns>
        public List<StatisticsModelDto> GetStatistics(string companyId)
        {
            List<StatisticsModelDto> lstStatsModel = new List<StatisticsModelDto>();
            lstStatsModel = this.leadService.GetStatistics(companyId);

            return lstStatsModel;
        }

        /// <summary>
        /// Trending search.
        /// </summary>
        /// <returns>List of string.</returns>
        public List<string> TrendingSearch()
        {
            List<string> lst = new List<string>();
            lst = this.leadService.TrendingSearch();

            return lst;
        }

        #endregion

        #region CRM Opportunity

        /// <summary>
        /// Adds the opportunity.
        /// </summary>
        /// <param name="buyUserModelDto">The buy user model dto.</param>
        /// <returns>Boolean object.</returns>
        public bool AddOpportunity(BuyUserModelDto buyUserModelDto)
        {
            bool isSuccess = true;
            string crmContactId = this.youfferContactService.GetMappingEntryByContactId(buyUserModelDto.UserId).ContactCRMId;
            string crmLeadId = this.youfferContactService.GetMappingEntryByContactId(buyUserModelDto.UserId).LeadId;

            this.LoggerService.LogException("AddOpportunity CRMLeadId - " + crmLeadId);

            string crmOrgId = this.youfferContactService.GetOrgCRMId(buyUserModelDto.CompanyId).CRMId;

            LeadModel leadModel = this.GetLead(crmLeadId);

            this.LoggerService.LogException("AddOpportunity Lead -json- " + JsonConvert.SerializeObject(leadModel));

            OrganisationModel org = this.GetOrganisation(buyUserModelDto.CompanyId);
            double price = Convert.ToDouble(buyUserModelDto.Amount);

            var oppInfo = new OpportunityModel();
            try
            {
                var opp = this.mapperFactory.GetMapper<LeadModel, VTigerPotential>().Map(leadModel);
                opp.related_to = crmOrgId;
                opp.contact_id = crmContactId;
                opp.amount = price;
                opp.cf_853 = buyUserModelDto.Interest;
                opp.cf_841 = true;
                opp.cf_843 = true;
                opp.cf_1048 = buyUserModelDto.PurchasedFromCredit;
                opp.cf_1058 = buyUserModelDto.PurchasedFromCash;

                var oppDetail = this.opportunityService.CreateOpportunity(opp);
                oppInfo = this.mapperFactory.GetMapper<VTigerPotential, OpportunityModel>().Map(oppDetail);
                this.youfferLeadService.MakeMappingEntry(leadModel.Id, oppDetail.id, crmOrgId, price, buyUserModelDto.Interest, buyUserModelDto.UserId);

                if (!string.IsNullOrEmpty(leadModel.GCMId))
                {
                    this.pushService.SendPurchasedNotificationToAndroid(leadModel.GCMId, buyUserModelDto.CompanyId, org.AccountName, buyUserModelDto.Interest, "Purchased", Notification.buyuser.ToString());
                }

                if (!string.IsNullOrEmpty(leadModel.UDId))
                {
                    this.pushService.SendPurchasedNotificationToiOS(leadModel.UDId, buyUserModelDto.CompanyId, org.AccountName, "Purchased", Notification.buyuser.ToString());
                }

                CRMNotifications notification = new CRMNotifications()
                {
                    OrganisationsId = crmOrgId,
                    ContactId = crmContactId,
                    NotificationType = ConfigConstants.OrganisationPurchasedContact
                };

                this.CreateNotification(notification);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("AddOpportunity :- " + ex.Message);
            }

            return isSuccess;
        }

        /// <summary>
        /// Adds the opportunity.
        /// </summary>
        /// <param name="leadInfo">The lead information.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="price">The price.</param>
        /// <param name="payPalDetailsDto">The pay pal details dto.</param>
        /// <param name="appUser">The application user.</param>
        /// <returns>Boolean object.</returns>
        public bool AddOpportunity(LeadModel leadInfo, string companyId, string contactId, double price, PayPalDetailsDto payPalDetailsDto, ApplicationUserDto appUser = null)
        {
            bool isSuccess = true;
            var oppInfo = new OpportunityModel();
            try
            {
                var opp = this.mapperFactory.GetMapper<LeadModel, VTigerPotential>().Map(leadInfo);
                opp.related_to = companyId;
                opp.contact_id = contactId;
                opp.amount = price;
                opp.cf_853 = payPalDetailsDto.Interest;
                opp.cf_841 = true;
                opp.cf_843 = true;
                opp.cf_1048 = false;
                opp.cf_1058 = false;

                var oppDetail = this.opportunityService.CreateOpportunity(opp);
                oppInfo = this.mapperFactory.GetMapper<VTigerPotential, OpportunityModel>().Map(oppDetail);

                LeadOpportunityMappingDto leadOppMapping = this.youfferLeadService.MakeMappingEntry(leadInfo.Id, oppDetail.id, companyId, price, payPalDetailsDto.Interest, payPalDetailsDto.UserId);
                payPalDetailsDto.LeadOppMappingId = leadOppMapping.Id;
                this.youfferPaymentService.SavePayPalDetails(payPalDetailsDto);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("AddOpportunity PaypalDetails :- " + ex.Message);
            }

            return isSuccess;
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
            try
            {
                string orgCRMId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;
                lst = this.opportunityService.GetOpportunities(orgCRMId, lastpageId, fetchCount, sortBy, direction);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetOpportunities :- " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Determines whether the user can be purchased or not.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <returns>Bpplean object.</returns>
        public bool CanPurchaseUser(string companyId, string userId, string interest)
        {
            bool canPurchase = true;
            VTigerPotential pot = this.opportunityService.GetOpportunity(companyId, userId, interest);
            canPurchase = pot == null;

            return canPurchase;
        }

        /// <summary>
        /// Deletes the opportunity.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Boolean object
        /// </returns>
        public bool DeleteOpportunity(string userId)
        {
            bool isSuccess = true;
            this.opportunityService.DeleteOpportunity(userId);

            return isSuccess;
        }

        /// <summary>
        /// Gets the owner companies.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of OwnerCompaniesDto object.</returns>
        public List<OwnerCompaniesDto> GetOwnerCompanies(string userId)
        {
            List<OwnerCompaniesDto> lstOwnerCompaniesDto = new List<OwnerCompaniesDto>();
            try
            {
                string crmId = this.youfferContactService.GetMappingEntryByContactId(userId).ContactCRMId;
                if (!string.IsNullOrEmpty(crmId))
                {
                    List<VTigerPotential> lst = this.opportunityService.GetOwnerCompanies(crmId);

                    foreach (var item in lst)
                    {
                        string companyId = this.youfferContactService.GetOrgIdFromCRMId(item.related_to).Id;
                        OrganisationModel orgModel = new OrganisationModel();
                        orgModel = this.GetOrganisation(companyId);
                        lstOwnerCompaniesDto.Add(new OwnerCompaniesDto { CompanyId = companyId, CompanyName = orgModel.AccountName, ImageURL = orgModel.ImageURL, CanCall = item.cf_843, PurchasedOn = item.createdtime });
                    }

                    lstOwnerCompaniesDto = lstOwnerCompaniesDto.Distinct().GroupBy(x => x.CompanyId).Select(grp => grp.First()).ToList();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetOwnerCompanies :- " + ex.Message);
            }

            return lstOwnerCompaniesDto;
        }

        /// <summary>
        /// Marks the not call.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>Boolean object.</returns>
        public bool MarkNotCall(string contactId, string orgId)
        {
            bool isSuccess = true;
            try
            {
                var query = "select * from Potentials where contact_id = '" + contactId + "' and related_to = '" + orgId + "';";
                List<VTigerPotential> lst = this.leadService.GetQueryResult<VTigerPotential>(query).ToList();
                if (lst.Any())
                {
                    this.opportunityService.MarkNotCall(lst);
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
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>Boolean object.</returns>
        public bool ReportCompany(string contactId, string orgId)
        {
            bool isSuccess = true;
            try
            {
                var query = "select * from Potentials where contact_id = '" + contactId + "' and related_to = '" + orgId + "';";
                List<VTigerPotential> lst = this.leadService.GetQueryResult<VTigerPotential>(query).ToList();
                if (lst.Any())
                {
                    this.opportunityService.ReportCompany(lst);
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
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>Boolean object.</returns>
        public bool ReportUser(string contactId, string orgId)
        {
            bool isSuccess = true;
            try
            {
                var query = "select * from Potentials where contact_id = '" + contactId + "' and related_to = '" + orgId + "';";
                List<VTigerPotential> lst = this.leadService.GetQueryResult<VTigerPotential>(query).ToList();
                if (lst.Any())
                {
                    ////this.leadService.BlockLead(contactId);
                    this.opportunityService.ReportUser(lst);
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
        /// Mark user as purchased
        /// </summary>
        /// <param name="userId"> The user Id</param>
        /// <param name="contactId"> The contact Id</param>
        /// <param name="userName">The user name</param>
        /// <param name="organisationId"> The organisation Id</param>
        /// <param name="interestName"> The interest Name</param>
        /// <param name="leadId"> The leadId </param>
        /// <returns> bool object</returns>
        public bool MarkUserAsPurchased(string userId, string contactId, string userName, string organisationId, string interestName, string leadId)
        {
            try
            {
                //// Updating Lead by removing the subinterest
                ContactModel contact = this.GetContact(userId);
                if (contact.SubInterest != null && contact.SubInterest.Length > 0)
                {
                    contact.SubInterest = contact.SubInterest.Where(x => x != interestName).ToArray();
                }

                contact = this.UpdateContact(userId, contact);

                CRMUserReview review = new CRMUserReview();
                List<CRMUserReview> reviewList = this.ReadUserReviews(contactId, organisationId, interestName);
                if (reviewList.Any())
                {
                    review = reviewList.First();
                    review.Rating = 5;
                    review.FeedbackText = AppSettings.Get<string>(ConfigConstants.MarkPurchasedText);
                    this.UpdateUserReview(review);
                }
                else
                {
                    review.FeedbackText = AppSettings.Get<string>(ConfigConstants.MarkPurchasedText);
                    review.Rating = 5;
                    review.ContactId = contactId;
                    review.OrganisationsId = organisationId;
                    review.InterestName = interestName;
                    this.AddUserReview(review);
                }

                //// Update the oppoutunity as deal closed if there is any field available. 

                LeadOpportunityMappingDto leadOppMapping = this.youfferLeadService.GetMappingEntryByLeadAndOrgCRMIdAndInterest(leadId, organisationId, interestName);
                if (leadOppMapping.Id > 0)
                {
                    VTigerPotential pot = this.opportunityService.ReadOpportunity(leadOppMapping.OpportunityId);
                    pot.cf_857 = true;
                    this.opportunityService.UpdateOpportunity(pot);
                }

                this.SendNotificationAfterUserPurchased(userId, contactId, userName, organisationId, "User purchased", interestName);

                return true;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Mark as purchased :- " + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Gets the purchased user total amount.
        /// </summary>
        /// <param name="crmContactId">The CRM contact identifier.</param>
        /// <returns>Decimal value</returns>
        public decimal GetPurchasedUserTotalAmount(string crmContactId)
        {
            return this.opportunityService.GetPurchasedUserTotalAmount(crmContactId);
        }

        /// <summary>
        /// Checks if user blocked.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>Bool object</returns>
        public bool CheckIfUserBlocked(string contactId, string orgId)
        {
            return this.opportunityService.CheckIfUserBlocked(contactId, orgId);
        }

        #endregion

        #region Company Notes

        /// <summary>
        /// Add Company Notes
        /// </summary>
        /// <param name="notes"> the note </param>
        /// <returns> CRMCompanyNotes obj </returns>
        public CRMCompanyNotes AddCompanyNote(CRMCompanyNotes notes)
        {
            VTigerCompanyNotes note = this.noteService.CreateNote(notes);
            notes = this.mapperFactory.GetMapper<VTigerCompanyNotes, CRMCompanyNotes>().Map(note);
            return notes;
        }

        /// <summary>
        ///  Retrieving note from CRM
        /// </summary>
        /// <param name="contactId"> the user id. </param>
        /// <param name="companyId"> the company id. </param>
        /// <param name="lastSeenId"> The last seen Id. </param>
        /// <param name="fetchCount"> The fetch Count</param>
        /// <returns> CRMCompanyNotes list </returns>
        public List<CRMCompanyNotes> ReadNotes(string contactId, string companyId, int lastSeenId, int fetchCount)
        {
            List<VTigerCompanyNotes> noteList = this.noteService.ReadNotes(contactId, companyId, lastSeenId, fetchCount);
            List<CRMCompanyNotes> notes = this.mapperFactory.GetMapper<List<VTigerCompanyNotes>, List<CRMCompanyNotes>>().Map(noteList);
            return notes;
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> Boolean object </returns>
        public bool DeleteNote(string noteId)
        {
            return this.noteService.DeleteNote(noteId);
        }

        #endregion

        #region User reviews

        /// <summary>
        /// Create User review
        /// </summary>
        /// <param name="review">the CRMUserReview model</param>
        /// <returns> CRMUserReview obj </returns>
        public CRMUserReview AddUserReview(CRMUserReview review)
        {
            VTigerUserReviews userReview = this.mapperFactory.GetMapper<CRMUserReview, VTigerUserReviews>().Map(review);
            userReview = this.userReviewService.CreateUserReview(userReview);
            review = this.mapperFactory.GetMapper<VTigerUserReviews, CRMUserReview>().Map(userReview);
            return review;
        }

        /// <summary>
        /// Read user reviews
        /// </summary>
        /// <param name="contactId"> The contact Id. </param>
        /// <param name="organisationId"> The Organisation Id.</param>
        /// <param name="interest">The interest</param>
        /// <param name="lastPageId">The last page Id</param>
        /// <param name="fetchCount">the fetchCount</param>
        /// <returns> CRMUserReview list </returns>
        public List<CRMUserReview> ReadUserReviews(string contactId, string organisationId = "", string interest = "", int lastPageId = 0, int fetchCount = 100000)
        {
            List<VTigerUserReviews> userReviews = this.userReviewService.ReadUserReviews(contactId, organisationId, interest, lastPageId, fetchCount);
            List<CRMUserReview> userReviewList = this.mapperFactory.GetMapper<List<VTigerUserReviews>, List<CRMUserReview>>().Map(userReviews);
            return userReviewList;
        }

        /// <summary>
        /// Update user reviews
        /// </summary>
        /// <param name="review">the CRMUserReview model</param>
        /// <returns> CRMUserReview obj </returns>
        public CRMUserReview UpdateUserReview(CRMUserReview review)
        {
            VTigerUserReviews userReview = this.mapperFactory.GetMapper<CRMUserReview, VTigerUserReviews>().Map(review);
            userReview = this.userReviewService.UpdateUserReview(userReview);
            review = this.mapperFactory.GetMapper<VTigerUserReviews, CRMUserReview>().Map(userReview);
            return review;
        }

        /// <summary>
        /// Gett the user rank
        /// </summary>
        /// <param name="contactId"> The contact Id</param>
        /// <returns>rank of user</returns>
        public decimal GetUserRank(string contactId)
        {
            return this.userReviewService.GetUserRank(contactId);
        }

        /// <summary>
        /// Gets the user reviews.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The list of CRMUserReview model.</returns>
        public List<CRMUserReview> GetUserReviews(string userId)
        {
            List<VTigerUserReviews> userReviews = this.userReviewService.GetUserReviews(userId);
            List<CRMUserReview> userReviewList = this.mapperFactory.GetMapper<List<VTigerUserReviews>, List<CRMUserReview>>().Map(userReviews);
            return userReviewList;
        }

        /// <summary>
        /// Gets the reviews for company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastPageId">The lastPage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>List of CRMUserReview model.</returns>
        public List<CRMUserReview> GetReviewsForCompany(string companyId, int lastPageId, int fetchCount, string sortBy, string direction)
        {
            List<VTigerUserReviews> userReviews = this.userReviewService.GetReviewsForCompany(companyId, lastPageId, fetchCount, sortBy, direction);
            List<CRMUserReview> userReviewList = this.mapperFactory.GetMapper<List<VTigerUserReviews>, List<CRMUserReview>>().Map(userReviews);
            return userReviewList;
        }

        #endregion

        #region Notifications

        /// <summary>
        /// Create Notification
        /// </summary>
        /// <param name="crmNotification">The CRMNotifications model</param>
        /// <returns>The CRMNotifications object</returns>
        public CRMNotifications CreateNotification(CRMNotifications crmNotification)
        {
            VTigerNotifications notification = this.mapperFactory.GetMapper<CRMNotifications, VTigerNotifications>().Map(crmNotification);
            notification = this.notificationService.CreateNotification(notification);
            crmNotification = this.mapperFactory.GetMapper<VTigerNotifications, CRMNotifications>().Map(notification);
            return crmNotification;
        }

        #endregion

        #region ContactUs

        /// <summary>
        /// Create ContactUs Message
        /// </summary>
        /// <param name="crmContactUs">The CRMContactUs model</param>
        /// <returns>The CRMContactUs object</returns>
        public CRMContactUs CreateContactUsMessage(CRMContactUs crmContactUs)
        {
            VTigerContactUs contactUs = this.mapperFactory.GetMapper<CRMContactUs, VTigerContactUs>().Map(crmContactUs);
            contactUs = this.contactUsService.CreateContactUsMessage(contactUs);
            crmContactUs = this.mapperFactory.GetMapper<VTigerContactUs, CRMContactUs>().Map(contactUs);
            return crmContactUs;
        }

        /// <summary>
        /// Create ContactUs Message
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>The CRMContactUs object</returns>
        public CRMContactUs ReadContactUsMessage(string userId)
        {
            CRMContactUs crmContactUs = new CRMContactUs();
            VTigerContactUs contactUs = this.contactUsService.ReadContactUsMessage(userId);
            try
            {
                crmContactUs = this.mapperFactory.GetMapper<VTigerContactUs, CRMContactUs>().Map(contactUs);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex.Message);
            }

            return crmContactUs;
        }

        /// <summary>
        /// Reads all contact us message.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>List of CRMContactUs object.</returns>
        public List<CRMContactUs> ReadAllContactUsMessage(string userId, int lastpageId, int fetchCount, string sortBy, string direction)
        {
            List<CRMContactUs> lstCRMContactUs = new List<CRMContactUs>();
            List<VTigerContactUs> lstContactUs = this.contactUsService.ReadAllContactUsMessage(userId, lastpageId, fetchCount, sortBy, direction);
            try
            {
                lstCRMContactUs = this.mapperFactory.GetMapper<List<VTigerContactUs>, List<CRMContactUs>>().Map(lstContactUs);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex.Message);
            }

            return lstCRMContactUs;
        }

        /// <summary>
        /// Marks the contact us messages deleted.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Boolean object.</returns>
        public bool MarkContactUsMessagesDeleted(string userId)
        {
            return this.contactUsService.MarkContactUsMessagesDeleted(userId);
        }

        /// <summary>
        /// Checks if contact us MSG.
        /// </summary>
        /// <param name="msgId">The MSG identifier.</param>
        /// <returns>Boolean object.</returns>
        public bool CheckIfContactUsMsg(string msgId)
        {
            return this.contactUsService.CheckIfContactUsMsg(msgId);
        }

        /// <summary>
        /// Deletes the contactus message.
        /// </summary>
        /// <param name="msgId">The MSG identifier.</param>
        /// <returns>Boolean object.</returns>
        public bool DeleteContactusMessage(string msgId)
        {
            return this.contactUsService.DeleteContactusMessage(msgId);
        }

        #endregion

        #region RequestPayment

        /// <summary>
        /// Creates the payment request.
        /// </summary>
        /// <param name="crmRequestPayment">The CRM request payment.</param>
        /// <returns>CRMRequestPayment object.</returns>
        public CRMRequestPayment CreatePaymentRequest(CRMRequestPayment crmRequestPayment)
        {
            VTigerRequestPayment requestPayment = this.mapperFactory.GetMapper<CRMRequestPayment, VTigerRequestPayment>().Map(crmRequestPayment);
            requestPayment = this.requestPaymentService.CreatePaymentRequest(requestPayment);
            crmRequestPayment = this.mapperFactory.GetMapper<VTigerRequestPayment, CRMRequestPayment>().Map(requestPayment);
            return crmRequestPayment;
        }

        #endregion

        #region Common

        /// <summary>
        /// Gets the query result.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable object.</returns>
        public DataTable GetQueryResult(string query)
        {
            DataTable dt = this.leadService.GetQueryResult(query);
            return dt;
        }

        /// <summary>
        /// Get the Query Result Based on Entity
        /// </summary>
        /// <typeparam name="T"> Type of entity</typeparam>
        /// <param name="query"> The query values</param>
        /// <returns> IEnumerable object</returns>
        public IEnumerable<T> GetQueryResult<T>(string query) where T : VTigerEntity
        {
            return this.leadService.GetQueryResult<T>(query);
        }

        /// <summary>
        /// Gets the meta data.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="fieldIndex">Index of the field.</param>
        /// <returns>List of VTigerPicklistItem object.</returns>
        public List<VTigerPicklistItem> GetMetaData(VTigerType module, int fieldIndex)
        {
            List<VTigerPicklistItem> lst = this.leadService.GetMetaData(module, fieldIndex);
            return lst;
        }

        /// <summary>
        /// Updates the parent business types of organisations.
        /// </summary>
        /// <returns>Boolean object.</returns>
        public bool UpdateParentBusinessTypesOfOrganisations()
        {
            bool isSuccess = true;
            try
            {
                var query = "select * from Accounts limit 51,50;";
                List<VTigerAccount> lst = this.leadService.GetQueryResult<VTigerAccount>(query).ToList();
                foreach (var item in lst)
                {
                    OrganisationModel org = this.mapperFactory.GetMapper<VTigerAccount, OrganisationModel>().Map(item);

                    ////string[] subBusinessTypeArr = org.SubBusinessType[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    ////string subBusinessType = string.Empty;

                    string[] mainBusinessTypeArr = org.MainBusinessType[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    string mainBusinessType = string.Empty;

                    ////if (subBusinessTypeArr.Length > 0)
                    ////{
                    ////    subBusinessType = subBusinessTypeArr.Aggregate((a, b) => Convert.ToString(a) + "," + Convert.ToString(b));

                    ////    string[] mainBusinessType = this.youfferInterestService.GetMainBusinessTypeFromSub(subBusinessType).Select(x => x.ParentBusinessTypeName).Distinct().ToArray();
                    ////    org.MainBusinessType = mainBusinessType;

                    ////    VTigerAccount acc = this.mapperFactory.GetMapper<OrganisationModel, VTigerAccount>().Map(org);
                    ////    this.organisationService.UpdateOrganisation(acc);
                    ////}

                    if (mainBusinessTypeArr.Length > 0)
                    {
                        mainBusinessType = mainBusinessTypeArr.Aggregate((a, b) => Convert.ToString(a) + "," + Convert.ToString(b));

                        string[] subBusinessType = this.youfferInterestService.GetSubBusinessTypeFromMain(mainBusinessType).Select(x => x.BusinessTypeName).Distinct().ToArray();
                        org.SubBusinessType = subBusinessType;

                        VTigerAccount acc = this.mapperFactory.GetMapper<OrganisationModel, VTigerAccount>().Map(org);
                        this.organisationService.UpdateOrganisation(acc);
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("UpdateParentBusinessTypesOfOrganisations :- " + ex.Message);
            }

            return isSuccess;
        }

        /// <summary>
        /// Updates the parent interest of users.
        /// </summary>
        /// <returns>Boolean object.</returns>
        public bool UpdateParentInterestOfUsers()
        {
            bool isSuccess = true;

            try
            {
                var query = "select * from Leads limit 1, 100;";
                List<VTigerLead> lst = this.leadService.GetQueryResult<VTigerLead>(query).ToList();
                foreach (var item in lst)
                {
                    LeadModel lead = this.mapperFactory.GetMapper<VTigerLead, LeadModel>().Map(item);

                    string[] subInterestArr = lead.SubInterest[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    string subInterest = string.Empty;
                    if (subInterestArr.Length > 0)
                    {
                        subInterest = subInterestArr.Aggregate((a, b) => Convert.ToString(a) + "," + Convert.ToString(b));
                        string[] mainInterest = this.youfferInterestService.GetMainBusinessTypeFromSub(subInterest).Select(x => x.ParentBusinessTypeName).Distinct().ToArray();
                        lead.MainInterest = mainInterest;

                        VTigerLead vtigerLead = this.mapperFactory.GetMapper<LeadModel, VTigerLead>().Map(lead);
                        this.leadService.UpdateLead(vtigerLead);
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("UpdateParentInterestOfUsers :- " + ex.Message);
            }

            return isSuccess;
        }

        #endregion

        #region Push Notifications

        /// <summary>
        /// Send Notification to company
        /// </summary>
        /// <param name="userid">The user id.</param>
        /// <param name="contactId"> The contact id.</param>
        /// <param name="username">The user name.</param>
        /// <param name="orgId">The org id.</param>
        /// <param name="message">The message text.</param>
        /// <param name="interest">The interest.</param>
        /// <returns> bool object </returns>
        public async Task<bool> SendNotificationAfterUserPurchased(string userid, string contactId, string username, string orgId, string message, string interest)
        {
            string type = Notification.purchased.ToString();
            List<VTigerPotential> lst = this.opportunityService.GetOwnerCompanies(contactId);
            foreach (var opp in lst)
            {
                var organisationDetail = this.organisationService.ReadOrganisation(opp.related_to);
                OrganisationModel org = this.mapperFactory.GetMapper<VTigerAccount, OrganisationModel>().Map(organisationDetail);

                if (!string.IsNullOrWhiteSpace(org.GCMId))
                {
                    this.pushService.SendMarkPurchasedNotificationToAndroid(org.GCMId, org.AccountName, org.Id, username, userid, interest, type);
                }

                if (!string.IsNullOrWhiteSpace(org.UDId))
                {
                    this.pushService.SendMarkPurchasedNotificationToiOS(org.UDId, org.AccountName, org.Id, username, userid, interest, type);
                }
            }

            return true;
        }

        /// <summary>
        /// Send Verification Code
        /// </summary>
        /// <param name="userId">The user Id</param>
        /// <param name="phoneNumber"> The Phone Number</param>
        /// <param name="code">The code number</param>
        /// <param name="message">The message</param>
        /// <returns> bool Object</returns>
        public bool SendVerificationCode(string userId, string phoneNumber, string code, string message)
        {
            string messageSid = this.smsService.SendVerificationCode(phoneNumber, message);
            if (!string.IsNullOrWhiteSpace(messageSid))
            {
                SmsVerificationDto sms = new SmsVerificationDto()
                {
                    PhoneNumber = phoneNumber,
                    UserId = userId,
                    Code = code,
                    Message = message,
                    MessageSid = messageSid
                };

                sms = this.youfferSmsService.InsUpdSms(sms);
                return sms.Id > 0;
            }

            return false;
        }

        /// <summary>
        /// Check and Update phone number after code verification
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="code">The code</param>
        /// <param name="errorMessage"> The error Message</param>
        /// <returns>bool object</returns>
        public bool UpdatePhoneNumberAfterVerification(string userId, string code, out string errorMessage)
        {
            errorMessage = string.Empty;
            SmsVerificationDto sms = this.youfferSmsService.IsCodeVerified(userId, code);
            if (sms.Id > 0)
            {
                ContactModel contact = this.GetContact(userId);
                contact.Phone = sms.PhoneNumber;
                contact = this.UpdateContact(userId, contact);
                if (string.IsNullOrWhiteSpace(contact.Id))
                {
                    errorMessage = "Error in Updating Contact";
                    return false;
                }

                return true;
            }

            errorMessage = "Invalid Code";
            return false;
        }
        #endregion
    }
}
