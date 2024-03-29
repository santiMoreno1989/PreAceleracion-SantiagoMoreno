﻿using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
              _dbSet.Add(entity);
           await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<T> FindByCondition(Func<T, bool> condition)
            => _dbSet.Where(condition);

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> condition = null,Func<IQueryable<T>,IIncludableQueryable<T,object>> include= null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (condition is not null)
                query = query.Where(condition);

            if (include is not null)
                query = include(query);

            return query;
        }
    }
}
