// ---------------------------------------------------------------------------------------------------
// <copyright file="MobileAppVersionDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-17</date>
// <summary>
//     The MobileAppVersionDto class
// </summary>
// ---------------------------------------------------------------------------------------------------
namespace Youffer.Resources.ViewModel
{
    /// <summary>
    /// Class MobileAppVersionDto
    /// </summary>
    public class MobileAppVersionDto
    {
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
    }
}
