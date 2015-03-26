// ---------------------------------------------------------------------------------------------------
// <copyright file="Feedback.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-8</date>
// <summary>
//     The Feedback class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class Feedback
    /// </summary>
    [Table("Feedback")]
    public class Feedback
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Feedback"/> class.
        /// </summary>
        public Feedback()
        {
            this.CreatedOn = DateTime.UtcNow;
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
        /// Gets or sets from identifier.
        /// </summary>
        /// <value>
        /// From identifier.
        /// </value>
        public string FromId { get; set; }

        /// <summary>
        /// Gets or sets to identifier.
        /// </summary>
        /// <value>
        /// To identifier.
        /// </value>
        public string ToId { get; set; }

        /// <summary>
        /// Gets or sets the feedback identifier.
        /// </summary>
        /// <value>
        /// The feedback identifier.
        /// </value>
        public int FeedbackId { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
