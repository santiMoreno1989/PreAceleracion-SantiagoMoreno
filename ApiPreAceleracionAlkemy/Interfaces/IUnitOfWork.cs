using ApiPreAceleracionAlkemy.Repositories;

namespace ApiPreAceleracionAlkemy.Entities
{
    public interface IUnitOfWork
    {
        IGeneroRepository Genero { get; }
        IPeliculaRepository Pelicula { get; }
        IPersonajeRepository Personaje { get; }

        int Complete();
        void Dispose();
    }
}