using ApiPreAceleracionAlkemy.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    public interface IPersonajeService
    {
        Task<Personaje> Add(Personaje entity);
        Task Delete(int id);
        Task<IEnumerable<Personaje>> GetAll();
        Task<Personaje> GetById(int id);
        Task<Personaje> Update(Personaje entity);
    }
}