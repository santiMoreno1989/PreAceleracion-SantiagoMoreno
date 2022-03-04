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
        private PersonajeRepository _personajeRepository;

        public UnitOfWork()
        {

        }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public PersonajeRepository PersonajeRepository
        {
            get
            {
                if (_personajeRepository == null)
                {

                    _personajeRepository = new PersonajeRepository(_context);
                }
                return _personajeRepository;
            }
        }


        //    public IGeneroRepository Genero { get; }
        //    public IPeliculaRepository Pelicula { get; }
        //    public IPersonajeRepository _personajeRepository { get; }

        //    public IPersonajeRepository Personaje => throw new NotImplementedException();

        //    public UnitOfWork(ApplicationDbContext context)
        //    {
        //        _context = context;
        //        _personajeRepository = new PersonajeRepository(context);
        //    }

        //    public async Task CompleteAsync()
        //    {
        //        await _context.SaveChangesAsync();
        //    }

        //    public void Dispose()
        //    {
        //        _context.Dispose();
        //    }







    }
}
