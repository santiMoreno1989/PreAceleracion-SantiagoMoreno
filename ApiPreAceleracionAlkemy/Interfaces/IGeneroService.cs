using ApiPreAceleracionAlkemy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Interfaces
{
   public interface IGeneroService
    {
        Task<IEnumerable<Genero>> GetAll();
        Task<Genero> GetById(int id);
        Task<Genero> Create(Genero genero);
        Task<Genero> Edit(Genero genero);

        void Delete(int id);
    }
}
