// ---------------------------------------------------------------------------------------------------
// <copyright file="MessageThreadDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-3</date>
// <summary>
//     The MessageThreadDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System;

    /// <summary>
    /// Class MessageThreadDto.
    /// </summary>
    public class MessageThreadDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageThreadDto"/> class.
        /// </summary>
        public MessageThreadDto()
        {
            this.IsDeleted = this.IsDeletedByCompany = this.IsDeletedByUser = false;
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets from user.
        /// </summary>
        /// <value>From user.</value>
        public string FromUser { get; set; }

        /// <summary>
        /// Gets or sets to user.
        /// </summary>
        /// <value>To user.</value>
        public string ToUser { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the Modified date.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted by user.
        /// </summary>
        public bool IsDeletedByUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted by company.
        /// </summary>
        public bool IsDeletedByCompany { get; set; }
    }
}
