// ---------------------------------------------------------------------------------------------------
// <copyright file="BusinessTypeModel.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-10</date>
// <summary>
//     The BusinessTypeModel class
// </summary>
// ---------------------------------------------------------------------------------------------------
namespace Youffer.Resources.Models
{
    /// <summary>
    /// Class BusinessTypeModel
    /// </summary>
    public class BusinessTypeModel
    {
        /// <summary>
        /// Gets or sets the name of the parent business type.
        /// </summary>
        public string ParentBusinessTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the business type.
        /// </summary>
        public string BusinessTypeName { get; set; }
    }
}
