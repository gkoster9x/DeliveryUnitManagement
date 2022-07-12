using DeliveryUnitManager.Reponsitory.Models.Users;
using DeliveryUnitManager.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using positionApi = DeliveryUnitManager.Reponsitory.Models.ApiModels.PositionApi;

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
        public async Task<ActionResult<IEnumerable<positionApi>>> GetPositions()
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            var positions = await _context.Positions.ToListAsync();
            var listPositionApi = new List<positionApi>();
            foreach (var position in positions)
            {
                listPositionApi.Add(new positionApi(position));
            }
            return listPositionApi;
        }


        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<positionApi>> GetPositions(long id)
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

            return new positionApi(positions);
        }

        // PUT: api/Positions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositions(long id, positionApi positionApi)
        {
            if (id != positionApi.ID)
            {
                return BadRequest();
            }
            var positions = await _context.Positions.FindAsync(id);
            positions.Code = positionApi.Code;
            positions.Name = positionApi.Name;
            positions.Description = positionApi.Description;
            positions.UpdateBy="testApi";
            positions.Updated = DateTime.Now;

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
        public async Task<ActionResult<Positions>> PostPositions(positionApi positions)
        {
            if (_context.Positions == null)
            {
                return Problem("Entity set 'DeliveryUnitDataContext.Positions'  is null.");
            }
            _context.Positions.Add(new Positions()
            {
                Name = positions.Name,
                Description = positions.Description,
                Code= positions.Code,
                Created= DateTime.Now,
                CreateBy="testApi",
                IsActive=true,

            });
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
