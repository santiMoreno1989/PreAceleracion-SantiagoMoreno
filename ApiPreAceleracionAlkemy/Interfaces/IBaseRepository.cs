using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Repositories
{
     interface IBaseRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAllEntities();
        TEntity GetEntity(int id);
        TEntity AddEntity(TEntity entity);
        TEntity UpdateEntity(TEntity entity);
        void DeleteEntity(int id);
    }
}
