using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EarthquakeApi.Models;

namespace EarthquakeApi.Services.Interfaces
{
    public interface IEarthquakeService : IDisposable
    {
        Task<IEnumerable<Earthquake>> GetLastTenEarthquakeAsync(double latitude, double longitude, DateTime startDate, DateTime endDate);
    }
}
