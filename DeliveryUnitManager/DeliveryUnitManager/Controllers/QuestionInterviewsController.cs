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
    public class QuestionInterviewsController : ControllerBase
    {
        private readonly DeliveryUnitDataContext _context;

        public QuestionInterviewsController(DeliveryUnitDataContext context)
        {
            _context = context;
        }

        // GET: api/QuestionInterviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionInterviews>>> GetQuestions()
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            return await _context.Questions.ToListAsync();
        }

        // GET: api/QuestionInterviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionInterviews>> GetQuestionInterviews(long id)
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            var questionInterviews = await _context.Questions.FindAsync(id);

            if (questionInterviews == null)
            {
                return NotFound();
            }

            return questionInterviews;
        }

        // PUT: api/QuestionInterviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionInterviews(long id, QuestionInterviews questionInterviews)
        {
            if (id != questionInterviews.ID)
            {
                return BadRequest();
            }

            _context.Entry(questionInterviews).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionInterviewsExists(id))
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

        // POST: api/QuestionInterviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionInterviews>> PostQuestionInterviews(QuestionInterviews questionInterviews)
        {
          if (_context.Questions == null)
          {
              return Problem("Entity set 'DeliveryUnitDataContext.Questions'  is null.");
          }
            _context.Questions.Add(questionInterviews);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionInterviews", new { id = questionInterviews.ID }, questionInterviews);
        }

        // DELETE: api/QuestionInterviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionInterviews(long id)
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }
            var questionInterviews = await _context.Questions.FindAsync(id);
            if (questionInterviews == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questionInterviews);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionInterviewsExists(long id)
        {
            return (_context.Questions?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
