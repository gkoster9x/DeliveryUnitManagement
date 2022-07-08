using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitManager.Reponsitory.Services
{
    public interface IService<T>
    {
        public T Get(int id);
        public IEnumerable<T> GetAll();
        public int SaveChange();
        public void AddNew(T entity);
        public void Delete(int id);
        public void Update(T entity);

    }
}
