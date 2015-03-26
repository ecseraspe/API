// ---------------------------------------------------------------------------------------------------
// <copyright file="ContactLeadMappingDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-21</date>
// <summary>
//     The ContactLeadMappingDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System;

    /// <summary>
    /// The ContactLeadMappingDto class
    /// </summary>
    public class ContactLeadMappingDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactLeadMappingDto"/> class.
        /// </summary>
        public ContactLeadMappingDto()
        {
            this.IsActive = true;
            this.IsDeleted = false;
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        } 

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the contact id.
        /// </summary>
        public string ContactId { get; set; }

        /// <summary>
        /// Gets or sets the contact crm id.
        /// </summary>
        public string ContactCRMId { get; set; }

        /// <summary>
        /// Gets or sets the crm lead id.
        /// </summary>
        public string LeadId { get; set; }

        /// <summary>
        /// Gets or sets the Modified date.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
