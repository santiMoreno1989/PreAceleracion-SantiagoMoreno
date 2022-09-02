using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.GeneroView;
using ApiPreAceleracionAlkemy.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    public interface IGeneroService
    {
        Task<Genero> Add(GeneroPostViewModel entity);
        Task<DeleteViewModelResponse> Delete(int id);
        Task<IEnumerable<Genero>> GetAll();
        Task<Genero> GetById(int id);
        Task<Genero> Update(GeneroPutViewModel entity);
        Task<IEnumerable<GeneroGetViewModel>> GetByCondition(string nombre);
        Task<Pagination<GeneroGetViewModel>> GetGenerosAsync(int pageIndex, int pageSize,string order);
    }
}