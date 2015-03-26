// ---------------------------------------------------------------------------------------------------
// <copyright file="CommonService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-28</date>
// <summary>
//     The CommonService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Youffer.Common.CRMService;
    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.Enum;
    using Youffer.Resources.Models;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Class CommonService.
    /// </summary>
    public class CommonService : ICommonService
    {
        /// <summary>
        /// The payment configuration information repository
        /// </summary>
        private readonly IRepository<PaymentConfigInfo> paymentConfigInfoRepository;

        /// <summary>
        /// Gets the Email Template Repository.
        /// </summary>
        private readonly IRepository<EmailTemplates> emailTemplateRepository;

        /// <summary>
        /// Gets the Common Repository.
        /// </summary>
        private readonly IRepository<Country> countryRepository;

        /// <summary>
        /// The dept repository
        /// </summary>
        private readonly IRepository<Department> deptRepository;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The contact service
        /// </summary>
        private readonly ICRMManagerService crmManagerService;

        /// <summary>
        /// The state repository
        /// </summary>
        private readonly IRepository<States> stateRepository;

        /// <summary>
        /// The MSG template repository
        /// </summary>
        private readonly IRepository<MessageTemplates> msgTemplateRepository;

        /// <summary>
        /// The mobile application version repository
        /// </summary>
        private readonly IRepository<MobileAppVersion> mobileAppVersionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonService"/> class.
        /// </summary>
        /// <param name="countryRepository">The country repository.</param>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="crmManagerService">The CRM manager service.</param>
        /// <param name="deptRepository">The dept repository.</param>
        /// <param name="emailTemplateRepository">The email template repository.</param>
        /// <param name="stateRepository">The state repository.</param>
        /// <param name="msgTemplateRepository">The MSG template repository.</param>
        /// <param name="paymentConfigInfoRepository">The payment configuration information repository.</param>
        /// <param name="mobileAppVersionRepository">The mobile app version repository.</param>
        public CommonService(IRepository<Country> countryRepository, ILoggerService loggerService, IMapperFactory mapperFactory, ICRMManagerService crmManagerService, IRepository<Department> deptRepository, IRepository<EmailTemplates> emailTemplateRepository, IRepository<States> stateRepository, IRepository<MessageTemplates> msgTemplateRepository, IRepository<PaymentConfigInfo> paymentConfigInfoRepository, IRepository<MobileAppVersion> mobileAppVersionRepository)
        {
            this.paymentConfigInfoRepository = paymentConfigInfoRepository;
            this.countryRepository = countryRepository;
            this.LoggerService = loggerService;
            this.mapperFactory = mapperFactory;
            this.crmManagerService = crmManagerService;
            this.deptRepository = deptRepository;
            this.emailTemplateRepository = emailTemplateRepository;
            this.stateRepository = stateRepository;
            this.msgTemplateRepository = msgTemplateRepository;
            this.mobileAppVersionRepository = mobileAppVersionRepository;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        /// <value>The logger service.</value>
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Gets the country data.
        /// </summary>
        /// <returns>The list of country model.</returns>
        public List<CountryModel> GetCountryMetaData()
        {
            List<CountryModel> lstCountryModel = new List<CountryModel>();
            try
            {
                List<Country> lstCountry = this.GetCountryData();
                lstCountryModel = this.mapperFactory.GetMapper<List<Country>, List<CountryModel>>().Map(lstCountry);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetCountryMetaData - " + ex.Message);
            }

            return lstCountryModel;
        }

        /// <summary>
        /// Gets the state meta data.
        /// </summary>
        /// <returns>List of states.</returns>
        public List<string> GetStateMetaData()
        {
            List<string> lstState = new List<string>();
            try
            {
                List<States> lstStates = this.GetStateData();
                lstState = lstStates.Select(x => x.StateName).Distinct().ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetStateMetaData - " + ex.Message);
            }

            return lstState;
        }

        /// <summary>
        /// Gets the user country details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> The country model object.</returns>
        public CountryModel GetUserCountryDetails(string userId)
        {
            Country country = new Country();
            try
            {
                string countryName = this.crmManagerService.GetContact(userId).MailingCountry;
                country = this.GetCountryData().Where(x => x.CountryName == countryName).FirstOrDefault() ?? new Country();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetUserCountryDetails - " + ex.Message);
            }

            return this.mapperFactory.GetMapper<Country, CountryModel>().Map(country);
        }

        /// <summary>
        /// Gets the emailTemplate details. 
        /// </summary>
        /// <param name="type">The Email type identifier.</param>
        /// <returns> The EmailTemplatesDto model object.</returns>
        public EmailTemplatesDto GetEmailTemlate(TemplateType type)
        {
            EmailTemplates emailTemplate = new EmailTemplates();
            try
            {
                emailTemplate = this.GetEmailTemplateData().Where(x => x.TemplateType == type).FirstOrDefault() ?? new EmailTemplates();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetEmailTemlate - " + ex.Message);
            }

            return this.mapperFactory.GetMapper<EmailTemplates, EmailTemplatesDto>().Map(emailTemplate);
        }

        /// <summary>
        /// Gets the message template.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>MessageTemplatesDto object.</returns>
        public MessageTemplatesDto GetMessageTemplate(MessageTemplateType type)
        {
            MessageTemplates msgTemplate = new MessageTemplates();
            try
            {
                msgTemplate = this.GetMessageTemplateData().Where(x => x.TemplateType == type).FirstOrDefault() ?? new MessageTemplates();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMessageTemplate - " + ex.Message);
            }

            return this.mapperFactory.GetMapper<MessageTemplates, MessageTemplatesDto>().Map(msgTemplate);
        }

        /// <summary>
        /// Gets user country details from country name.
        /// </summary>
        /// <param name="countryName">Name of the country.</param>
        /// <returns> The country model object.</returns>
        public CountryModel GetUserCountryDetailsFromName(string countryName)
        {
            Country country = new Country();
            try
            {
                if (!string.IsNullOrWhiteSpace(countryName))
                {
                    country = this.GetCountryData().Where(x => x.CountryName == countryName).FirstOrDefault() ?? new Country();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetUserCountryDetailsFromName - " + ex.Message);
            }

            return this.mapperFactory.GetMapper<Country, CountryModel>().Map(country);
        }

        /// <summary>
        /// Gets the company country details.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>CountryModel object.</returns>
        public CountryModel GetCompanyCountryDetails(string companyId)
        {
            Country country = new Country();
            try
            {
                string countryName = this.crmManagerService.GetOrganisation(companyId).BillCountry;
                if (!string.IsNullOrWhiteSpace(countryName))
                {
                    country = this.GetCountryData().Where(x => x.CountryName == countryName).FirstOrDefault() ?? new Country();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetCompanyCountryDetails - " + ex.Message);
            }

            return this.mapperFactory.GetMapper<Country, CountryModel>().Map(country);
        }

        /// <summary>
        /// Gets the contact us dept meta data.
        /// </summary>
        /// <returns>List of Department model</returns>
        public List<DepartmentModel> GetContactUsDeptMetaData()
        {
            List<DepartmentModel> lstDeptModel = new List<DepartmentModel>();
            try
            {
                List<Department> lstDept = this.deptRepository.GetAll().ToList();
                lstDeptModel = this.mapperFactory.GetMapper<List<Department>, List<DepartmentModel>>().Map(lstDept);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetContactUsDeptMetaData - " + ex.Message);
            }

            return lstDeptModel;
        }

        /// <summary>
        /// Gets the country from ISO2 code.
        /// </summary>
        /// <param name="iSO2Code">The ISO2 code.</param>
        /// <returns>string object.</returns>
        public string GetCountryFromISO2Code(string iSO2Code)
        {
            Country country = new Country();
            try
            {
                if (!string.IsNullOrWhiteSpace(iSO2Code))
                {
                    country = this.GetCountryData().Where(x => x.ISO2 == iSO2Code).FirstOrDefault() ?? new Country();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetCountryFromISO2Code - " + ex.Message);
            }

            return country.CountryName;
        }

        /// <summary>
        /// Gets the country from calling code.
        /// </summary>
        /// <param name="callingCode">The calling code.</param>
        /// <returns>string object.</returns>
        public string GetCountryFromCallingCode(string callingCode)
        {
            Country country = new Country();
            try
            {
                if (!string.IsNullOrWhiteSpace(callingCode))
                {
                    country = this.GetCountryData().Where(x => x.CallingCode == callingCode).FirstOrDefault() ?? new Country();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetCountryFromCallingCode - " + ex.Message);
            }

            return country.CountryName;
        }

        /// <summary>
        /// Gets the state from area code.
        /// </summary>
        /// <param name="areaCode">The area code.</param>
        /// <returns>StateModel object.</returns>
        public StateModel GetStateFromAreaCode(string areaCode)
        {
            States state = new States();
            try
            {
                if (!string.IsNullOrWhiteSpace(areaCode))
                {
                    state = this.GetStateData().Where(x => x.AreaCode == areaCode).FirstOrDefault() ?? new States();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetStateFromAreaCode - " + ex.Message);
            }

            return this.mapperFactory.GetMapper<States, StateModel>().Map(state);
        }

        /// <summary>
        /// Gets the payment configuration information.
        /// </summary>
        /// <param name="country">The country name</param>
        /// <returns>List of PaymentConfigInfoDto</returns>
        public List<PaymentConfigInfoDto> GetPaymentConfigInfo(string country)
        {
            List<PaymentConfigInfoDto> paymentConfigDataList = new List<PaymentConfigInfoDto>();
            try
            {
                List<PaymentConfigInfo> paymentConfigList = this.GetPaymentConfigInfoData();
                if (!string.IsNullOrWhiteSpace(country))
                {
                    paymentConfigList = paymentConfigList.Where(x => x.Country.ToLower() == country.ToLower()).ToList();
                }

                paymentConfigDataList = this.mapperFactory.GetMapper<List<PaymentConfigInfo>, List<PaymentConfigInfoDto>>().Map(paymentConfigList);

                foreach (var item in paymentConfigDataList)
                {
                    foreach (var item1 in item.PaymentGatewayInfo)
                    {
                        Dictionary<string, string> tmp = new Dictionary<string, string>();
                        item1.PaymentGatewayDetailsInfo.ForEach(x => tmp.Add(x.PaymentGatewayKey, x.PaymentGatewayValue));
                        item1.PaymentGatewayInfoDict = tmp;
                    }
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetPaymentConfigInfo" + ex.Message);
            }

            return paymentConfigDataList;
        }

        /// <summary>
        /// Gets the mobile application version.
        /// </summary>
        /// <param name="mobileAppVersionDto">The mobile application version dto.</param>
        /// <returns>MobileAppVersionDto object.</returns>
        public MobileAppVersionDto GetMobileAppVersion(MobileAppVersionDto mobileAppVersionDto)
        {
            try
            {
                MobileAppVersion appVersion = this.mobileAppVersionRepository.Find(x => x.Version > mobileAppVersionDto.Version && x.OS == mobileAppVersionDto.OS && x.IsActive).FirstOrDefault() ?? new MobileAppVersion();
                return this.mapperFactory.GetMapper<MobileAppVersion, MobileAppVersionDto>().Map(appVersion);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMobileAppVersion" + ex.Message);
            }

            return new MobileAppVersionDto();
        }

        /// <summary>
        /// Gets the calling code from is o2 code.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns>String object.</returns>
        public string GetCallingCodeFromISO2Code(string countryCode)
        {
            Country country = new Country();
            try
            {
                if (!string.IsNullOrWhiteSpace(countryCode))
                {
                    country = this.GetCountryData().Where(x => x.ISO2 == countryCode).FirstOrDefault() ?? new Country();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetCallingCodeFromISO2Code - " + ex.Message);
            }

            return country.CallingCode;
        }

        /// <summary>
        /// Gets the is o2 code from calling code.
        /// </summary>
        /// <param name="callingCode">The calling code.</param>
        /// <returns>
        /// String object.
        /// </returns>
        public string GetISO2CodeFromCallingCode(string callingCode)
        {
            Country country = new Country();
            try
            {
                if (!string.IsNullOrWhiteSpace(callingCode))
                {
                    country = this.GetCountryData().Where(x => x.CallingCode == callingCode).FirstOrDefault() ?? new Country();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetISO2CodeFromCallingCode - " + ex.Message);
            }

            return country.ISO2;
        }

        /// <summary>
        /// Sets cache to system
        /// </summary>
        /// <typeparam name="T">Type of Item</typeparam>
        /// <param name="item">Item to be cached</param>
        /// <param name="cacheName">Name of cache</param>
        public void SetCache<T>(T item, string cacheName)
        {
            HttpContext.Current.Cache[cacheName] = item;
        }

        /// <summary>
        /// Gets the cached Item.
        /// </summary>
        /// <typeparam name="T">Type of Item</typeparam>
        /// <param name="cacheName">Name of cache</param>
        /// <returns>Item Object</returns>
        public T GetCache<T>(string cacheName)
        {
            try
            {
                return (T)HttpContext.Current.Cache[cacheName];
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Gets the country data.
        /// </summary>
        /// <returns> List of Country.</returns>
        private List<Country> GetCountryData()
        {
            List<Country> countryList = HttpContext.Current.Cache["CountryList"] as List<Country>;
            if (countryList == null || !countryList.Any())
            {
                countryList = this.countryRepository.GetAll().ToList();
                HttpContext.Current.Cache["CountryList"] = countryList;
            }

            return countryList;
        }

        /// <summary>
        /// Gets the state data.
        /// </summary>
        /// <returns>List of States.</returns>
        private List<States> GetStateData()
        {
            List<States> stateList = HttpContext.Current.Cache["StateList"] as List<States>;
            if (stateList == null || !stateList.Any())
            {
                stateList = this.stateRepository.GetAll().OrderBy(x => x.StateName).ToList();
                HttpContext.Current.Cache["StateList"] = stateList;
            }

            return stateList;
        }

        /// <summary>
        /// Gets the EmailTemplates data.
        /// </summary>
        /// <returns> List of EmailTemplates </returns>
        private List<EmailTemplates> GetEmailTemplateData()
        {
            List<EmailTemplates> emailTemplateList = null;

            try
            {
                emailTemplateList = HttpContext.Current.Cache["EmailTemplateList"] as List<EmailTemplates>;
                if (emailTemplateList == null || !emailTemplateList.Any())
                {
                    emailTemplateList = this.emailTemplateRepository.GetAll().ToList();
                    HttpContext.Current.Cache["EmailTemplateList"] = emailTemplateList;
                }
            }
            catch (Exception ex)
            {
                emailTemplateList = this.emailTemplateRepository.GetAll().ToList();
            }

            return emailTemplateList;
        }

        /// <summary>
        /// Gets the MessageTemplates data.
        /// </summary>
        /// <returns> List of MessageTemplates </returns>
        private List<MessageTemplates> GetMessageTemplateData()
        {
            List<MessageTemplates> msgTemplateList = null;

            try
            {
                msgTemplateList = HttpContext.Current.Cache["MessageTemplateList"] as List<MessageTemplates>;
                if (msgTemplateList == null || !msgTemplateList.Any())
                {
                    msgTemplateList = this.msgTemplateRepository.GetAll().ToList();
                    HttpContext.Current.Cache["MessageTemplateList"] = msgTemplateList;
                }
            }
            catch (Exception ex)
            {
                msgTemplateList = this.msgTemplateRepository.GetAll().ToList();
            }

            return msgTemplateList;
        }

        /// <summary>
        /// Gets the MessageTemplates data.
        /// </summary>
        /// <returns> List of PaymentConfigInfo </returns>
        private List<PaymentConfigInfo> GetPaymentConfigInfoData()
        {
            List<PaymentConfigInfo> paymentConfigList = new List<PaymentConfigInfo>();
            try
            {
                paymentConfigList = this.paymentConfigInfoRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetPaymentConfigInfoData:- " + ex.Message);
            }

            return paymentConfigList;
        }
    }
}
