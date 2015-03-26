// ---------------------------------------------------------------------------------------------------
// <copyright file="CountryDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-24</date>
// <summary>
//     The CountryDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel.MaxmindGeoIP2
{
    public class CountryDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryDto"/> class.
        /// </summary>
        public CountryDto()
        {
            this.CountryName = "United States";
            this.IsoCode = "US";
        }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the iso code.
        /// </summary>
        public string IsoCode { get; set; }
    }
}
