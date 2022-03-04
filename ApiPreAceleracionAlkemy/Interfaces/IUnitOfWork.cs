using ApiPreAceleracionAlkemy.Repositories;

namespace ApiPreAceleracionAlkemy.Entities
{
    public interface IUnitOfWork
    {
        PersonajeRepository PersonajeRepository { get; }
    }
}