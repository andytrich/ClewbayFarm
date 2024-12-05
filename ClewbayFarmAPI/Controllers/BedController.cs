using ClewbayFarmAPI.Dtos;
using ClewbayFarmAPI.Models;
using ClewbayFarmAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClewbayFarmAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BedController : Controller
    {
        private readonly ClewbayFarmContext _context;
        public BedController(ClewbayFarmContext context)
        {
            _context = context;
        }

        [HttpGet("beds/{bedId}/crops")]
        public async Task<ActionResult<IEnumerable<BedCrop>>> GetCropsInBed(int bedId, int startWeek, int endWeek)
        {
            var crops = await _context.BedCrops
                .Include(bc => bc.Crop)
                .Where(bc => bc.BedId == bedId &&
                             (bc.PlantingWeek <= endWeek && bc.RemovalWeek >= startWeek))
                .ToListAsync();

            return Ok(crops);
        }
        [HttpGet("beds/{bedId}/gaps")]
        public async Task<ActionResult<IEnumerable<object>>> GetBedGaps(int bedId, int startWeek, int endWeek)
        {
            var crops = await _context.BedCrops
                .Where(bc => bc.BedId == bedId &&
                             (bc.PlantingWeek <= endWeek && bc.RemovalWeek >= startWeek))
                .OrderBy(bc => bc.PlantingWeek)
                .ToListAsync();

            var gaps = new List<object>();
            var currentWeek = startWeek;

            foreach (var crop in crops)
            {
                if (currentWeek < crop.PlantingWeek)
                {
                    gaps.Add(new
                    {
                        StartWeek = currentWeek,
                        EndWeek = crop.PlantingWeek - 1
                    });
                }
                currentWeek = crop.RemovalWeek + 1;
            }

            if (currentWeek <= endWeek)
            {
                gaps.Add(new
                {
                    StartWeek = currentWeek,
                    EndWeek = endWeek
                });
            }

            return Ok(gaps);
        }
        [HttpPost("beds/{bedId}/allocate")]
        public async Task<ActionResult> AllocateCropToBed(int bedId, [FromBody] AllocateCropDto request)
        {
            // Validate crop existence
            var crop = await _context.Crops
                .Include(c => c.CropBedAttribute)
                .FirstOrDefaultAsync(c => c.CropId == request.CropId);

            if (crop == null)
                return NotFound($"Crop with ID {request.CropId} not found.");

            // Calculate removal date
            var plantingDate = DateHelper.GetDateFromWeekNumber(request.PlantingYear, request.PlantingWeek);
            var removalDate = plantingDate.AddDays(crop.CropBedAttribute.TimeToMaturity);

            // Add entry to BedCrops
            var bedCrop = new BedCrop
            {
                BedId = bedId,
                CropId = request.CropId,
                PlantingDate = DateOnly.FromDateTime(plantingDate),
                RemovalDate = DateOnly.FromDateTime(removalDate)
            };

            _context.BedCrops.Add(bedCrop);

            // Calculate seeds and modules
            //var seedsRequired = crop.CropBedAttribute.PlantSpacing > 0 && crop.CropBedAttribute.RowSpacing > 0
            //    ? (int)(crop.CropBedAttribute.RowSpacing
            //    * crop.CropBedAttribute.PlantSpacing
            //    / 144.0) // ft² calculation
            //    : 0;
            var seedsRequired = 100;

            //var moduleTrays = (int)Math.Ceiling(seedsRequired / 128.0); // Assuming 128 modules per tray
            var moduleTrays = 100;

            // Add entry to ModuleTrays
            var propagationArea = await _context.PropagationAreas.FirstOrDefaultAsync(); // Simplified selection
            if (propagationArea == null)
                return BadRequest("No propagation areas available.");

            var moduleTray = new ModuleTray
            {
                AreaId = propagationArea.AreaId,
                TrayTypeId = 1, // Assuming TrayTypeId = 1 corresponds to 128-cell trays
                CropId = request.CropId,
                SeedsPerModule = seedsRequired,
                PlantingDate = DateOnly.FromDateTime(DateTime.UtcNow),
                RemovalDate = DateOnly.FromDateTime(plantingDate)
            };

            _context.ModuleTrays.Add(moduleTray);

            // Save changes
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Crop allocated to bed and propagation entry created.", BedCropId = bedCrop.BedCropId });
        }

    }
}
