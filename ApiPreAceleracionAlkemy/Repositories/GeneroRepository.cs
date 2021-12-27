using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Repositories
{
    public class GeneroRepository : BaseRepository<Genero, ApplicationDbContext>, IGeneroRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GeneroRepository(ApplicationDbContext dbContext)
               : base(dbContext)
        {
            _applicationDbContext = dbContext;
        }
        public Genero GetGenero(int id)
        {
            return DbSet.Include(p => p.Peliculas).FirstOrDefault(m => m.Id == id);
        }
        public List<Genero> GetGeneros()
        {
            return DbSet.Include(p => p.Peliculas).ThenInclude(q => q.Personajes).ToList();
        }
        public override Genero DeleteEntity(int id)
        {
            Genero genero = _applicationDbContext.Find<Genero>(id);
            if(genero.TimeStams == null)
                      
                genero.TimeStams = DateTime.Now;
                _applicationDbContext.Attach(genero);
                _applicationDbContext.Entry(genero).State = EntityState.Modified;
                _applicationDbContext.SaveChanges();
                       
            
            return genero;
        }
    }
}
