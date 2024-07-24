using Microsoft.AspNetCore.Mvc; // Imports the ASP.NET Core MVC namespace.
using Microsoft.EntityFrameworkCore; // Imports the Entity Framework Core namespace.
using TeamWebAPI.Data; // Includes namespace where the data context is defined.
using TeamWebAPI.Models; // Includes namespace where the models are defined.

namespace TeamWebAPI.Controllers
{
    [Route("api/[controller]")] // Defines the route for this controller.
    [ApiController] // Specifies that this is an API controller.
    public class TeamMemberController : ControllerBase
    {
        // Declares a readonly field for the database context.
        private readonly AppDbContext _context;

        // Constructor to initialize the database context via dependency injection.
        public TeamMemberController(AppDbContext context)
        {
            _context = context;
        }

        // Gets a list of team members, with an optional parameter for an ID.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamMember>>> GetTeamMembers([FromQuery] int? id)
        {
            // If no ID is given or the ID is 0, returns the first 5 team members.
            if (id == null || id == 0)
            {
                return await _context.TeamMembers.Take(5).ToListAsync();
            }

            // Finds a team member via a specified ID.
            var teamMember = await _context.TeamMembers.FindAsync(id);

            // If the team member is not found, returns 404 Not Found error message.
            if (teamMember == null)
            {
                return NotFound();
            }

            // Returns the found team member in a list.
            return Ok(new List<TeamMember> { teamMember });
        }

        // Gets a specified team member via ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamMember>> GetTeamMember(int id)
        {
            // Finds a team member by ID
            var teamMember = await _context.TeamMembers.FindAsync(id);

            // If the team member isn't found, returns 404 Not Found message.
            if (teamMember == null)
            {
                return NotFound();
            }

            // Returns found team member.
            return teamMember;
        }

        [HttpPost]
        public async Task<ActionResult<TeamMember>> PostTeamMember(TeamMember teamMember)
        {
            _context.TeamMembers.Add(teamMember);
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
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            _context.TeamMembers.Remove(teamMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamMemberExists(int id)
        {
            return _context.TeamMembers.Any(e => e.Id == id);
        }
    }
}
