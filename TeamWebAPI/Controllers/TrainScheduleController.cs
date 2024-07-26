using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamWebAPI.Data;
using TeamWebAPI.Models;


namespace TeamWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainScheduleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrainScheduleController(AppDbContext context)
        {
            _context = context;
        }

    //GET : api/TrainSchedule
    //Retrieve train schedule(s)
        [HttpGet]

        public async Task<ActionResult<IEnumerable<TrainSchedule>>> GetTrainSchedules([FromQuery] int? id)
        {
            //return last 4 records if no ID is provided
            if (id == null || id == 0)
            {
                return await _context.TrainSchedules.Take(4).ToListAsync();
            }
            //return schedule by ID
            var trainSchedule = await _context.TrainSchedules.FindAsync(id);

            if (trainSchedule == null)
            {
                return NotFound();
            }
            //return schedules in a list
            return Ok(new List<TrainSchedule> { trainSchedule });

        }

//Add new train route
        [HttpPost]

        public async Task<ActionResult<TrainSchedule>> PostTrainSchedule(TrainSchedule trainSchedule)
        {
            _context.TrainSchedules.Add(trainSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainSchedules", new { id = trainSchedule.Id }, trainSchedule);
        }

//Update an existing train route by ID
        [HttpPut("{id}")]

       //IAction - defines HTTP status return type  
        public async Task<IActionResult> PutTrainSchedule(int id, TrainSchedule trainSchedule)
        {
            if (id != trainSchedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainScheduleExists(id))
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
//Delete a record by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainSchedule(int id)
        {
            var trainSchedule = await _context.TrainSchedules.FindAsync(id);
            if (trainSchedule == null)
            {
                return NotFound();
            }

            _context.TrainSchedules.Remove(trainSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainScheduleExists(int id)
        {
            return _context.TrainSchedules.Any(e => e.Id == id);
        }
    }
}
