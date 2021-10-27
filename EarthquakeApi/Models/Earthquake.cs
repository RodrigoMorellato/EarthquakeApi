using System;
using System.Globalization;

namespace EarthquakeApi.Models
{
    public class Earthquake
    {
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }

        public Earthquake(string csvLine)
        {
            string[] values = csvLine.Split(',');

            DateTime.TryParse(values[0], out var date);
            double.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var latitude);
            double.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var longitude);
            double.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var distance);

            Date = date.Date;
            Latitude = latitude;
            Longitude = longitude;
            Distance = distance * 100;
            Place = values[13];
        }
    }
}
