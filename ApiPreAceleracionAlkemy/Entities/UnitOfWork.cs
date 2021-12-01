using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Entities
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGeneroRepository Genero { get; }
        public IPeliculaRepository Pelicula { get; }
        public IPersonajeRepository Personaje { get; }
        public UnitOfWork(ApplicationDbContext context, IGeneroRepository generoRepository, IPeliculaRepository peliculaRepository, IPersonajeRepository personajeRepository)
        {
            this._context = context;
            this.Genero = generoRepository;
            this.Pelicula = peliculaRepository;
            this.Personaje = personajeRepository;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
