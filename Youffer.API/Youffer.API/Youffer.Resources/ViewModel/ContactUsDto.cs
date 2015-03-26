// ---------------------------------------------------------------------------------------------------
// <copyright file="ContactUsDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-11</date>
// <summary>
//     The ContactUsDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System;
    using Youffer.Resources.Enum;

    /// <summary>
    /// Class ContactUsDto
    /// </summary>
    public class ContactUsDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactUsDto"/> class.
        /// </summary>
        public ContactUsDto()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the dept identifier.
        /// </summary>
        /// <value>
        /// The dept identifier.
        /// </value>
        public ContactUsDept DeptId { get; set; }

        /// <summary>
        /// Gets or sets the media identifier.
        /// </summary>
        /// <value>
        /// The media identifier.
        /// </value>
        public long MediaId { get; set; }

        /// <summary>
        /// Gets the created on.
        /// </summary>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the thread identifier.
        /// </summary>
        /// <value>
        /// The thread identifier.
        /// </value>
        public long ThreadId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }
    }
}
