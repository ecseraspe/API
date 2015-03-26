// ---------------------------------------------------------------------------------------------------
// <copyright file="YoufferLeadService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-28</date>
// <summary>
//     The YoufferLeadService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Class YoufferLeadService.
    /// </summary>
    public class YoufferLeadService : IYoufferLeadService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The Lead Opportunity Repository.
        /// </summary>
        private readonly IRepository<LeadOpportunityMapping> leadOpportunityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="YoufferLeadService" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="leadOpportunityRepository">The Lead Opportunity Repository.</param>
        /// <param name="mapperFactory">The Mapper factory.</param>
        public YoufferLeadService(ILoggerService loggerService, IRepository<LeadOpportunityMapping> leadOpportunityRepository, IMapperFactory mapperFactory)
        {
            this.LoggerService = loggerService;
            this.leadOpportunityRepository = leadOpportunityRepository;
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        /// <value>The logger service.</value>
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Gets the mapping entry by lead CRM identifier.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <returns>The LeadOpportunityMappingDto object.</returns>
        public LeadOpportunityMappingDto GetMappingEntryByLeadCRMId(string leadCRMId)
        {
            LeadOpportunityMapping map = new LeadOpportunityMapping();
            try
            {
                map = this.leadOpportunityRepository.Find(x => x.LeadId == leadCRMId).FirstOrDefault() ?? new LeadOpportunityMapping();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMappingEntryByLeadCRMId :- " + ex.Message);
            }

            return this.mapperFactory.GetMapper<LeadOpportunityMapping, LeadOpportunityMappingDto>().Map(map);
        }

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
        public LeadOpportunityMappingDto MakeMappingEntry(string leadId, string opportunityId, string companyId, double price, string interest, string userId)
        {
            LeadOpportunityMappingDto res = new LeadOpportunityMappingDto();
            try
            {
                object[] sqlCol = { new SqlParameter("@ContactId", userId), new SqlParameter("@LeadId", leadId), new SqlParameter("@OpportunityId", opportunityId), new SqlParameter("@CompanyId", companyId), new SqlParameter("@Price", price), new SqlParameter("@Interest", interest) };
                res = this.leadOpportunityRepository.SqlQuery<LeadOpportunityMappingDto>("InsLeadOpportunityMapping @ContactId, @LeadId, @OpportunityId, @CompanyId, @Price, @Interest", sqlCol).FirstOrDefault();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("MakeMappingEntry YoufferLead:- " + ex.Message);
            }

            return res;
        }

        /// <summary>
        /// Gets the mapping entry by lead and org CRM identifier.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <param name="orgCRMId">The org CRM identifier.</param>
        /// <returns>List of LeadOpportunityMappingDto object.</returns>
        public List<LeadOpportunityMappingDto> GetMappingEntryByLeadAndOrgCRMId(string leadCRMId, string orgCRMId)
        {
            List<LeadOpportunityMapping> lst = new List<LeadOpportunityMapping>();
            try
            {
                lst = this.leadOpportunityRepository.Find(x => x.LeadId == leadCRMId && x.CompanyId == orgCRMId).ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMappingEntryByLeadAndOrgCRMId :- " + ex.Message);
            }

            return this.mapperFactory.GetMapper<List<LeadOpportunityMapping>, List<LeadOpportunityMappingDto>>().Map(lst);
        }

        /// <summary>
        /// Gets the mapping entry by lead and org CRM identifier and interest.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <param name="orgCRMId">The org CRM identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <returns>LeadOpportunityMappingDto object.></returns>
        public LeadOpportunityMappingDto GetMappingEntryByLeadAndOrgCRMIdAndInterest(string leadCRMId, string orgCRMId, string interest)
        {
            LeadOpportunityMapping leadOppMapping = new LeadOpportunityMapping();
            try
            {
                leadOppMapping = this.leadOpportunityRepository.Find(x => x.LeadId == leadCRMId && x.CompanyId == orgCRMId && x.Interest == interest).FirstOrDefault() ?? new LeadOpportunityMapping();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMappingEntryByLeadAndOrgCRMIdAndInterest :- " + ex.Message);
            }

            return this.mapperFactory.GetMapper<LeadOpportunityMapping, LeadOpportunityMappingDto>().Map(leadOppMapping);
        }

        /// <summary>
        /// Gets the mapping entry by lead and org CRM identifier and interest.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <returns>LeadOpportunityMappingDto object.></returns>
        public LeadOpportunityMappingDto GetMappingEntryByLeadCRMIdAndInterest(string leadCRMId, string interest)
        {
            LeadOpportunityMapping leadOppMapping = new LeadOpportunityMapping();
            try
            {
                leadOppMapping = this.leadOpportunityRepository.Find(x => x.LeadId == leadCRMId & x.Interest == interest).FirstOrDefault() ?? new LeadOpportunityMapping();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMappingEntryByLeadCRMIdAndInterest :- " + ex.Message);
            }

            return this.mapperFactory.GetMapper<LeadOpportunityMapping, LeadOpportunityMappingDto>().Map(leadOppMapping);
        }

        /// <summary>
        /// Gets the mapping entry by user id and org CRM identifier and interest.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="orgCRMId">The org CRM identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <returns>LeadOpportunityMappingDto object.></returns>
        public LeadOpportunityMappingDto GetMappingEntryByContactAndOrgCRMIdAndInterest(string userId, string orgCRMId, string interest)
        {
            LeadOpportunityMapping leadOppMapping = new LeadOpportunityMapping();
            try
            {
                leadOppMapping = this.leadOpportunityRepository.Find(x => x.ContactId == userId && x.CompanyId == orgCRMId && x.Interest == interest).FirstOrDefault() ?? new LeadOpportunityMapping();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMappingEntryByContactAndOrgCRMIdAndInterest :- " + ex.Message);
            }

            return this.mapperFactory.GetMapper<LeadOpportunityMapping, LeadOpportunityMappingDto>().Map(leadOppMapping);
        }
    }
}
