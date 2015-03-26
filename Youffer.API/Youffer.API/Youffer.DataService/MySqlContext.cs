// ---------------------------------------------------------------------------------------------------
// <copyright file="MySqlContext.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-31</date>
// <summary>
//     The MySqlContext class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService
{
    using System.Data.Entity; 
    using Resources.Constants;

    /// <summary>
    /// The AuthContext class
    /// </summary>
    public class MySqlContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlContext"/> class.
        /// </summary>
        public MySqlContext()
            : base(ConfigConstants.VtigerConnectionStringKey)
        {
        }

        /// <summary>
        /// Gets or sets the vtiger contactdetails.
        /// </summary>
        public DbSet<vtiger_contactdetails> vtiger_contactdetails { get; set; }

        /// <summary>
        /// Gets or sets the vtiger contactscf.
        /// </summary>
        public DbSet<vtiger_contactscf> vtiger_contactscf { get; set; }
    }
}