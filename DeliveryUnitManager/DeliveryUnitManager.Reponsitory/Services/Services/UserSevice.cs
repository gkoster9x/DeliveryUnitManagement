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

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
                _context.Users.Remove(user);
        }

        public Users Get(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return new Users();
            return user;
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Users.ToList();
        }

        public int SaveChange()
        {
            return _context.SaveChanges();
        }

        public void Update(Users entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
