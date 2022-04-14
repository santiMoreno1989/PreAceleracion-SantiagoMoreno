using ApiPreAceleracionAlkemy.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    public interface IPeliculaService
    {
        Task<Pelicula> Add(Pelicula entity);
        Task Delete(int id);
        Task<IEnumerable<Pelicula>> GetAll();
        Task<Pelicula> GetById(int id);
        Task<Pelicula> Update(Pelicula entity);
    }
}