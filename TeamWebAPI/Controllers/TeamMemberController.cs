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
        [HttpGet(Name = "GetTeamMember")]
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

        // Creates a new team member.
        [HttpPost]
        public async Task<ActionResult<TeamMember>> PostTeamMember(TeamMember teamMember)
        {
            // Adds the new team member to the context and saves changes.
            _context.TeamMembers.Add(teamMember);
            await _context.SaveChangesAsync();

            // Returns 201 Created response with the location of the new team member.
            return CreatedAtAction("GetTeamMember", new { id = teamMember.Id }, teamMember);
        }

        // Updates an existing team member.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamMember(int id, TeamMember teamMember)
        {
            // If the ID in the URL doesn't match the ID of the provided team member, returns 400 Bad Request.
            if (id != teamMember.Id)
            {
                return BadRequest();
            }

            // Marks the team member entity as modified.
            _context.Entry(teamMember).State = EntityState.Modified;

            try
            {
                // Saves the changes to the database.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If the team member doesn't exist, returns 404 Not Found.
                if (!TeamMemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    // Rethrows the exception.
                    throw;
                }
            }

            // Returns 204 No Content.
            return NoContent();
        }

        // Deletes a specified team member via ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            // Finds a team member via ID.
            var teamMember = await _context.TeamMembers.FindAsync(id);
            // If the team member isn't found, returns 404 Not Found.
            if (teamMember == null)
            {
                return NotFound();
            }

            // Removes the team member from the context and saves the changes.
            _context.TeamMembers.Remove(teamMember);
            await _context.SaveChangesAsync();

            // Returns 204 No Content.
            return NoContent();
        }

        // Method to check if a team member exists via ID.
        private bool TeamMemberExists(int id)
        {
            return _context.TeamMembers.Any(e => e.Id == id);
        }
    }
}
