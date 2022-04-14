using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.ViewModel.GeneroView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    public interface IGeneroService
    {
        Task<Genero> Add(Genero entity);
        Task Delete(int id);
        Task<IEnumerable<Genero>> GetAll();
        Task<Genero> GetById(int id);
        Task<Genero> Update(Genero entity);
        IEnumerable<GeneroGetViewModel> GetByCondition(string nombre);
    }
}