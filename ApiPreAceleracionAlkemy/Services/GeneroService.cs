using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Filter;
using ApiPreAceleracionAlkemy.Interfaces;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.GeneroView;
using ApiPreAceleracionAlkemy.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    public class GeneroService : IGeneroService
    {

        private readonly IUnitOfWork _unitOfWork;

        public GeneroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Genero> Add(Genero entity)
        {
            return await _unitOfWork.generoRepository.Add(entity);
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.generoRepository.Delete(id);
        }

        public async Task<IEnumerable<Genero>> GetAll()=> await _unitOfWork.generoRepository.GetAll();

        public async Task<Genero> GetById(int id)
        {
            return await _unitOfWork.generoRepository.GetById(id);

        }

        public async Task<Genero> Update(Genero entity)
        {
            return await _unitOfWork.generoRepository.Update(entity);
        }
        public IEnumerable<GeneroGetViewModel> GetByCondition(string nombre) 
        {
            if (string.IsNullOrWhiteSpace(nombre) || nombre.Length < 3)
                throw new NotFoundException($"No se han encontrado resultados con el nombre : {nombre}");

            var internalGenero = _unitOfWork.generoRepository.FindByCondition(
                n => n.Nombre.ToLower().Contains(nombre.ToLower())).Select(x=>(GeneroGetViewModel) x)
                .OrderBy(n=> n.Nombre);
            
            return internalGenero;
        }

        public async Task<Pagination<GeneroGetViewModel>> GetGenerosAsync(int pageIndex, int pageSize,string sort)
        {
            var generos = _unitOfWork.generoRepository.GetQuery(
                include: ge=> ge.Include(pe=> pe.Peliculas));
            generos = sort switch
            {
                "NOMBRE_ASC" => generos.OrderBy(n => n.Nombre),
                "NOMBRE_DESC" => generos.OrderByDescending(n => n.Nombre),

                "FECHA_ASC" => generos.OrderBy(f=> f.FechaCreacion),
                "FECHA_DESC" => generos.OrderByDescending(f=> f.FechaCreacion),

                _ => generos.OrderBy(i=> i.Id)
            };

            return await generos.Select(ge => (GeneroGetViewModel)ge).PaginatedResponseAsync(pageIndex, pageSize);
        }

    }
}
