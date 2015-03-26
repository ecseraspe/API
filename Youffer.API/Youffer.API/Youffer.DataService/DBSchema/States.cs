// ---------------------------------------------------------------------------------------------------
// <copyright file="States.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-23</date>
// <summary>
//     The States class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class States
    /// </summary>
    [Table("States")]
    public class States
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the area code.
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        public string StateCode { get; set; }
    }
}
