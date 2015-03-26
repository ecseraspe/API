// ---------------------------------------------------------------------------------------------------
// <copyright file="YoufferInterestService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-13</date>
// <summary>
//     The YoufferInterestService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;
    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.Models;

    /// <summary>
    /// Class YoufferInterestService
    /// </summary>
    public class YoufferInterestService : IYoufferInterestService
    {
        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The main interest repository
        /// </summary>
        private readonly IRepository<MainInterest> mainInterestRepository;

        /// <summary>
        /// The sub interest repository
        /// </summary>
        private readonly IRepository<SubInterest> subInterestRepository;

        /// <summary>
        /// The parent business type repository
        /// </summary>
        private readonly IRepository<ParentBusinessType> parentBusinessTypeRepository;

        /// <summary>
        /// The business type repository
        /// </summary>
        private readonly IRepository<BusinessType> businessTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="YoufferInterestService"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="mainInterestRepository">The main interest repository.</param>
        /// <param name="subInterestRepository">The sub interest repository.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="parentBusinessTypeRepository">The parent business repository.</param>
        /// <param name="businessTypeRepository">The business type repository.</param>
        public YoufferInterestService(ILoggerService loggerService, IRepository<MainInterest> mainInterestRepository, IRepository<SubInterest> subInterestRepository, IMapperFactory mapperFactory, IRepository<ParentBusinessType> parentBusinessTypeRepository, IRepository<BusinessType> businessTypeRepository)
        {
            this.LoggerService = loggerService;
            this.mainInterestRepository = mainInterestRepository;
            this.subInterestRepository = subInterestRepository;
            this.mapperFactory = mapperFactory;
            this.parentBusinessTypeRepository = parentBusinessTypeRepository;
            this.businessTypeRepository = businessTypeRepository;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Gets the sub interest from main.
        /// </summary>
        /// <param name="mainInterest">The main interest.</param>
        /// <returns>List of InterestModel object.</returns>
        public List<InterestModel> GetSubInterestFromMain(string mainInterest)
        {
            List<InterestModel> lst = new List<InterestModel>();

            try
            {
                object[] sqlCol = { new SqlParameter("@MainInterest", mainInterest) };
                lst = this.mainInterestRepository.SqlQuery<InterestModel>("GetInterestListByMainInterest @MainInterest", sqlCol).ToList<InterestModel>();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetSubInterestFromMain - " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Gets the sub business type from main.
        /// </summary>
        /// <param name="mainBusinessType">Type of the main business.</param>
        /// <returns>List of BusinessTypeModel object.</returns>
        public List<BusinessTypeModel> GetSubBusinessTypeFromMain(string mainBusinessType)
        {
            List<BusinessTypeModel> lst = new List<BusinessTypeModel>();

            try
            {
                object[] sqlCol = { new SqlParameter("@MainBusinessType", mainBusinessType) };
                lst = this.parentBusinessTypeRepository.SqlQuery<BusinessTypeModel>("GetBusinessTypeListByParentBusinessType @MainBusinessType", sqlCol).ToList<BusinessTypeModel>();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetSubBusinessTypeFromMain - " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Gets the main business type from sub.
        /// </summary>
        /// <param name="subBusinessType">Type of the sub business.</param>
        /// <returns>List of BusinessTypeModel object.</returns>
        public List<BusinessTypeModel> GetMainBusinessTypeFromSub(string subBusinessType)
        {
            List<BusinessTypeModel> lst = new List<BusinessTypeModel>();

            try
            {
                object[] sqlCol = { new SqlParameter("@SubBusinessType", subBusinessType) };
                lst = this.businessTypeRepository.SqlQuery<BusinessTypeModel>("GetParentBusinessTypeListByBusinessType @SubBusinessType", sqlCol).ToList<BusinessTypeModel>();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMainBusinessTypeFromSub - " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Gets the Main interest from sub.
        /// </summary>
        /// <param name="subInterest">The sub interest.</param>
        /// <returns>List of InterestModel object.</returns>
        public List<InterestModel> GetMainInterestFromSub(string subInterest)
        {
            List<InterestModel> lst = new List<InterestModel>();

            try
            {
                object[] sqlCol = { new SqlParameter("@SubInterest", subInterest) };
                lst = this.mainInterestRepository.SqlQuery<InterestModel>("GetInterestListBySubInterest @SubInterest", sqlCol).ToList<InterestModel>();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMainInterestFromSub - " + ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Gets all parent business types.
        /// </summary>
        /// <returns>List of string</returns>
        public List<string> GetAllParentBusinessTypes()
        {
            List<string> list = new List<string>();
            try
            {
                list = this.parentBusinessTypeRepository.GetAll().Select(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Get All main interest" + ex.Message);
            }

            return list;
        }

        /// <summary>
        /// Gets all sub interests.
        /// </summary>
        /// <param name="interestName">Name of the interest.</param>
        /// <returns>List of DictModel</returns>
        public List<DictModel> GetAllSubInterests(string interestName)
        {
            List<DictModel> list = new List<DictModel>();
            try
            {
                List<InterestModel> subList = this.GetSubInterestFromMain(interestName);

                list = subList.Select(x => new DictModel { Key = x.SubInterestName, Value = x.SubInterestName }).ToList<DictModel>();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Get All main interest" + ex.Message);
            }

            return list;
        }

        /// <summary>
        /// Gets the search options.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns>List of SearchOptions object.</returns>
        public List<SearchOptions> GetSearchOptions(string searchText)
        {
            List<SearchOptions> searchOpt = new List<SearchOptions>();

            try
            {
                searchOpt = HttpContext.Current.Cache["SearchOptionsList"] as List<SearchOptions>;
                if (searchOpt == null || !searchOpt.Any())
                {
                    object[] sqlCol = { new SqlParameter("@SearchText", searchText) };
                    searchOpt = this.parentBusinessTypeRepository.SqlQuery<SearchOptions>("GetSearchOptions @SearchText", sqlCol).ToList<SearchOptions>();
                    HttpContext.Current.Cache["SearchOptionsList"] = searchOpt;
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetSearchOptions - " + ex.Message);
            }

            return searchOpt;
        }
    }
}
