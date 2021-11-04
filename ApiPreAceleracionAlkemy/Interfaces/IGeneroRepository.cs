using ApiPreAceleracionAlkemy.Entities;
using System.Collections.Generic;

namespace ApiPreAceleracionAlkemy.Repositories
{
    public interface IGeneroRepository : IBaseRepository<Genero>
    {
        Genero GetGenero(int id);
        List<Genero> GetGeneros();
    }
}