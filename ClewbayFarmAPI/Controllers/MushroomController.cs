using Microsoft.AspNetCore.Mvc;

namespace ClewbayFarmAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MushroomController : Controller
    {
        //        private static readonly string[] Summaries = new[]
        //{
        //            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //        };
        //        [HttpGet()]
        //        public IEnumerable<WeatherForecast> Get()
        //        {
        //            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //            {
        //                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //                TemperatureC = Random.Shared.Next(-20, 55),
        //                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //            })
        //            .ToArray();
        //        }

        public class SensorData
        {
            public float Temperature { get; set; }
            public float Humidity { get; set; }
            public int Co2Level { get; set; }
        }

        [HttpPost]
        public IActionResult Post([FromBody] SensorData data)
        {
            // Log or process the data
            Console.WriteLine($"Temperature: {data.Temperature}, Humidity: {data.Humidity}, CO2 Level: {data.Co2Level}");

            // Return a response
            return Ok(new { Message = "Data received successfully" });
        }
    }
}
