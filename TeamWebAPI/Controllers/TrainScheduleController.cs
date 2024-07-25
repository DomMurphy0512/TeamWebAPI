using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamWebAPI.Data;
using TeamWebAPI.Models;

namespace TeamWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainSchedule : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrainSchedule(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(TrainSchedule){
            
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetTrainSchedule(int id)
        {
            var TrainSchedule = await _context.TrainSchedule.FindAsync(id);

            if (teamMember == null)
            {
                return NotFound();
            }

            return teamMember;
        }

        [HttpPost]
        public async Task<ActionResult<TeamMember>> PostTrainSchedule(TeamMember teamMember)
        {
            _context.TrainSchedule.Add(teamMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeamMember", new { id = teamMember.Id }, teamMember);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamMember(int id, TeamMember teamMember)
        {
            if (id != teamMember.Id)
            {
                return BadRequest();
            }

            _context.Entry(teamMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamMemberExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var teamMember = await _context.TrainSchedule.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            _context.TrainSchedule.Remove(teamMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamMemberExists(int id)
        {
            return _context.TrainSchedule.Any(e => e.Id == id);
        }
    }
}
