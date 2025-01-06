using ClewbayFarmAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ClewbayFarmAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MushroomController : Controller
    {
        private readonly ClewbayFarmContext _context;

        public MushroomController(ClewbayFarmContext context)
        {
            _context = context;
        }

        public class SensorData
        {
            public float Temperature { get; set; }
            public float Humidity { get; set; }
            public int Co2Level { get; set; }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                // Retrieve all records from the MushroomEnv table
                var allRecords = _context.MushroomEnv
                    .OrderByDescending(m => m.RecordedDateTime) // Optional: order by date
                    .ToList();

                return Ok(allRecords); // Return the records as JSON
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while retrieving the data." });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] SensorData data)
        {
            if (data == null)
            {
                return BadRequest(new { Message = "Invalid sensor data." });
            }

            try
            {
                // Create a new MushroomEnv entity
                var mushroomEnv = new MushroomEnv
                {
                    Temperature = data.Temperature,
                    Humidity = data.Humidity,
                    CO2Level = data.Co2Level,
                    RecordedDateTime = DateTime.UtcNow // Use UTC time for consistency
                };

                // Add the entity to the database context
                _context.MushroomEnv.Add(mushroomEnv);

                // Save changes to the database
                _context.SaveChanges();

                // Return a success response
                return Ok(new { Message = "Data saved successfully" });
            }
            catch (Exception ex)
            {
                // Log the error and return a server error response
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while saving the data." });
            }
        }
    }
}
