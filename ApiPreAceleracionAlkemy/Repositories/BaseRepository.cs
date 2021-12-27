using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Repositories
{
    public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private DbSet<TEntity> _dbSet;
        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet ??= _dbContext.Set<TEntity>(); }
        }
        protected BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<TEntity> GetAllEntities()
        {
            return _dbContext.Set<TEntity>().ToList();
        }
        public TEntity GetEntity(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);

        }
        public TEntity AddEntity(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public TEntity UpdateEntity(TEntity entity)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;

        }
        public virtual TEntity DeleteEntity(int id)
        {
            var delete = _dbContext.Find<TEntity>(id);
            _dbContext.Remove(delete);
            _dbContext.SaveChanges();
            return delete;
        }
    }
}