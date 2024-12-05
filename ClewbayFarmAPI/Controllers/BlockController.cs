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

        [HttpGet("{blockId}/GetCropsInBlock")]
        public async Task<ActionResult<IEnumerable<BlockCropDetailsDto>>> GetCropsInBlock(
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
                    && bc.PlantingWeek <= week && bc.RemovalWeek >= week) // Active in week
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
        [HttpGet("{blockId}/GetGapsForBlock")]
        public async Task<ActionResult<IEnumerable<GapDto>>> GetGapsForBlock(int blockId, [FromQuery] int week)
        {
            // Fetch all beds for the given block
            var beds = await _context.Beds
                .Where(b => b.BlockId == blockId)
                .ToListAsync();

            if (!beds.Any())
            {
                return NotFound($"No beds found for block with ID {blockId}.");
            }

            // Collect gaps for each bed in the block
            var gaps = new List<GapDto>();

            foreach (var bed in beds)
            {
                // Fetch crops for this bed
                var cropsInBed = await _context.BedCrops
                    .Where(bc => bc.BedId == bed.BedId)
                    .OrderBy(bc => bc.PlantingWeek)
                    .ToListAsync();

                int currentWeek = week;

                foreach (var crop in cropsInBed)
                {
                    if (currentWeek < crop.PlantingWeek)
                    {
                        // Add a gap before the next crop is planted
                        gaps.Add(new GapDto
                        {
                            BedId = bed.BedId,
                            StartWeek = currentWeek,
                            EndWeek = crop.PlantingWeek - 1
                        });
                    }

                    // Update current week to after this crop's removal
                    currentWeek = crop.RemovalWeek + 1;
                }

                // Add a gap after the last crop, if any
                if (currentWeek <= 52) // Assuming 52 weeks in a year
                {
                    gaps.Add(new GapDto
                    {
                        BedId = bed.BedId,
                        StartWeek = currentWeek,
                        EndWeek = 52
                    });
                }
            }

            return Ok(gaps);
        }
    }
}
