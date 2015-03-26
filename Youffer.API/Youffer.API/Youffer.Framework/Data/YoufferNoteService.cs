// ---------------------------------------------------------------------------------------------------
// <copyright file="YoufferNoteService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-1</date>
// <summary>
//     The YoufferNoteService class
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
    /// Class YoufferNoteService.
    /// </summary>
    public class YoufferNoteService : IYoufferNoteService
    {
        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The company notes repository
        /// </summary>
        private readonly IRepository<CompanyNotes> companyNotesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="YoufferNoteService"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="companyNotesRepository">The company notes repository.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        public YoufferNoteService(ILoggerService loggerService, IRepository<CompanyNotes> companyNotesRepository, IMapperFactory mapperFactory)
        {
            this.LoggerService = loggerService;
            this.companyNotesRepository = companyNotesRepository;
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Gets the company notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>List of CompanyNotesDto object.</returns>
        public List<CompanyNotesDto> GetCompanyNotes(string userId, string companyId)
        {
            List<CompanyNotesDto> lstNotesModel = new List<CompanyNotesDto>();
            try
            {
                List<CompanyNotes> lstNotes = this.companyNotesRepository.Find(x => x.UserId == userId && x.CompanyId == companyId && !x.IsDeleted).ToList();
                lstNotesModel = this.mapperFactory.GetMapper<List<CompanyNotes>, List<CompanyNotesDto>>().Map(lstNotes);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetUserReviews - " + ex.Message);
            }

            return lstNotesModel;
        }

        /// <summary>
        /// Saves the company note.
        /// </summary>
        /// <param name="companyNote">The company note.</param>
        /// <returns>CompanyNotesDto object.</returns>
        public CompanyNotesDto SaveCompanyNote(CompanyNotesDto companyNote)
        {
            try
            {
                CompanyNotes note = this.mapperFactory.GetMapper<CompanyNotesDto, CompanyNotes>().Map(companyNote);
                this.companyNotesRepository.Insert(note);
                this.companyNotesRepository.Commit();

               //// companyNote.Id = note.Id;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("SaveCompanyNote - " + ex.Message);
            }

            return companyNote;
        }
    }
}
