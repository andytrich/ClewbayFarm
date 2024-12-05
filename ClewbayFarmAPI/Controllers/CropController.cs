using ClewbayFarmAPI.Dtos;
using ClewbayFarmAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ClewbayFarmAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CropController : Controller
    {
        private readonly ClewbayFarmContext _context;
        public CropController(ClewbayFarmContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crop>>> GetCrops()
        {
            return await _context.Crops.ToListAsync();
        }
        // Get crop by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Crop>> GetCrop(int id)
        {
            var crop = await _context.Crops.FindAsync(id);

            if (crop == null)
            {
                return NotFound();
            }

            return crop;
        }
        // Create a new crop
        [HttpPost]
        public async Task<ActionResult<Crop>> CreateCrop(CropDto cropDto)
        {
            // Map the DTO to the Crop entity
            var crop = new Crop
            {
                Type = cropDto.Type,
                Variety = cropDto.Variety,
                IsDirectSow = cropDto.IsDirectSow
            };

            // Add the crop to the database
            _context.Crops.Add(crop);
            await _context.SaveChangesAsync();

            // Return the created crop
            return CreatedAtAction(nameof(GetCrop), new { id = crop.CropId }, crop);
        }

    }
}
