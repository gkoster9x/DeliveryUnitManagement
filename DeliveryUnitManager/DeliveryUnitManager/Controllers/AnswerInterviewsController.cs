using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryUnitManager.Reponsitory.Models.BankingQuestionInterview;
using DeliveryUnitManager.Repository.Models;

namespace DeliveryUnitManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerInterviewsController : ControllerBase
    {
        private readonly DeliveryUnitDataContext _context;

        public AnswerInterviewsController(DeliveryUnitDataContext context)
        {
            _context = context;
        }

        // GET: api/AnswerInterviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerInterviews>>> GetAnswers()
        {
          if (_context.Answers == null)
          {
              return NotFound();
          }
            return await _context.Answers.ToListAsync();
        }

        // GET: api/AnswerInterviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerInterviews>> GetAnswerInterviews(long id)
        {
          if (_context.Answers == null)
          {
              return NotFound();
          }
            var answerInterviews = await _context.Answers.FindAsync(id);

            if (answerInterviews == null)
            {
                return NotFound();
            }

            return answerInterviews;
        }

        // PUT: api/AnswerInterviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswerInterviews(long id, AnswerInterviews answerInterviews)
        {
            if (id != answerInterviews.ID)
            {
                return BadRequest();
            }

            _context.Entry(answerInterviews).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerInterviewsExists(id))
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

        // POST: api/AnswerInterviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnswerInterviews>> PostAnswerInterviews(AnswerInterviews answerInterviews)
        {
          if (_context.Answers == null)
          {
              return Problem("Entity set 'DeliveryUnitDataContext.Answers'  is null.");
          }
            _context.Answers.Add(answerInterviews);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswerInterviews", new { id = answerInterviews.ID }, answerInterviews);
        }

        // DELETE: api/AnswerInterviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswerInterviews(long id)
        {
            if (_context.Answers == null)
            {
                return NotFound();
            }
            var answerInterviews = await _context.Answers.FindAsync(id);
            if (answerInterviews == null)
            {
                return NotFound();
            }

            _context.Answers.Remove(answerInterviews);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnswerInterviewsExists(long id)
        {
            return (_context.Answers?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
