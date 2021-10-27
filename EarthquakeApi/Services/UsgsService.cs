using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EarthquakeApi.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EarthquakeApi.Services
{
    public class UsgsService : IUsgsService
    {
        private readonly IConfiguration _configuration;
        private string _filePath;
        private string _usgsTempFolder;

        public UsgsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// It reads all lines from USGS .csv file using the file downloaded in the last hour.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ReadCsv()
        {
            await DownloadCsv();

            if (!File.Exists(_filePath))
                throw new DirectoryNotFoundException($"USGS earthquakes list file not found. Directory: {_usgsTempFolder}");

            return (await File.ReadAllLinesAsync(_filePath)).Skip(1);
        }

        /// <summary>
        /// It process download .csv file of USGS. It uses the file from the last hour.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task DownloadCsv()
        {
            var url =_configuration.GetValue<string>("Settings:UsgsEarthquakeUrl");

            _usgsTempFolder = Path.Combine(Path.GetTempPath(), "UsgsInfoEarthquake");
            var filename = $"all_month_{DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}_{DateTime.Now.ToString("HH", CultureInfo.InvariantCulture)}.csv";

            if (!Directory.Exists(_usgsTempFolder))
                Directory.CreateDirectory(_usgsTempFolder);

            _filePath = Path.Combine(_usgsTempFolder, filename);
            if (File.Exists(_filePath))
                return;

            DeleteOldFiles();
            await DownloadFile(url, "UsgsInfoEarthquake", filename);
        }

        /// <summary>
        /// Download file and saving it at the specific path
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        private async Task DownloadFile(string url, string path, string filename)
        {
            using WebClient client = new();
            await client.DownloadFileTaskAsync(url, $"{Path.Combine(Path.GetTempPath(), path)}\\{filename}");
        }

        /// <summary>
        /// Only delete CSV files containing "all_month" in their filenames
        /// </summary>
        private void DeleteOldFiles()
        {
            var fileList = Directory.GetFiles(_usgsTempFolder, _configuration.GetValue<string>("Settings:UsgsFilesToDelete"));
            foreach (var file in fileList)
            {
                File.Delete(file);
            }
        }

        public void Dispose()
        {
        }
    }
}
