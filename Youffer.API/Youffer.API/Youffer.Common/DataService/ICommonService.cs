// ---------------------------------------------------------------------------------------------------
// <copyright file="ICommonService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-28</date>
// <summary>
//     The ICommonService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using System.Collections.Generic;
    using Youffer.Resources.Enum;
    using Youffer.Resources.Models;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Interface ICommonService
    /// </summary>
    public interface ICommonService
    {
        /// <summary>
        /// Gets the country data.
        /// </summary>
        /// <returns> The Country Model.</returns>
        List<CountryModel> GetCountryMetaData();

        /// <summary>
        /// Gets the user country details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> The country model object.</returns>
        CountryModel GetUserCountryDetails(string userId);

        /// <summary>
        /// Gets user country details from country name.
        /// </summary>
        /// <param name="countryName">Name of the country.</param>
        /// <returns>CountryModel object.</returns>
        CountryModel GetUserCountryDetailsFromName(string countryName);

        /// <summary>
        /// Gets the company country details.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>CountryModel object.</returns>
        CountryModel GetCompanyCountryDetails(string companyId);

        /// <summary>
        /// Gets the contact us dept meta data.
        /// </summary>
        /// <returns>List of Department model</returns>
        List<DepartmentModel> GetContactUsDeptMetaData();

        /// <summary>
        /// Gets the country from ISO2 code.
        /// </summary>
        /// <param name="iSO2Code">The ISO2 code.</param>
        /// <returns>string object.</returns>
        string GetCountryFromISO2Code(string iSO2Code);

        /// <summary>
        /// Gets the country from calling code.
        /// </summary>
        /// <param name="callingCode">The calling code.</param>
        /// <returns>string object.</returns>
        string GetCountryFromCallingCode(string callingCode);

        /// <summary>
        /// Gets the state from area code.
        /// </summary>
        /// <param name="areaCode">The area code.</param>
        /// <returns>StateModel object.</returns>
        StateModel GetStateFromAreaCode(string areaCode);

        /// <summary>
        /// Gets the state meta data.
        /// </summary>
        /// <returns>List of states.</returns>
        List<string> GetStateMetaData();

        /// <summary>
        /// Gets the emailTemplate details. 
        /// </summary>
        /// <param name="type">The Email type identifier.</param>
        /// <returns> The EmailTemplatesDto model object.</returns>
        EmailTemplatesDto GetEmailTemlate(TemplateType type);

        /// <summary>
        /// Gets the message template.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>MessageTemplatesDto object.</returns>
        MessageTemplatesDto GetMessageTemplate(MessageTemplateType type);

        /// <summary>
        /// Gets the payment configuration information.
        /// </summary>
        /// <param name="country">The country name</param>
        /// <returns>List of PaymentConfigInfoDto</returns>
        List<PaymentConfigInfoDto> GetPaymentConfigInfo(string country);

        /// <summary>
        /// Sets cache to system
        /// </summary>
        /// <typeparam name="T">Type of Item</typeparam>
        /// <param name="item">Item to be cached</param>
        /// <param name="cacheName">Name of cache</param>
        void SetCache<T>(T item, string cacheName);

        /// <summary>
        /// Gets the cached Item.
        /// </summary>
        /// <typeparam name="T">Type of Item</typeparam>
        /// <param name="cacheName">Name of cache</param>
        /// <returns>Item Object</returns>
        T GetCache<T>(string cacheName);

        /// <summary>
        /// Gets the mobile application version.
        /// </summary>
        /// <param name="mobileAppVersionDto">The mobile application version dto.</param>
        /// <returns>MobileAppVersionDto object.</returns>
        MobileAppVersionDto GetMobileAppVersion(MobileAppVersionDto mobileAppVersionDto);

        /// <summary>
        /// Gets the calling code from is o2 code.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns>String object.</returns>
        string GetCallingCodeFromISO2Code(string countryCode);

        /// <summary>
        /// Gets the is o2 code from calling code.
        /// </summary>
        /// <param name="callingCode">The calling code.</param>
        /// <returns>String object.</returns>
        string GetISO2CodeFromCallingCode(string callingCode);
    }
}
