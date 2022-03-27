using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Interfaces;
using System;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private IRepository<Genero> _generoRepository;
        private IRepository<Pelicula> _peliculaRepository;
        private IRepository<Personaje> _personajeRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public IRepository<Genero> generoRepository => _generoRepository ?? new Repository<Genero>(_dbContext);

        //public IRepository<Pelicula> peliculaRepository => _peliculaRepository ?? new Repository<Pelicula>(_dbContext);

        //public IRepository<Personaje> personajeRepository => _personajeRepository ?? new Repository<Personaje>(_dbContext);


        public IRepository<Genero> generoRepository
        {
            get
            {
                if (_generoRepository == null)
                {
                    _generoRepository = new Repository<Genero>(_dbContext);
                }
                return _generoRepository;
            }
        }

        public IRepository<Pelicula> peliculaRepository
        {
            get
            {
                if (_peliculaRepository == null)
                {
                    _peliculaRepository = new Repository<Pelicula>(_dbContext);
                }
                return _peliculaRepository;
            }
        }

        public IRepository<Personaje> personajeRepository
        {
            get
            {
                if (_personajeRepository == null)
                {
                    _personajeRepository = new Repository<Personaje>(_dbContext);
                }
                return _personajeRepository;
            }
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
