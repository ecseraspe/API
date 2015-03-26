// ---------------------------------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-20</date>
// <summary>
//     The IUserService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using System.Collections.Generic;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The IUserService interface
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Updating the user
        /// </summary>
        /// <param name="user"> The User model. </param>
        /// <returns> ApplicationUserDto object </returns>
        ApplicationUserDto UpdateUser(ApplicationUserDto user);

        /// <summary>
        /// Get Application User
        /// </summary>
        /// <param name="contactId"> The Contact Id. </param>
        /// <returns> ApplicationUserDto object</returns>
        ApplicationUserDto GetContact(string contactId);

        /// <summary>
        /// Get users List
        /// </summary>
        /// <param name="userId"> The user Id</param>
        /// <param name="companyId"> The companyId</param>
        /// <returns> List of Users</returns>
        List<ApplicationUserDto> GetUsers(string userId, string companyId);

        /// <summary>
        /// Get Application User
        /// </summary>
        /// <param name="contactId"> The Contact Id. </param>
        /// <returns> ApplicationUserDto object</returns>
        ApplicationUserDto GetContactByCrmId(string contactId);
    }
}
