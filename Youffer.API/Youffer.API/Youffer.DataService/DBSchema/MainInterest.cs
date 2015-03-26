// ---------------------------------------------------------------------------------------------------
// <copyright file="MainInterest.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-13</date>
// <summary>
//     The MainInterest class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class MainInterest
    /// </summary>
    [Table("MainInterest")]
    public class MainInterest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainInterest"/> class.
        /// </summary>
        public MainInterest()
        {
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
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the main interest.
        /// </summary>
        /// <value>
        /// The main interest.
        /// </value>
        public string MainInterestName { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        /// <value>
        /// The modified on.
        /// </value>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is Active.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
    }
}
