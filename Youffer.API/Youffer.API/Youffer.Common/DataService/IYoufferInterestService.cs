// ---------------------------------------------------------------------------------------------------
// <copyright file="IYoufferInterestService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-13</date>
// <summary>
//     The IYoufferInterestService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using System.Collections.Generic;
    using Youffer.Resources.Models;

    /// <summary>
    /// Interface IYoufferInterestService
    /// </summary>
    public interface IYoufferInterestService
    {
        /// <summary>
        /// Gets the main interest from sub.
        /// </summary>
        /// <param name="subInterest">The sub interest.</param>
        /// <returns>List of InterestModel object.</returns>
        List<InterestModel> GetMainInterestFromSub(string subInterest);

        /// <summary>
        /// Gets the sub interest from main.
        /// </summary>
        /// <param name="mainInterest">The main interest.</param>
        /// <returns>List of InterestModel object.</returns>
        List<InterestModel> GetSubInterestFromMain(string mainInterest);

        /// <summary>
        /// Gets all parent business types.
        /// </summary>
        /// <returns>List of string</returns>
        List<string> GetAllParentBusinessTypes();

        /// <summary>
        /// Gets all sub interests.
        /// </summary>
        /// <param name="interestName">Name of the interest.</param>
        /// <returns>List of DictModel</returns>
        List<DictModel> GetAllSubInterests(string interestName);

        /// <summary>
        /// Gets the sub business type from main.
        /// </summary>
        /// <param name="mainBusinessType">Type of the main business.</param>
        /// <returns>List of BusinessTypeModel object.</returns>
        List<BusinessTypeModel> GetSubBusinessTypeFromMain(string mainBusinessType);

        /// <summary>
        /// Gets the main business type from sub.
        /// </summary>
        /// <param name="subBusinessType">Type of the sub business.</param>
        /// <returns>List of BusinessTypeModel object.</returns>
        List<BusinessTypeModel> GetMainBusinessTypeFromSub(string subBusinessType);

        /// <summary>
        /// Gets the search options.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns>List of SearchOptions object.</returns>
        List<SearchOptions> GetSearchOptions(string searchText);
    }
}
