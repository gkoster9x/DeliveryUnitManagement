using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryUnitManager.Reponsitory.Models.Users;
using DeliveryUnitManager.Repository.Models;

namespace DeliveryUnitManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly DeliveryUnitDataContext _context;

        public PositionsController(DeliveryUnitDataContext context)
        {
            _context = context;
        }

        //GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Positions>>> GetPositions()
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            return await _context.Positions.ToListAsync();
        }


        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Positions>> GetPositions(long id)
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            var positions = await _context.Positions.FindAsync(id);

            if (positions == null)
            {
                return NotFound();
            }

            return positions;
        }

        // PUT: api/Positions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositions(long id, Positions positions)
        {
            if (id != positions.ID)
            {
                return BadRequest();
            }

            _context.Entry(positions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionsExists(id))
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

        // POST: api/Positions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Positions>> PostPositions(Positions positions)
        {
            if (_context.Positions == null)
            {
                return Problem("Entity set 'DeliveryUnitDataContext.Positions'  is null.");
            }
            _context.Positions.Add(positions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPositions", new { id = positions.ID }, positions);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositions(long id)
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            var positions = await _context.Positions.FindAsync(id);
            if (positions == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(positions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionsExists(long id)
        {
            return (_context.Positions?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
