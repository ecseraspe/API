// ---------------------------------------------------------------------------------------------------
// <copyright file="CRMCompanyNotes.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-17</date>
// <summary>
//     The CRMCompanyNotes class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.CRMModel
{
    using System;

    /// <summary>
    /// The CRM Company Notes class
    /// </summary>
    public class CRMCompanyNotes : BaseCrmModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CRMCompanyNotes" /> class.
        /// </summary>
        public CRMCompanyNotes()
        {
            this.CreatedOn = this.ModifiedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the comnpany notes no
        /// </summary>
        public string CompanyNotesNo { get; set; }

        /// <summary>
        /// Gets or sets the Notes
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the contact Id
        /// </summary>
        public string ContactId { get; set; }

        /// <summary>
        /// Gets or sets the OrganisationId
        /// </summary>
        public string OrganisationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the Created on
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the Modified on
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}
