// ---------------------------------------------------------------------------------------------------
// <copyright file="CityDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-24</date>
// <summary>
//     The CityDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel.MaxmindGeoIP2
{
    public class CityDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CityDto"/> class.
        /// </summary>
        public CityDto()
        {
            this.CountryInfo = new CountryDto();
        }

        /// <summary>
        /// Gets or sets the name of the most specific sub division.
        /// </summary>
        public string MostSpecificSubDivisionName { get; set; }

        /// <summary>
        /// Gets or sets the most specific sub division iso code.
        /// </summary>
        public string MostSpecificSubDivisionIsoCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the country information.
        /// </summary>
        public CountryDto CountryInfo { get; set; }
    }
}
