using ApiPreAceleracionAlkemy.Entities;
using System.Collections.Generic;

namespace ApiPreAceleracionAlkemy.Repositories
{
    public interface IPersonajeRepository : IBaseRepository<Personaje>
    {
        Personaje GetPersonaje(int id);
        
        List<Personaje> GetPersonajes();
    }
}