using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using EarthquakeApi.Models;
using EarthquakeApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EarthquakeApi.Controllers
{
    /// <summary>
    /// API Earthquake information
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EarthquakeController : ControllerBase
    {
        private readonly IEarthquakeService _earthquakeService;

        public EarthquakeController(IEarthquakeService earthquakeService)
        {
            _earthquakeService = earthquakeService;
        }

        /// <summary>
        /// Verifies and return a list of the last 10 earthquakes which impacted at requested area
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Earthquake>))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Earthquake>>> GetLastTenEarthquake(double latitude, double longitude, DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _earthquakeService.GetLastTenEarthquakeAsync(latitude, longitude, startDate, endDate);

                return result.Count() == 0 
                    ? NotFound()
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
