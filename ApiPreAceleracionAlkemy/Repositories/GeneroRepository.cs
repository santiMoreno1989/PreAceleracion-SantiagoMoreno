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
        public GeneroRepository(ApplicationDbContext dbContext)
               : base(dbContext)
        {

        }
        public Genero GetGenero(int id)
        {
            return DbSet.Include(p => p.Peliculas).FirstOrDefault(m => m.Id == id);
        }
        public List<Genero> GetGeneros()
        {
            return DbSet.Include(p => p.Peliculas).ToList();
        }
    }
}
