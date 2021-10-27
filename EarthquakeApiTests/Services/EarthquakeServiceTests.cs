using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EarthquakeApi.Services;
using EarthquakeApi.Services.Interfaces;
using EarthquakeApiTests.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EarthquakeApiTests.Services
{
    [TestClass()]
    public class EarthquakeServiceTests
    {
        private string _filePath;

        [TestInitialize]
        public void TestInitialize()
        {
            HelperFileTest.CreateFileTest();
            _filePath = HelperFileTest.GetFilePathTest();
        }

        [DataTestMethod()]
        [DataRow(61.7000, -151.700, 1)]
        [DataRow(31.1946, -111.8894, 2)]
        public void GetLastestTenEarthquakeAsyncTest(double latitude, double longitude, int indexTest)
        {
            //Arrange
            var usgsServiceMock = new Mock<IUsgsService>();
            usgsServiceMock.Setup(m => m.ReadCsv()).Callback(() => { }).Returns(() => ReadCsv());
            var earthquakeService = new EarthquakeService(usgsServiceMock.Object);

            DateTime startDate;
            DateTime endDate;
            if (indexTest == 1)
            {
                startDate = new DateTime(2021, 10, 24);
                endDate = new DateTime(2021, 10, 24);
            }
            else
            {
                startDate = new DateTime(2021, 10, 23);
                endDate = new DateTime(2021, 10, 23);
            }

            //act
            var result = earthquakeService.GetLastTenEarthquakeAsync(latitude, longitude, startDate, endDate).Result;
            var earthquakes = result.ToList();

            //assert
            if (indexTest == 1)
                Assert.AreEqual(earthquakes.Count, 10);
            else
            {
                Assert.AreEqual(earthquakes.Count, 1);
                Assert.IsTrue(earthquakes[0].Place.Contains("51km SW of Rosarito"));
            }
        }

        private async Task<IEnumerable<string>> ReadCsv()
        {
            return (await File.ReadAllLinesAsync(_filePath)).Skip(1);
        }
    }
}