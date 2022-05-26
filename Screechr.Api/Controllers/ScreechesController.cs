using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Screechr.Api.Entities;
using Screechr.Api.Helpers;
using Screechr.Api.Models;

namespace Screechr.Api.Controllers
{
    /// <summary>
    /// The screeches controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ScreechesController : ControllerBase
    {
        private readonly ScreechrsDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreechesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ScreechesController(ScreechrsDbContext context)
        {
            _context = context;
        }

        // GET: api/Screeches
        /// <summary>
        /// Gets the screechs.
        /// </summary>
        /// <returns>A Task.</returns>
        [HttpGet]
        [Authorize]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Screech>>> GetScreechs(
            string? select,
            string? filter,
            string orderby = "CreatedDate desc",
            int top = 50,
            int skip = 0
        )
        {
            if (_context.Screechs == null)
            {
                return NotFound();
            }
            // Default sort order is creation date in descending order
            return await _context.Screechs.ToListAsync();
        }

        // GET: api/Screeches/5
        /// <summary>
        /// Gets the screech.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Screech>> GetScreech(int id)
        {
            if (_context.Screechs == null)
            {
                return NotFound();
            }
            var screech = await _context.Screechs.FindAsync(id);

            if (screech == null)
            {
                return NotFound();
            }

            return screech;
        }

        // PUT: api/Screeches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Puts the screech.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="screech">The screech.</param>
        /// <returns>A Task.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutScreech(int id, Screech screech)
        {
            if (id != screech.Id)
            {
                return BadRequest();
            }

            _context.Entry(screech).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreechExists(id))
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

        // POST: api/Screeches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Posts the screech.
        /// </summary>
        /// <param name="screech">The screech.</param>
        /// <returns>A Task.</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Screech>> PostScreech(Screech screech)
        {
            if (_context.Screechs == null)
            {
                return Problem("Entity set 'ScreechrsDbContext.Screechs'  is null.");
            }
            _context.Screechs.Add(screech);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScreech", new { id = screech.Id }, screech);
        }

        // DELETE: api/Screeches/5
        /// <summary>
        /// Deletes the screech.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteScreech(int id)
        {
            if (_context.Screechs == null)
            {
                return NotFound();
            }
            var screech = await _context.Screechs.FindAsync(id);
            if (screech == null)
            {
                return NotFound();
            }

            _context.Screechs.Remove(screech);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScreechExists(int id)
        {
            return (_context.Screechs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
