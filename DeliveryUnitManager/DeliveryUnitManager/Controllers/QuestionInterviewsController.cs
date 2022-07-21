using DeliveryUnitManager.Models;
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
        private readonly List<string> OrderParams = new List<string>()
        {
            "Question",
            "Project",
            "Platform",
            "Level",
        };
        public QuestionInterviewsController(DeliveryUnitDataContext context)
        {
            _context = context;
        }

        [HttpGet("getOrder")]
        public async Task<List<string>> GetOrderParameters()
        {
            return await Task.Run(() =>
            {
                return OrderParams;
            });
        }
        // GET: api/QuestionInterviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionApi>>> GetQuestions(string? platformType, string? level, string? project, int? page, string? order)
        {
            try
            {

            if (_context.Questions == null)
            {
                return NotFound();
            }
            var query = _context.Questions.AsQueryable();
            if (!string.IsNullOrEmpty(project))
            {
                if (!project.Equals("undefined"))
                    query = query.Where(q => q.Projects.Equals(project));
            }

            if (!string.IsNullOrEmpty(platformType))
            {
                if (!platformType.Equals("undefined"))
                    query = query.Where(q => !string.IsNullOrEmpty(q.PlatformType) && q.PlatformType.Equals(platformType));
            }
            if (!string.IsNullOrEmpty(level) )
            {
                if (!level.Equals("undefined"))
                    query = query.Where(q => q.Level.Equals(level));
            }
            if (!string.IsNullOrEmpty(order) )
            {
                if (!order.Equals("undefined"))
                    switch (order)
                {
                    case "Level":
                        query = query.OrderBy(q => q.Level);
                        break;
                    case "Question":
                        query = query.OrderBy(q => q.Question);
                        break;
                    case "Platform":
                        query = query.OrderBy(q => q.PlatformType);
                        break;
                    case "Project":
                        query = query.OrderBy(q => q.Projects);
                        break;
                }
            }
            var listQuestions = await query.ToListAsync();
            List<QuestionApi> questionApis = new List<QuestionApi>();
            foreach (var item in listQuestions)
            {
                questionApis.Add(new QuestionApi(item));
            }
            return questionApis;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        // GET: api/QuestionInterviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionApi>> GetQuestionInterviews(long id)
        {
            if (_context.Questions == null)
            {
                return Ok(new TokenModel(false, "No data in storage"));
            }
            var questionInterviews = await _context.Questions.FindAsync(id);

            if (questionInterviews == null)
            {
                return Ok(new TokenModel(false, "Data not found"));
            }

            return new QuestionApi(questionInterviews);
        }

        // PUT: api/QuestionInterviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<TokenModel> PutQuestionInterviews(BankingQuestionModel question)
        {
            if (question.Id == 0)
            {
                return new TokenModel(false, "Question ID  is null");
            }
            var questionInterview = await _context.Questions.FindAsync(question.Id);
            if (questionInterview == null)
            {
                return new TokenModel(false, "Question not found");
            }
            questionInterview.Question = question.Question;
            questionInterview.Answer = question.Answer;
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
            catch (Exception ex)
            {
                if (!QuestionInterviewsExists(question.Id))
                {
                    return new TokenModel(false, "Question not found");
                }
                else
                {
                    return new TokenModel(false, ex.Message);
                }
            }

            return new TokenModel(true, "Update succesfully");
        }

        // POST: api/QuestionInterviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<TokenModel> PostQuestionInterviews(BankingQuestionModel question)
        {
            if (_context.Questions == null)
            {
                return new TokenModel(false, "Entity set 'DeliveryUnitDataContext.Questions'  is null.");
            }
            var questionInterviews = new QuestionInterviews()
            {
                Question = question.Question,
                PlatformType = question.PlatformType,
                Answer= question.Answer,
                Level = question.Level,
                Projects = question.Projects,
                IsActive=true,
                Created = DateTime.Now,
                CreateBy="testApi"
            };
            _context.Questions.Add(questionInterviews);
            await _context.SaveChangesAsync();

            return new TokenModel(true, "Create question successfully");
        }

        // DELETE: api/QuestionInterviews/5
        [HttpDelete("{id}")]
        public async Task<TokenModel> DeleteQuestionInterviews(long id)
        {
            if (_context.Questions == null)
            {
                return new TokenModel(false, "No data in storage");
            }
            var questionInterviews = await _context.Questions.FindAsync(id);
            if (questionInterviews == null)
            {
                return new TokenModel(false, "Data not found");
            }

            _context.Questions.Remove(questionInterviews);
            await _context.SaveChangesAsync();

            return new TokenModel(true, "Data deleted successfully");
        }

        private bool QuestionInterviewsExists(long id)
        {
            return (_context.Questions?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
