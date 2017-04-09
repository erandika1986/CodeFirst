using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(long id);
        Task<T> GetByIdAsync(long id);
        T GetById(string id);
        Task<T> GetByIdAsync(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(long id);
        Task DeleteAsync(long id);
        void Delete(string id);
        Task DeleteAsync(string id);
        void DeAttached(T entity);
    }
}
