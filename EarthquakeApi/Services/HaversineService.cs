using System;
using System.Threading.Tasks;

namespace EarthquakeApi.Services
{
    public static class HaversineService
    {
        public static async Task<double> CalculatePointToPointDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var radius = 3959; // in miles // 6371.393; In kilometers
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);
 
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Asin(Math.Sqrt(a));
            return await Task.FromResult(radius * 2 * Math.Asin(Math.Sqrt(a)));
        }
 
        private static double ToRadians(double angle) {
            return Math.PI * angle / 180.0;
        }
    }
}
