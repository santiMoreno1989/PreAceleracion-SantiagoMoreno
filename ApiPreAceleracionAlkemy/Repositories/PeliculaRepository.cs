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
        private readonly ApplicationDbContext _context;
        public PeliculaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
             _context= dbContext;
        }
        public Pelicula GetPelicula(int id)
        {
            return DbSet.Include(p => p.Personajes).FirstOrDefault(x => x.Id == id);
        }
        public List<Pelicula> GetPeliculas()
        {
            return DbSet.Include(p => p.Personajes).Include(x => x.Genero).ToList();
        }
        public override Pelicula DeleteEntity(int id)
        {
            Pelicula pelicula = _context.Find<Pelicula>(id);
            if (pelicula.Deleted == null) 
            {
                pelicula.Deleted = DateTime.Now;
                _context.Attach(pelicula);
                _context.Entry(pelicula).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return pelicula;
        }
    }
}
