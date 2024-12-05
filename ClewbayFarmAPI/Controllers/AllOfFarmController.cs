using ClewbayFarmAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AllOfFarmController : ControllerBase
{
    private readonly ClewbayFarmContext _context;

    public AllOfFarmController(ClewbayFarmContext context)
    {
        _context = context;
    }

    // Get all crops
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
    public async Task<ActionResult<Crop>> CreateCrop(Crop crop)
    {
        _context.Crops.Add(crop);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCrop), new { id = crop.CropId }, crop);
    }

    // Update an existing crop
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCrop(int id, Crop crop)
    {
        if (id != crop.CropId)
        {
            return BadRequest();
        }

        _context.Entry(crop).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Crops.Any(e => e.CropId == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // Delete a crop
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCrop(int id)
    {
        var crop = await _context.Crops.FindAsync(id);
        if (crop == null)
        {
            return NotFound();
        }

        _context.Crops.Remove(crop);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
