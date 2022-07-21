using DeliveryUnitManager.Reponsitory.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DeliveryUnitManager.Reponsitory.Services.Services
{
    public class PositionService : IService<Positions>
    {
        private readonly Repository.Models.DeliveryUnitDataContext _context;
        public PositionService(Repository.Models.DeliveryUnitDataContext context)
        {
            _context = context;
        }
        public void AddNew(Positions entity)
        {
           _context.Positions.Add(entity);
        }

        public void Delete(long id)
        {
            _context.Remove(id);
        }

        public Positions? Get(long id)
        {
            return _context.Positions.Find(id);
        }

        public async Task<Positions?> GetAsync(long id)
        {
            return await _context.Positions.FindAsync(id);
        }
        public IEnumerable<Positions> GetAll()
        {
            return _context.Positions.ToList();
        }

        public async Task<IEnumerable<Positions>> GetAllAsync()
        {
            return await _context.Positions.ToListAsync();
        }


        public int SaveChanges()
        {
           return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(Positions entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
