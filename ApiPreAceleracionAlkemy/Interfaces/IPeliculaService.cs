using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.PeliculaView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    public interface IPeliculaService
    {
        Task<Pelicula> Add(PeliculaPostViewModel entity);
        Task Delete(int id);
        Task<IEnumerable<PeliculasGetViewModel>> GetAll();
        Task<Pelicula> GetById(int id);
        Task<Pelicula> UpdateAsync(int id,PeliculaPutViewModel entity);
    }
}