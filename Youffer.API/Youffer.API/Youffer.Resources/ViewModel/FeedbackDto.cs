// ---------------------------------------------------------------------------------------------------
// <copyright file="FeedbackDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-8</date>
// <summary>
//     The FeedbackDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System;

    /// <summary>
    /// Class FeedbackDto
    /// </summary>
    public class FeedbackDto
    {
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
