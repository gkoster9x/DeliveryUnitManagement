using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryUnitManager.Reponsitory.Models.BankingQuestionInterview;
using DeliveryUnitManager.Repository.Models;
using ProjectApi = DeliveryUnitManager.Reponsitory.Models.ApiModels.ProjectDevelopApi;
using DeliveryUnitManager.Models;

namespace DeliveryUnitManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDevelopsController : ControllerBase
    {
        private readonly DeliveryUnitDataContext _context;

        public ProjectDevelopsController(DeliveryUnitDataContext context)
        {
            _context = context;
        }

        // GET: api/ProjectDevelops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectApi>>> GetProjectDevelops()
        {
            if (_context.ProjectDevelops == null)
            {
                return NotFound();
            }
            var projects = await _context.ProjectDevelops.ToListAsync();
            var projectsApi = new List<ProjectApi>();
            foreach (var project in projects)
            {
                projectsApi.Add(new ProjectApi(project));
            }
            return projectsApi;
        }

        // GET: api/ProjectDevelops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectApi>> GetProjectDevelop(long id)
        {
            if (_context.ProjectDevelops == null)
            {
                return Ok(new TokenModel(false,"No data in storage") );
            }
            var projectDevelop = await _context.ProjectDevelops.FindAsync(id);

            if (projectDevelop == null)
            {
                return Ok(new TokenModel(false,"Data not found"));
            }
            return new ProjectApi(projectDevelop);
        }

        // PUT: api/ProjectDevelops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<TokenModel> PutProjectDevelop(BankQuestionModel project)
        {
            if (project.Id == null || project.Id == 0)
            {
                return new TokenModel(false, "Project ID  is null");
            }
            var projectDevelop = await _context.ProjectDevelops.FindAsync(project.Id);
            if (projectDevelop == null)
            {
                return new TokenModel(false, "Project not found");
            }
            projectDevelop.Name = project.Name;
            projectDevelop.Code = project.Code;
            projectDevelop.Description = project.Description;
            projectDevelop.Technology   = project.Technology;
            projectDevelop.Updated = DateTime.Now;
            projectDevelop.UpdateBy = "TestApi";
            _context.Entry(projectDevelop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new TokenModel(false,ex.Message);
            }

            return new TokenModel(true,"Update succesfully");
        }

        // POST: api/ProjectDevelops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<TokenModel> PostProjectDevelop(BankQuestionModel project)
        {
            if (_context.ProjectDevelops == null)
            {
                return new TokenModel(false,"Entity set 'DeliveryUnitDataContext.ProjectDevelops'  is null.");
            }
            var projectDevelop = new ProjectDevelop()
            {
                Code = project.Code,
                Name = project.Name,
                Description = project.Description,
                Technology = project.Technology,
                Created = DateTime.Now,
                CreateBy = "TestApi",
                IsActive = true,
            };
            _context.ProjectDevelops.Add(projectDevelop);
            await _context.SaveChangesAsync();

            return new TokenModel(true, "Create project successfully");         
        }

        // DELETE: api/ProjectDevelops/5
        [HttpDelete("{id}")]
        public async Task<TokenModel> DeleteProjectDevelop(long id)
        {
            if (_context.ProjectDevelops == null)
            {
                return new TokenModel(false,"No data in storage");
            }
            var projectDevelop = await _context.ProjectDevelops.FindAsync(id);
            if (projectDevelop == null)
            {
                return new TokenModel(false,"Data not found");
            }

            _context.ProjectDevelops.Remove(projectDevelop);
            await _context.SaveChangesAsync();

            return new TokenModel(true, "Data deleted successfully");
        }

        private bool ProjectDevelopExists(long id)
        {
            return (_context.ProjectDevelops?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
