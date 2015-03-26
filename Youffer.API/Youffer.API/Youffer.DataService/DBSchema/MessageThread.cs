// ---------------------------------------------------------------------------------------------------
// <copyright file="MessageThread.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-3</date>
// <summary>
//     The MessageThread class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class MessageThread.
    /// </summary>
    [Table("MessageThread")]
    public class MessageThread
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageThread"/> class.
        /// </summary>
        public MessageThread()
        {
            this.IsDeleted = this.IsDeletedByUser = this.IsDeletedByCompany = false;
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the repository Id.
        /// </summary>
        [NotMapped]
        public object RepositoryId
        {
            get
            {
                return this.Id;
            }
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
