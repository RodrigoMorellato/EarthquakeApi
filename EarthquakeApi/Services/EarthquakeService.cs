using EarthquakeApi.Models;
using EarthquakeApi.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EarthquakeApi.Services
{
    public class EarthquakeService : IEarthquakeService
    {
        private readonly IUsgsService _usgs;

        public EarthquakeService(IUsgsService usgs)
        {
            _usgs = usgs;
        }

        /// <summary>
        /// Ask for a list of earthquakes that impacted the selected area and returns the 10 most recent
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Earthquake>> GetLastTenEarthquakeAsync(double latitude, double longitude, DateTime startDate, DateTime endDate)
        {
            var list = await GetUsgsEarthquakeInfoAsync(latitude, longitude, startDate, endDate);
            return list?.OrderByDescending(x => x.Date).Take(10);
        }

        #region Private Methods

        /// <summary>
        /// Ask for a list of earthquakes and returns those that fit the parameters
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Earthquake>> GetUsgsEarthquakeInfoAsync(double latitude, double longitude, DateTime startDate, DateTime endDate)
        {
            bool Predicate(Earthquake x)
            {
                return x.Date >= startDate
                       && x.Date <= endDate
                       && HaversineService.CalculatePointToPointDistance(x.Latitude, x.Longitude, latitude, longitude).Result <= x.Distance;
            }

            return (await _usgs.ReadCsv())
                .Select(x => new Earthquake(x))
                .Where(Predicate);
        }

        #endregion


        public void Dispose()
        {
        }
    }
}
