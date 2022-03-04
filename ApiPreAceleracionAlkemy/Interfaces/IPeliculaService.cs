using ApiPreAceleracionAlkemy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Interfaces
{
   public interface IPeliculaService
    {
        Task<IEnumerable<Pelicula>> GetAll();
        Task<Pelicula> GetById(int id);
        Task<Pelicula> Create(Pelicula pelicula);
        Task<Pelicula> Edit(Pelicula pelicula);

        void Delete(int id);
    }
}
