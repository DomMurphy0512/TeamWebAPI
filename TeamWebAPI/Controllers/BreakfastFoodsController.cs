using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamWebAPI.Data;
using TeamWebAPI.Models;

namespace TeamWebAPI.Controllers
{
    // Define the route for the controller and specify it as an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class BreakfastFoodsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor to initialize the database context
        public BreakfastFoodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BreakfastFoods/5
        // Retrieves a specific breakfast food item by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<BreakfastFood>> GetBreakfastFood(int id)
        {
            var breakfastFood = await _context.BreakfastFood.FindAsync(id);

            // If the item is not found, return a 404 Not Found response
            if (breakfastFood == null)
            {
                return NotFound();
            }

            // Return the breakfast food item
            return breakfastFood;
        }

        // GET: api/BreakfastFoods
        // Retrieves a list of breakfast food items, or a specific item if an ID is provided in the query string
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreakfastFood>>> GetBreakfastFoods([FromQuery] int? id)
        {
            // If no ID is provided, return the first 5 items
            if (id == null || id == 0)
            {
                return await _context.BreakfastFood.Take(5).ToListAsync();
            }
            else
            {
                var breakfastFood = await _context.BreakfastFood.FindAsync(id);

                // If the item is not found, return a 404 Not Found response
                if (breakfastFood == null)
                {
                    return NotFound();
                }

                // Return the specific breakfast food item
                return Ok(breakfastFood);
            }
        }

        // POST: api/BreakfastFoods
        // Creates a new breakfast food item
        [HttpPost]
        public async Task<ActionResult<BreakfastFood>> PostBreakfastFood(BreakfastFood breakfastFood)
        {
            _context.BreakfastFood.Add(breakfastFood);
            await _context.SaveChangesAsync();

            // Return a 201 Created response with the location of the new item
            return CreatedAtAction(nameof(GetBreakfastFoods), new { id = breakfastFood.Id }, breakfastFood);
        }

        // PUT: api/BreakfastFoods/5
        // Updates an existing breakfast food item by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBreakfastFood(int id, BreakfastFood breakfastFood)
        {
            // If the ID in the URL does not match the ID in the item, return a 400 Bad Request response
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
                // If the item does not exist, return a 404 Not Found response
                if (!BreakfastFoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Return a 204 No Content response to indicate successful update
            return NoContent();
        }

        // DELETE: api/BreakfastFoods/5
        // Deletes a specific breakfast food item by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreakfastFood(int id)
        {
            var breakfastFood = await _context.BreakfastFood.FindAsync(id);

            // If the item is not found, return a 404 Not Found response
            if (breakfastFood == null)
            {
                return NotFound();
            }

            _context.BreakfastFood.Remove(breakfastFood);
            await _context.SaveChangesAsync();

            // Return a 204 No Content response to indicate successful deletion
            return NoContent();
        }

        // Helper method to check if a breakfast food item exists by ID
        private bool BreakfastFoodExists(int id)
        {
            return _context.BreakfastFood.Any(e => e.Id == id);
        }
    }
}
