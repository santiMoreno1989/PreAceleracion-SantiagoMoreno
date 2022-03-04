using ApiPreAceleracionAlkemy.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Repositories
{
    public interface IPersonajeRepository : IBaseRepository<Personaje>
    {
        Task <Personaje> GetPersonaje(int id);
        
        Task<IEnumerable<Personaje>> GetPersonajes();
    }
}