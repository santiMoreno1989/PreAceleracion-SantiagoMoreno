using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Interfaces;
using ApiPreAceleracionAlkemy.Repositories;
using System;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Genero> generoRepository { get; }
        IRepository<Pelicula> peliculaRepository { get; }
        IRepository<Personaje> personajeRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
