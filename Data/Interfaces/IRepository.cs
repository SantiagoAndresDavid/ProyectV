using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        public Task Save(TEntity entity);
        
        public Task Delete(TEntity entity);
        
        public Task Update(TEntity entity);

        public Task<List<TEntity>> GetAll();
    }
}