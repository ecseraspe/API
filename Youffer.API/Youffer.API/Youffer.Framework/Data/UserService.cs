// ---------------------------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-20</date>
// <summary>
//     The UserService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The UserService class
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        ///  Gets the User Repository.
        /// </summary>
        private readonly IRepository<ApplicationUser> userRepository;

        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        ///  Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="userRepository"> The User Repository. </param>
        /// <param name="loggerService"> The Logger Service. </param>
        /// <param name="mapperFactory">The mapper factory.</param>
        public UserService(IRepository<ApplicationUser> userRepository, ILoggerService loggerService, IMapperFactory mapperFactory)
        {
            this.userRepository = userRepository;
            this.LoggerService = loggerService;
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Updating the user
        /// </summary>
        /// <param name="user"> The User model. </param>
        /// <returns> ApplicationUserDto object </returns>
        public ApplicationUserDto UpdateUser(ApplicationUserDto user)
        {
            try
            {
                user.ModifiedOn = DateTime.UtcNow;
                var newUser = this.mapperFactory.GetMapper<ApplicationUserDto, ApplicationUser>().Map(user);
                this.userRepository.Update(newUser);
                this.userRepository.Commit();
                user = this.mapperFactory.GetMapper<ApplicationUser, ApplicationUserDto>().Map(newUser);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Updating User - " + ex.Message);
            }

            return user;
        }

        /// <summary>
        /// Get Application User
        /// </summary>
        /// <param name="contactId"> The Contact Id. </param>
        /// <returns> ApplicationUserDto object</returns>
        public ApplicationUserDto GetContact(string contactId)
        {
            ApplicationUserDto user = new ApplicationUserDto();
            try
            {
                ApplicationUser appUser = this.userRepository.First(x => x.Id == contactId) ?? new ApplicationUser();
                user = this.mapperFactory.GetMapper<ApplicationUser, ApplicationUserDto>().Map(appUser);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Fetching contact :- " + ex.Message);
            }

            return user;
        }

        /// <summary>
        /// Get Application User
        /// </summary>
        /// <param name="contactId"> The Contact Id. </param>
        /// <returns> ApplicationUserDto object</returns>
        public ApplicationUserDto GetContactByCrmId(string contactId)
        {
            ApplicationUserDto user = new ApplicationUserDto();
            try
            {
                ApplicationUser appUser = this.userRepository.First(x => x.CRMId == contactId) ?? new ApplicationUser();
                user = this.mapperFactory.GetMapper<ApplicationUser, ApplicationUserDto>().Map(appUser);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Fetching contact :- " + ex.Message);
            }

            return user;
        }

        /// <summary>
        /// Get users List
        /// </summary>
        /// <param name="userId"> The user Id</param>
        /// <param name="companyId"> The companyId</param>
        /// <returns> List of Users</returns>
        public List<ApplicationUserDto> GetUsers(string userId, string companyId)
        {
            List<ApplicationUserDto> userList = new List<ApplicationUserDto>();
            try
            {
                List<ApplicationUser> users = this.userRepository.Find(x => x.Id == userId || x.Id == companyId).ToList();
                userList = this.mapperFactory.GetMapper<List<ApplicationUser>, List<ApplicationUserDto>>().Map(users);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Getting Users :- " + ex.Message);
            }

            return userList;
        }
    }
}
