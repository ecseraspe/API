// ---------------------------------------------------------------------------------------------------
// <copyright file="CountryModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-28</date>
// <summary>
//     The CountryModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.Models
{
    /// <summary>
    /// Class CountryModel.
    /// </summary>
    public class CountryModel
    {
        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>The country identifier.</value>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>The name of the country.</value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the long name of the country.
        /// </summary>
        /// <value>The long name of the country.</value>
        public string LongCountryName { get; set; }

        /// <summary>
        /// Gets or sets the is o2.
        /// </summary>
        /// <value>The is o2.</value>
        public string ISO2 { get; set; }

        /// <summary>
        /// Gets or sets the is o3.
        /// </summary>
        /// <value>The is o3.</value>
        public string ISO3 { get; set; }

        /// <summary>
        /// Gets or sets the number code.
        /// </summary>
        /// <value>The number code.</value>
        public string NumCode { get; set; }

        /// <summary>
        /// Gets or sets the calling code.
        /// </summary>
        /// <value>The calling code.</value>
        public string CallingCode { get; set; }

        /// <summary>
        /// Gets or sets the CCTLD.
        /// </summary>
        /// <value>The CCTLD.</value>
        public string CCTLD { get; set; }

        /// <summary>
        /// Gets or sets the flag.
        /// </summary>
        /// <value>The flag identifier.</value>
        public string Flag { get; set; }
    }
}
