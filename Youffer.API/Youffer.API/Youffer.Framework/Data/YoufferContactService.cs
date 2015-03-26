// ---------------------------------------------------------------------------------------------------
// <copyright file="YoufferContactService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-21</date>
// <summary>
//     The YoufferContactService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The YoufferContactService class
    /// </summary>
    public class YoufferContactService : IYoufferContactService
    {
        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The Contact Lead Repository.
        /// </summary>
        private readonly IRepository<ContactLeadMapping> contactLeadRepository;

        /// <summary>
        /// The application user
        /// </summary>
        private readonly IRepository<ApplicationUser> appUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="YoufferContactService" /> class.
        /// </summary>
        /// <param name="loggerService"> The logger service. </param>
        /// <param name="contactLeadRepository"> The Contact Lead Repository. </param> 
        /// <param name="mapperFactory"> The mapper factory. </param> 
        /// <param name="appUser"> The ApplicationUser repository. </param>
        public YoufferContactService(ILoggerService loggerService, IRepository<ContactLeadMapping> contactLeadRepository, IMapperFactory mapperFactory, IRepository<ApplicationUser> appUser)
        {
            this.LoggerService = loggerService;
            this.contactLeadRepository = contactLeadRepository;
            this.mapperFactory = mapperFactory;
            this.appUser = appUser;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Get Mapping details on the basis of contact Id.
        /// </summary>
        /// <param name="contactId"> The contact id. </param>
        /// <returns> ContactLeadMappingDto object </returns>
        public ContactLeadMappingDto GetMappingEntryByContactId(string contactId)
        {
            ContactLeadMapping map = new ContactLeadMapping();
            try
            {
                map = this.contactLeadRepository.Find(x => x.ContactId == contactId && !x.IsDeleted && x.IsActive).FirstOrDefault() ?? new ContactLeadMapping();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMappingEntryByContactId YoufferContact:- " + ex.Message);
            }

            return this.mapperFactory.GetMapper<ContactLeadMapping, ContactLeadMappingDto>().Map(map);
        }

        /// <summary>
        /// Get Mapping details on the basis of CRM contact Id.
        /// </summary>
        /// <param name="contactCRMId"> The CRM contact id. </param>
        /// <returns> ContactLeadMappingDto object </returns>
        public ContactLeadMappingDto GetMappingEntryByCrMContactId(string contactCRMId)
        {
            ContactLeadMapping map = new ContactLeadMapping();
            try
            {
                map = this.contactLeadRepository.Find(x => x.ContactCRMId == contactCRMId && !x.IsDeleted && x.IsActive).FirstOrDefault() ?? new ContactLeadMapping();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMappingEntryByCrMContactId :- " + ex.Message);
            }

            return this.mapperFactory.GetMapper<ContactLeadMapping, ContactLeadMappingDto>().Map(map);
        }

        /// <summary>
        /// Gets the mapping entry by CRM lead identifier.
        /// </summary>
        /// <param name="leadCRMId">The lead CRM identifier.</param>
        /// <returns>
        /// ContactLeadMappingDto object.
        /// </returns>
        public ContactLeadMappingDto GetMappingEntryByCrmLeadId(string leadCRMId)
        {
            ContactLeadMapping map = new ContactLeadMapping();
            try
            {
                map = this.contactLeadRepository.Find(x => x.LeadId == leadCRMId && !x.IsDeleted && x.IsActive).FirstOrDefault() ?? new ContactLeadMapping();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMappingEntryByCrMContactId :- " + ex.Message);
            }

            return this.mapperFactory.GetMapper<ContactLeadMapping, ContactLeadMappingDto>().Map(map);
        }

        /// <summary>
        /// Makes the mapping entry.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="leadId">The lead identifier.</param>
        /// <param name="createNewLead">if set to <c>true</c> [create new lead].</param>
        /// <returns>
        /// ContactLeadMappingDto object.
        /// </returns>
        public ContactLeadMappingDto MakeMappingEntry(string contactId, string leadId, bool createNewLead)
        {
            ContactLeadMappingDto res = new ContactLeadMappingDto();
            try
            {
                object[] sqlCol = { new SqlParameter("@ContactId", contactId), new SqlParameter("@LeadId", leadId), new SqlParameter("@CreateNewLead", createNewLead) };
                res = this.contactLeadRepository.SqlQuery<ContactLeadMappingDto>("InsContactLeadMapping @ContactId, @LeadId, @CreateNewLead ", sqlCol).FirstOrDefault();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("MakeMappingEntry :- " + ex.Message);
            }

            return res;
        }

        /// <summary>
        /// Gets the org CRM identifier.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>ApplicationUserDto object.</returns>
        public ApplicationUserDto GetOrgCRMId(string companyId)
        {
            ApplicationUser appUser = new ApplicationUser();
            try
            {
                appUser = this.appUser.Find(x => x.Id == companyId).FirstOrDefault() ?? new ApplicationUser();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetOrgCRMId :- " + ex.Message);
            }

            return this.mapperFactory.GetMapper<ApplicationUser, ApplicationUserDto>().Map(appUser);
        }

        /// <summary>
        /// Gets the org identifier from CRM identifier.
        /// </summary>
        /// <param name="orgCRMId">The org CRM identifier.</param>
        /// <returns>ApplicationUserDto object.</returns>
        public ApplicationUserDto GetOrgIdFromCRMId(string orgCRMId)
        {
            ApplicationUser appUser = new ApplicationUser();
            try
            {
                appUser = this.appUser.Find(x => x.CRMId == orgCRMId).FirstOrDefault() ?? new ApplicationUser();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetOrgIdFromCRMId :- " + ex.Message);
            }

            return this.mapperFactory.GetMapper<ApplicationUser, ApplicationUserDto>().Map(appUser);
        }
    }
}
