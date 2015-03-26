// ---------------------------------------------------------------------------------------------------
// <copyright file="SubInterest.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-13</date>
// <summary>
//     The SubInterest class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class SubInterest
    /// </summary>
    [Table("SubInterest")]
    public class SubInterest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubInterest"/> class.
        /// </summary>
        public SubInterest()
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
        /// Gets or sets the main interest identifier.
        /// </summary>
        /// <value>
        /// The main interest identifier.
        /// </value>
        public int MainInterestId { get; set; }

        /// <summary>
        /// Gets or sets the sub interest.
        /// </summary>
        /// <value>
        /// The sub interest.
        /// </value>
        public string SubInterestName { get; set; }

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
