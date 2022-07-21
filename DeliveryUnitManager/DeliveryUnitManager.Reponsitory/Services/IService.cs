using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitManager.Reponsitory.Services
{
    public interface IService<T>
    {
        public T? Get(long id);
        public Task<T?> GetAsync(long id);

        public IEnumerable<T> GetAll();
        public Task<IEnumerable<T>> GetAllAsync();
        public int SaveChanges();
        public Task<int> SaveChangesAsync();

        public void AddNew(T entity);
        public void Delete(long id);
        public void Update(T entity);


    }
}
