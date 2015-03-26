// ---------------------------------------------------------------------------------------------------
// <copyright file="INoteService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-4</date>
// <summary>
//     The INoteService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.CRMService
{
    using System.Collections.Generic;
    using System.Data;
    using Youffer.CRM;
    using Youffer.Resources.CRMModel;

    /// <summary>
    /// The INoteService interface
    /// </summary>
    public interface INoteService
    {
        /// <summary>
        /// Add company Notes
        /// </summary>
        /// <param name="notes"> the note</param>
        /// <returns> VTigerCompanyNotes obj </returns>
        VTigerCompanyNotes CreateNote(CRMCompanyNotes notes);

        /// <summary>
        ///  Retrieving note from CRM
        /// </summary>
        /// <param name="userid"> the user id. </param>
        /// <param name="companyId"> the company id. </param>
        /// <param name="lastSeenId"> The last seen Id. </param>
        /// <param name="fetchCount"> The fetch Count</param>
        /// <returns> VTigerCompanyNotes list </returns>
        List<VTigerCompanyNotes> ReadNotes(string userid, string companyId, int lastSeenId, int fetchCount);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> Boolean object </returns>
        bool DeleteNote(string noteId);
    }
}
