// ---------------------------------------------------------------------------------------------------
// <copyright file="IP2LocationService.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-23</date>
// <summary>
//     The IP2LocationService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.MaxmindGeoIP2
{
    using System;
    using MaxMind;
    using MaxMind.GeoIP2;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.Common.MaxmindGeoIP2;
    using Youffer.Resources.ViewModel.MaxmindGeoIP2;

    /// <summary>
    /// Class IP2LocationService
    /// </summary>
    public class IP2LocationService : IIP2LocationService
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly WebServiceClient client;

        /// <summary>
        /// The logger service
        /// </summary>
        private readonly ILoggerService loggerService;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="IP2LocationService"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="licenseKey">The license key.</param>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        public IP2LocationService(int userId, string licenseKey, ILoggerService loggerService, IMapperFactory mapperFactory)
        {
            this.loggerService = loggerService;
            this.mapperFactory = mapperFactory;

            try
            {
                this.client = new WebServiceClient(userId, licenseKey);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the country data.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>CountryDto object.</returns>
        public CountryDto GetCountryData(string ipAddress)
        {
            CountryDto countryData = new CountryDto();

            try
            {
                var response = this.client.Country(ipAddress);
                countryData.CountryName = response.Country.Name;
                countryData.IsoCode = response.Country.IsoCode;
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("GetCountryData for IP Address -  " + ipAddress + " - " + ex.Message);
            }

            return countryData;
        }

        /// <summary>
        /// Gets the city data.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>CityDto object.</returns>
        public CityDto GetCityData(string ipAddress)
        {
            CityDto cityData = new CityDto();

            try
            {
                var response = this.client.City(ipAddress);

                cityData.MostSpecificSubDivisionName = response.MostSpecificSubdivision.Name;
                cityData.MostSpecificSubDivisionIsoCode = response.MostSpecificSubdivision.IsoCode;
                cityData.CityName = response.City.Name;
                cityData.PostalCode = response.Postal.Code;
                cityData.Latitude = response.Location.Latitude.HasValue ? response.Location.Latitude.Value : 0.0;
                cityData.Longitude = response.Location.Longitude.HasValue ? response.Location.Longitude.Value : 0.0;
                cityData.CountryInfo.CountryName = response.Country.Name;
                cityData.CountryInfo.IsoCode = response.Country.IsoCode;
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("GetCityData for IP Address -  " + ipAddress + " - " + ex.Message);
            }

            return cityData;
        }
    }
}
