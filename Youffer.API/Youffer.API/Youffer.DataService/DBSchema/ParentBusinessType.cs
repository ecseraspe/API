// ---------------------------------------------------------------------------------------------------
// <copyright file="ParentBusinessType.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-10</date>
// <summary>
//     The ParentBusinessType class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class ParentBusinessType
    /// </summary>
    [Table("ParentBusinessType")]
    public class ParentBusinessType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParentBusinessType"/> class.
        /// </summary>
        public ParentBusinessType()
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
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
