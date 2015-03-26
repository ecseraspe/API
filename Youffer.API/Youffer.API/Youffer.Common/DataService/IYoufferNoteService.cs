// ---------------------------------------------------------------------------------------------------
// <copyright file="IYoufferNoteService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-1</date>
// <summary>
//     The IYoufferNoteService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using System.Collections.Generic;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Interface IYoufferNoteService
    /// </summary>
    public interface IYoufferNoteService
    {
        /// <summary>
        /// Gets the company notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>List of CompanyNotesDto object.</returns>
        List<CompanyNotesDto> GetCompanyNotes(string userId, string companyId);

        /// <summary>
        /// Saves the company note.
        /// </summary>
        /// <param name="companyNote">The company note.</param>
        /// <returns>CompanyNotesDto object.</returns>
        CompanyNotesDto SaveCompanyNote(CompanyNotesDto companyNote);
    }
}
