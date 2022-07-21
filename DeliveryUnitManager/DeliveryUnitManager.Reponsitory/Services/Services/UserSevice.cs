using DeliveryUnitManager.Reponsitory.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DeliveryUnitManager.Reponsitory.Services.Services
{
    public class UserSevice : IService<Users>
    {
        private readonly Repository.Models.DeliveryUnitDataContext _context;
        public UserSevice(Repository.Models.DeliveryUnitDataContext context)
        {
            _context = context;
        }
        public void AddNew(Users entity)
        {
            _context.Users.Add(entity);
        }

        public void Delete(long id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
                _context.Users.Remove(user);
        }

        public Users? Get(long id)
        {
            return  _context.Users.Find(id);

        }
        public async Task<Users?> GetAsync(long id)
        {
            return await _context.Users.FindAsync(id);
            
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Users.ToList();
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(Users entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
