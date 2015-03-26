// ---------------------------------------------------------------------------------------------------
// <copyright file="NoteService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-4</date>
// <summary>
//     The NoteService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data.CRMService
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Youffer.Common.CRMService;
    using Youffer.Common.LogService;
    using Youffer.CRM;
    using Youffer.Resources.CRMModel;

    /// <summary>
    /// The NoteService class
    /// </summary>
    public class NoteService : INoteService
    {
        /// <summary>
        /// VTiger service instance
        /// </summary>
        private readonly IVTigerService vTigerService;

        /// <summary>
        ///  Initializes a new instance of the <see cref="NoteService" /> class.
        /// </summary>
        /// <param name="vTigerService">the vtiger service</param>
        /// <param name="loggerService">the logger service</param>
        public NoteService(IVTigerService vTigerService, ILoggerService loggerService)
        {
            this.vTigerService = vTigerService;
            this.LoggerService = loggerService;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Add company Notes
        /// </summary>
        /// <param name="notes"> The notes</param>
        /// <returns> VTigerCompanyNotes obj </returns>
        public VTigerCompanyNotes CreateNote(CRMCompanyNotes notes)
        {
            try
            {
                VTigerCompanyNotes note = this.vTigerService.AddCompanyNotes(notes.Note, notes.Assigned_User_Id, notes.ContactId, notes.OrganisationId);
                return note;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Adding Notes :- " + ex.Message);
            }

            return new VTigerCompanyNotes();
        }

        /// <summary>
        ///  Retrieving note from CRM
        /// </summary>
        /// <param name="contactId"> the user id. </param>
        /// <param name="companyId"> the company id. </param>
        /// <param name="lastSeenId"> The last seen Id. </param>
        /// <param name="fetchCount"> The fetch Count</param>
        /// <returns> VTigerCompanyNotes list </returns>
        public List<VTigerCompanyNotes> ReadNotes(string contactId, string companyId, int lastSeenId, int fetchCount)
        {
            List<VTigerCompanyNotes> noteList = new List<VTigerCompanyNotes>();
            try
            {
                lastSeenId = lastSeenId < 1 ? 1 : lastSeenId;
                int startVal = (lastSeenId - 1) * fetchCount;
                string query = "select * from Companynotes where companynotes_tks_organisation = '" + companyId + "' and companynotes_tks_contact = '" + contactId + "' and companynotes_tks_isdeleted = 0 LIMIT " + startVal + " , " + fetchCount + ";";
                IEnumerable<VTigerCompanyNotes> notes = this.vTigerService.Query<VTigerCompanyNotes>(query);
                noteList = notes.ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Retrieving Notes :- " + ex.Message);
            }

            return noteList;
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> Boolean object </returns>
        public bool DeleteNote(string noteId)
        {
            try
            {
                VTigerCompanyNotes note = this.vTigerService.Retrieve<VTigerCompanyNotes>(noteId);
                note.companynotes_tks_isdeleted = true;
                note = this.vTigerService.Update<VTigerCompanyNotes>(note);
                return note.companynotes_tks_isdeleted;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Delete Note :- " + ex.Message);
            }

            return false;
        }
    }
}
