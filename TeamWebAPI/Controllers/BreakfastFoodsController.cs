using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamWebAPI.Data;
using TeamWebAPI.Models;

namespace TeamWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreakfastFoodsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BreakfastFoodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BreakfastFoods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BreakfastFood>> GetBreakfastFood(int id)
        {
            var breakfastFood = await _context.BreakfastFood.FindAsync(id);

            if (breakfastFood == null)
            {
                return NotFound();
            }

            return breakfastFood;
        }


        // GET: api/BreakfastFoods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreakfastFood>>> GetBreakfastFoods([FromQuery] int? id)
        {
            if (id == null || id == 0)
            {
                return await _context.BreakfastFood.Take(5).ToListAsync();
            }
            else
            {
                var breakfastFood = await _context.BreakfastFood.FindAsync(id);

                if (breakfastFood == null)
                {
                    return NotFound();
                }

                return Ok(breakfastFood);
            }
        }

        // POST: api/BreakfastFoods
        [HttpPost]
        public async Task<ActionResult<BreakfastFood>> PostBreakfastFood(BreakfastFood breakfastFood)
        {
            _context.BreakfastFood.Add(breakfastFood);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBreakfastFoods), new { id = breakfastFood.Id }, breakfastFood);
        }

        // PUT: api/BreakfastFoods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBreakfastFood(int id, BreakfastFood breakfastFood)
        {
            if (id != breakfastFood.Id)
            {
                return BadRequest();
            }

            _context.Entry(breakfastFood).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreakfastFoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/BreakfastFoods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreakfastFood(int id)
        {
            var breakfastFood = await _context.BreakfastFood.FindAsync(id);
            if (breakfastFood == null)
            {
                return NotFound();
            }

            _context.BreakfastFood.Remove(breakfastFood);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BreakfastFoodExists(int id)
        {
            return _context.BreakfastFood.Any(e => e.Id == id);
        }
    }
}
