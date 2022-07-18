using DeliveryUnitManager.Reponsitory.Models.BankingQuestionInterview;
using DeliveryUnitManager.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionApi = DeliveryUnitManager.Reponsitory.Models.ApiModels.BankingQuestionApi;

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
        public async Task<ActionResult<IEnumerable<QuestionApi>>> GetQuestions()
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }

            var listQuestions = await _context.Questions.ToListAsync();
            List<QuestionApi> questionApis = new List<QuestionApi>();
            foreach (var item in listQuestions)
            {
                questionApis.Add(new QuestionApi(item));
            }
            return questionApis;
        }

        // GET: api/QuestionInterviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionApi>> GetQuestionInterviews(long id)
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

            return new QuestionApi(questionInterviews);
        }

        // PUT: api/QuestionInterviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionInterviews(long id, QuestionApi question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }
            var questionInterview = await _context.Questions.FindAsync(id);
            questionInterview.Question = question.Question;
            questionInterview.Type = question.Type;
            questionInterview.PlatformType = question.PlatformType;
            questionInterview.Level = question.Level;
            questionInterview.Projects = question.Projects;
            questionInterview.Updated = DateTime.Now;
            questionInterview.UpdateBy="testApi";

            _context.Entry(questionInterview).State = EntityState.Modified;

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
        public async Task<ActionResult<QuestionApi>> PostQuestionInterviews(QuestionApi question)
        {
            if (_context.Questions == null)
            {
                return Problem("Entity set 'DeliveryUnitDataContext.Questions'  is null.");
            }
            var questionInterviews = new QuestionInterviews()
            {
                Question = question.Question,
                PlatformType = question.PlatformType,
                Type= question.Type,
                Level = question.Level,
                Projects = question.Projects,
                IsActive=true,
                Created = DateTime.Now,
                CreateBy="testApi"
            };
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
