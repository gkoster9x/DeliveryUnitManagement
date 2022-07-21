using DeliveryUnitManager.Attributes;
using DeliveryUnitManager.Reponsitory.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using userApi = DeliveryUnitManager.Reponsitory.Models.ApiModels.UserAPI;

namespace DeliveryUnitManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly Repository.Models.DeliveryUnitDataContext _context;
        private readonly Reponsitory.Services.Services.UserSevice _service;
        public UserController( Reponsitory.Services.Services.UserSevice service)
        {
            _service = service;
        }

        //GET: api/Users
        [HttpGet]
        [CustomAuthorize(Role = "test")]
        public async Task<ActionResult<IEnumerable<userApi>>> GetUsers()
        {
            if (_service.GetAll() == null)
            {
                return NotFound();
            }
            var users = await _service.GetAllAsync();
            var usersApi = new List<userApi>();
            foreach (var user in users)
            {
                usersApi.Add(new userApi(user));
            }
            return usersApi;
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<userApi>> GetUsers(long id)
        {
            if (_service.GetAll() == null)
            {
                return NotFound();
            }
            var users = await _service.GetAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return new userApi(users);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers( Users users)
        {
            if ( users.Id == 0)
            {
                return BadRequest();
            }

            //_context.Entry(users).State = EntityState.Modified;
            _service.Update(users);

            try
            {
                await _service.SaveChangesAsync();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
            {
                if (!UsersExists(users.Id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(userApi users)
        {
            if (_service.GetAll() == null)
            {
                return Problem("Entity set 'DeliveryUnitDataContext.Users'  is null.");
            }
            var user = new Users()
            {
                Username = users.Username,
                Password = users.Password,
                Fullname = users.FullName,
                Email = users.Email,
                PhoneNumber = users.PhoneNumber,
                Address = users.Address,
                Gender =   users.Gender,
                PositionId = users.PositionID,
                DoB = users.DoB,
                IsActive= true,
                Created= DateTime.Now,
                CreateBy="testApi"
            };
            _service.AddNew(user);
            await _service.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(long id)
        {
            if (_service.GetAll() == null)
            {
                return NotFound();
            }
            var users = await _service.GetAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _service.Delete(users.Id);
            await _service.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(long id)
        {
            return (_service.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
