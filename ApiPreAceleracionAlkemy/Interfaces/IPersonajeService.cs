using ApiPreAceleracionAlkemy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Interfaces
{
    public interface IPersonajeService
    {
        Task<IEnumerable<Personaje>> GetAll();
        Task<IEnumerable<Personaje>> GetCustomsPersonajes(string sortOrder,string name,short? age,int? IdMovie);
        Task<Personaje> GetById(int id);
        Task<Personaje> Create(Personaje personaje);
        Task<Personaje> Edit(Personaje personaje);

        void Delete(int id);
    }
}
