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
        private readonly ApplicationDbContext _context;
        public PersonajeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
        public async Task<Personaje> GetPersonaje(int id)
        {
            return await DbSet.Include(p => p.Peliculas).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task <IEnumerable<Personaje>> GetPersonajes()
        {
            return await DbSet.Include(p => p.Peliculas).ThenInclude(g => g.Genero).ToListAsync();
        }

        public  override Personaje DeleteEntity(int id)
        {
            Personaje personaje = _context.Find<Personaje>(id);
            if (personaje.DeletedStamp == null) 
            {
                personaje.DeletedStamp = DateTime.Now;
                _context.Attach(personaje);
                _context.Entry(personaje).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return personaje;
        }

    }
}
