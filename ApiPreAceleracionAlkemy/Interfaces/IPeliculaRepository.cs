using ApiPreAceleracionAlkemy.Entities;
using System.Collections.Generic;

namespace ApiPreAceleracionAlkemy.Repositories
{
    public interface IPeliculaRepository : IBaseRepository<Pelicula>
    {
        Pelicula GetPelicula(int id);
        List<Pelicula> GetPeliculas();
    }
}