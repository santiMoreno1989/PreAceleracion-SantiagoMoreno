using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Repositories
{
     public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task <IEnumerable<TEntity>> GetAllEntities();
        Task<TEntity> GetEntity(int id);
        Task<TEntity> AddEntity(TEntity entity);
        Task<TEntity> UpdateEntity(TEntity entity);
        TEntity DeleteEntity(int id);
    }
}
