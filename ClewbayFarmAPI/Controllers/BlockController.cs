using ClewbayFarmAPI.Dtos;
using ClewbayFarmAPI.Models;
using ClewbayFarmAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClewbayFarmAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlockController : Controller
    {
        private readonly ClewbayFarmContext _context;
        public BlockController(ClewbayFarmContext context)
        {
            _context = context;
        }

        [HttpGet("blocks/{blockId}/crops")]
        public async Task<ActionResult<IEnumerable<BlockCropDetailsDto>>> GetCropsInBlockForWeek(
int blockId,
    int week)
        {
            // Query beds in the given block
            var blockBeds = await _context.Beds
                .Where(b => b.BlockId == blockId)
                .ToListAsync();

            if (!blockBeds.Any())
            {
                return NotFound($"No beds found for block with ID {blockId}.");
            }

            // Query crops active in the given week
            var bedCropDetails = await _context.BedCrops
                .Include(bc => bc.Crop)
                .Where(bc => blockBeds.Select(b => b.BedId).Contains(bc.BedId)
                    && (bc.PlantingWeek <= week && (bc.RemovalWeek == null || bc.RemovalWeek >= week))) // Active in week
                .Select(bc => new BlockCropDetailsDto
                {
                    BedId = bc.BedId,
                    CropType = bc.Crop.Type,
                    CropVariety = bc.Crop.Variety,
                    PlantingDate = bc.PlantingDate.ToDateTime(TimeOnly.MinValue),
                    RemovalDate = bc.RemovalDate.ToDateTime(TimeOnly.MinValue),
                    PlantingWeek = bc.PlantingWeek,
                    RemovalWeek = bc.RemovalWeek
                })
                .ToListAsync();

            return Ok(bedCropDetails);
        }
    }
}
