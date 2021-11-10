using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Repositories
{
    public class PersonajeRepository : BaseRepository<Personaje, ApplicationDbContext>, IPersonajeRepository
    {
        public PersonajeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public Personaje GetPersonaje(int id)
        {
            return DbSet.Include(p => p.Peliculas).FirstOrDefault(x => x.Id == id);
        }
        public List<Personaje> GetPersonajes()
        {
            return DbSet.Include(p => p.Peliculas).ThenInclude(g => g.Genero).ToList();
        }
        
    }
}
