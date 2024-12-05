using ClewbayFarmAPI.Dtos;
using ClewbayFarmAPI.Models;
using ClewbayFarmAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClewbayFarmAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : Controller
    {
        private readonly ClewbayFarmContext _context;
        public JobsController(ClewbayFarmContext context)
        {
            _context = context;
        }

        [HttpGet("GetJobs")]
        public async Task<ActionResult<JobListDto>> GetJobs(int year, int week)
        {
            // Calculate start and end dates of the given week
            var startDate = DateOnly.FromDateTime(DateHelper.GetDateFromWeekNumber(year, week));
            var endDate = startDate.AddDays(6); // Week runs from startDate to 6 days later

            // Propagation tasks
            var propagationTasks = await _context.ModuleTrays
                .Include(mt => mt.Crop)
                .Where(mt =>
                    mt.PlantingDate >= startDate && mt.PlantingDate <= endDate || // To be planted
                    mt.RemovalDate >= startDate && mt.RemovalDate <= endDate)    // To be removed
                .Select(mt => new
                {
                    Action = mt.PlantingDate >= startDate && mt.PlantingDate <= endDate ? "Plant" : "Remove",
                    Crop = mt.Crop.Type + " - " + mt.Crop.Variety,
                    Date = mt.PlantingDate >= startDate && mt.PlantingDate <= endDate ? mt.PlantingDate : mt.RemovalDate
                })
                .ToListAsync();

            // Bed planting tasks
            var plantingTasks = await _context.BedCrops
                .Include(bc => bc.Crop)
                .Where(bc => bc.PlantingDate >= startDate && bc.PlantingDate <= endDate)
                .Select(bc => new
                {
                    Action = "Plant into bed",
                    Crop = bc.Crop.Type + " - " + bc.Crop.Variety,
                    Bed = bc.BedId,
                    Date = bc.PlantingDate
                })
                .ToListAsync();

            // Bed harvesting tasks
            var harvestingTasks = await _context.BedCrops
                .Include(bc => bc.Crop)
                .Where(bc => bc.RemovalDate >= startDate && bc.RemovalDate <= endDate)
                .Select(bc => new
                {
                    Action = "Harvest/Remove",
                    Crop = bc.Crop.Type + " - " + bc.Crop.Variety,
                    Bed = bc.BedId,
                    Date = bc.RemovalDate
                })
                .ToListAsync();

            // Cover tasks based on CropId
            var coverTasks = await _context.BedCrops
                .Include(bc => bc.Crop)
                .ThenInclude(c => c.Covers) // Navigation to Covers table via Crop
                .Where(bc =>
                    (bc.PlantingDate <= endDate && (bc.RemovalDate == null || bc.RemovalDate >= startDate))) // Active beds
                .SelectMany(bc => bc.Crop.Covers.Select(cover => new
                {
                    Action = cover.StartWeek == week ? "Add Cover" :
                             cover.EndWeek == week ? "Remove Cover" : "Maintain Cover",
                    CoverType = cover.CoverType,
                    Crop = bc.Crop.Type + " - " + bc.Crop.Variety,
                    Bed = bc.BedId,
                    Week = week
                }))
                .ToListAsync();

            // Combine all tasks
            var jobs = propagationTasks
                .Select(selector: p => new JobDto { Action = p.Action, Crop = p.Crop, BedOrTray = null, Date = (DateTime)(p.Date?.ToDateTime(TimeOnly.MinValue)) })
                .Concat(plantingTasks
                    .Select(p => new JobDto { Action = p.Action, Crop = p.Crop, BedOrTray = $"Bed {p.Bed}", Date = (DateTime)(p.Date.ToDateTime(TimeOnly.MinValue)) }))
                .Concat(harvestingTasks
                    .Select(p => new JobDto { Action = p.Action, Crop = p.Crop, BedOrTray = $"Bed {p.Bed}", Date = (DateTime)(p.Date.ToDateTime(TimeOnly.MinValue)) }))
                .Concat(coverTasks
                    .Select(c => new JobDto
                    {
                        Action = $"{c.Action} ({c.CoverType})",
                        Crop = c.Crop,
                        BedOrTray = $"Bed {c.Bed}",
                        Date = startDate.ToDateTime(TimeOnly.MinValue) // Covers are weekly, so we'll set it to the week's start
                    }))
                .OrderBy(j => j.Date)
                .ToList();

            return Ok(new JobListDto { Week = week, Year = year, Jobs = jobs });
        }
    } 
}