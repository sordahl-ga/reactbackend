using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using reactbackend.Providers;
namespace reactbackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private IConfiguration _config;
        private IDocumentDBRepository<WeatherForecast> _docdbrepo = null;
        public SampleDataController(IConfiguration configuration)
        {
            _config = configuration;
           
        }
        private IDocumentDBRepository<WeatherForecast> GetDB()
        {
            if (_docdbrepo == null) _docdbrepo = new DocumentDBRepository<WeatherForecast>(_config);
            return _docdbrepo;
        }
        [HttpGet("[action]")]
        public async Task<IEnumerable<WeatherForecast>> WeatherForecasts()
        {
            try
            {
                return await GetDB().GetItemsAsync(t => t.Temperature > -1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<WeatherForecast>();
            }
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int Temperature { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(Temperature / 0.5556);
                }
            }
        }
    }
}
