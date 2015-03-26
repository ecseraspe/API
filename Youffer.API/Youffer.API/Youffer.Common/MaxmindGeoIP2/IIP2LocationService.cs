// ---------------------------------------------------------------------------------------------------
// <copyright file="IIP2LocationService.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-23</date>
// <summary>
//     The IIP2LocationService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.MaxmindGeoIP2
{
    using Youffer.Resources.ViewModel.MaxmindGeoIP2;

    /// <summary>
    /// Interface IIP2LocationService
    /// </summary>
    public interface IIP2LocationService
    {
        /// <summary>
        /// Gets the country data.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>CountryDto object.</returns>
        CountryDto GetCountryData(string ipAddress);

        /// <summary>
        /// Gets the city data.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>CityDto object.</returns>
        CityDto GetCityData(string ipAddress);
    }
}
