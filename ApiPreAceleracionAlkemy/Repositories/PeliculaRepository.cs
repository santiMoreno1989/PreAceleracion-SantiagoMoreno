using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Repositories
{
    public class PeliculaRepository : BaseRepository<Pelicula, ApplicationDbContext>, IPeliculaRepository
    {
        public PeliculaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public Pelicula GetPelicula(int id)
        {
            return DbSet.Include(p => p.Personajes).FirstOrDefault(x => x.Id == id);
        }
        public List<Pelicula> GetPeliculas()
        {
            return DbSet.Include(p => p.Personajes).Include(x => x.Genero).ToList();
        }
    }
}
