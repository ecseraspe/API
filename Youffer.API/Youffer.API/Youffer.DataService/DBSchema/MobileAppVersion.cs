// ---------------------------------------------------------------------------------------------------
// <copyright file="MobileAppVersion.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-17</date>
// <summary>
//     The MobileAppVersion class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class MobileAppVersion
    /// </summary>
    [Table("MobileAppVersion")]
    public class MobileAppVersion
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public double Version { get; set; }

        /// <summary>
        /// Gets or sets the os.
        /// </summary>
        public int OS { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [force upgrade].
        /// </summary>
        public bool ForceUpgrade { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [recommended upgrade].
        /// </summary>
        public bool RecommendedUpgrade { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
